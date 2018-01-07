﻿module Modules

open Spiral.Types
open Spiral.Lib

let allocator = 
    (
    "Allocator",[option;extern_],"The stack based GPU memory allocator module.",
    """
inl {Cuda size} ret ->
    open Cuda
    open Extern
    inl smartptr_create ptr =
        inl ptr_ty = {value = type ptr} |> stack // Seals the type in a layout type so it does not get instantiated.
        inl cell = Option.some ptr |> ref
        function
        | .Dispose -> cell := Option.none ptr_ty.value
        | .Try -> cell()
        | () -> join (
            match cell() with
            | .Some, x -> x
            | _ -> failwith ptr_ty.value "A Cuda memory cell that has been disposed has been tried to be accessed."
            )

    ///// n is the number of args the create function has.
    inl safe_alloc n create =
        if lit_is n = false then error_type "n need to be static."
        inl rec loop vars = function
            | 0 ret ->
                inl tns = Tuple.foldr (inl x create -> create x) vars create
                inl r = ret tns
                tns.update_body (inl {ar} -> ar.ptr.Dispose) |> ignore
                r
            | n x -> loop (x :: vars) (n-1)
        function
        | .unsafe -> create
        | x -> loop () n x

    inl to_float x = FS.UnOp .float x float64
    inl to_int x = FS.UnOp .int64 x int64
    
    inl pool = 
        inl size = 
            match size with
            | _ : float64 -> 
                inl CudaDeviceProperties_type = fs [text: "ManagedCuda.CudaDeviceProperties"]
                FS.Method context .GetDeviceInfo() CudaDeviceProperties_type
                |> inl x -> FS.Method x .get_TotalGlobalMemory() SizeT_type
                |> to_int |> to_float |> (*) size |> to_int
            | _ : int64 -> size
        inl q = FS.Method context .AllocateMemory (SizeT size) CUdeviceptr_type
        {size ptr=smartptr_create q}

    inl pool_type = type pool
    inl stack_type = fs [text: "System.Collections.Generic.Stack"; types: pool_type]
    inl stack = FS.Constructor stack_type ()

    inl allocate =
        inl smartptr_ty = type (pool.ptr)
        inl f x = x.ptr() |> ptr_to_uint, x.size |> to_uint
        inl pool_ptr, pool_size = f pool
        met rec remove_disposed_and_return_the_first_live ret =
            if FS.Method stack .get_Count() int32 > 0i32 then 
                inl t = FS.Method stack .Peek() pool_type
                match t.ptr.Try with
                | .Some, ptr -> join (ret (ptr_to_uint ptr, t.size |> to_uint))
                | _ -> FS.Method stack .Pop() pool_type |> ignore; remove_disposed_and_return_the_first_live ret 
            else join (ret (pool_ptr, 0u64))
            : smartptr_ty
        inl (!dyn size) ->
            inb top_ptr, top_size = remove_disposed_and_return_the_first_live
            inl pool_used = top_ptr - pool_ptr + top_size
            assert (to_uint size + pool_used <= pool_size) "Cache size has been exceeded in the allocator."
            inl cell = {size ptr=top_ptr + top_size |> uint_to_ptr |> smartptr_create}
            FS.Method stack .Push cell unit
            cell.ptr

    ret {allocate ptr_to_uint uint_to_ptr safe_alloc}

    inl ptr = pool.ptr
    FS.Method context .FreeMemory (ptr()) unit
    ptr.Dispose
    """) |> module_

let cuda_tensor = 
    (
    "CudaTensor",[option;extern_;host_tensor],"The Cuda tensor module.",
    """
inl {stream Cuda Allocator} ->
    open Cuda
    open Allocator
    open HostTensor
    open Extern
    inl cuda_array_create elem_type len = 
        inl ptr = allocate (len * unsafe_convert int64 (sizeof elem_type))
        function // It needs to be like this rather than a module so toa_map does not split it.
        | .elem_type -> elem_type
        | .ptr -> ptr
    inl create data = create {data with array_create = cuda_array_create}
    inl create_like tns = create {elem_type=tns.elem_type; dim=tns.dim}

    inl from_host_array ar =
        inl elem_type = ar.elem_type
        inl size = array_length ar |> unsafe_convert int64
        inl t = cuda_array_create elem_type size
        FS.Method context .CopyToDevice(t.ptr(), ar) unit
        t

    inl to_host_array size1d ar =
        inl elem_type = ar.elem_type
        inl ptr = ar.ptr()
        inl t = array_create elem_type size1d
        FS.Method context .CopyToHost (t,ptr) unit
        FS.Method context .Synchronize() unit
        t

    inl transfer_template f tns = 
        assert_contiguous tns
        tns.update_body <| inl {body with offset=o::_ ar} ->
            // I do not feel like messing with GC handles in Spiral right now.
            // Allowing a copy with an offset would be easy though. See ManagedCuda's CopyToHost and CopyToDevice.
            assert (o = 0) "Only unviewed arrays are allowed for now."
            {body with ar = f ar}

    inl from_host_tensor = transfer_template from_host_array
    inl to_host_tensor tns = transfer_template (to_host_array (length tns)) tns

    inl to_dev_tensor tns = 
        tns.update_body (inl {body with ar offset} ->
            inl ptr, elem_type = ar.ptr(), ar.elem_type
            inl o = match offset with o :: _ | o -> o
            inl ptr = ptr_to_uint ptr + unsafe_convert uint64 o |> uint_to_ptr    
            inl ar = !UnsafeCoerceToArrayCudaGlobal(ptr,elem_type)
            inl offset = match offset with _ :: o' -> 0 :: o' | offset -> 0
            {body with ar offset}
            )

    inl clear (!to_dev_tensor tns) = 
        assert_contiguous tns
        inl size = length tns
        inl stream = Stream.extract stream
        tns.update_body <| inl {body with ar} ->
            FS.Method context .ClearMemoryAsync (ar,0u8,size * sizeof (ar.elem_type) |> SizeT,stream) unit
        |> ignore

    inl clear' x = clear x; x
    inl zero = create >> clear'
    inl zero_like = create_like >> clear'

    inl from_host_tensors x ret = 
        inl tensors = toa_map from_host_tensor x
        inl r = ret tensors
        toa_map (inl x -> x.update_body (inl {ar} -> ar.ptr.Dispose)) |> ignore
        r

    // CPS'd variants of the allocator functions.
    inl create = safe_alloc 1 create
    inl from_host_tensor = safe_alloc 1 from_host_tensor
    inl zero_like = safe_alloc 1 zero_like

    {create from_host_tensor from_host_tensors to_host_tensor to_dev_tensor clear zero zero_like}
    """) |> module_

let cuda_kernel =
    (
    "CudaKernel",[lazy_;host_tensor],"The Cuda kernels module.",
    """
inl {stream Cuda CudaTensor} ->
    open HostTensor
    open Cuda
    open CudaTensor
    open Extern
    inl divup a b = (a-1)/b+1 // Integer division with rounding up. (a+b-1)/b is another variant on this.
    inl map f (!zip in) (!zip out) =
        assert_zip (in, out) |> ignore
        inl in = to_1d in |> to_dev_tensor
        inl out = to_1d out |> to_dev_tensor
        inl near_to = length in

        inl blockDim = 128
        inl gridDim = min 64 (divup near_to blockDim)

        run {
            stream blockDim gridDim
            kernel = cuda // Lexical scoping rocks.
                inl from = blockIdx.x * blockDim.x + threadIdx.x
                inl by = gridDim.x * blockDim.x
                Loops.for {from near_to by body=inl {i} -> out i .set (f (in i .get))}
            } |> ignore

    /// Flattens the tensor to 1d, maps and reduces it.
    /// Lazily returns the output. Requires the redo and the neutral element.
    /// Map is optional. Allocates a temporary tensor for the intermediary results.
	/// Requires the continuation in order for the sake of memory allocation.
    inl map_redo {d with redo neutral_elem} (!zip (!to_1d in)) ret =
        inl in' = to_dev_tensor in
        inl near_to = length in'
        inl map = match d with {map} -> map | _ -> id

        inl blockDim = 128
        inl gridDim = min 64 (divup near_to blockDim)
        inl elem_type = type (in.elem_type |> map)
        inl ty = elem_type

        inl cub_block_reduce thread_result redo =
            macro.cd ty [
                text: "cub::BlockReduce"
                iter: "<",",",">",[type: ty; arg: blockDim]
                args: ()
                text: ".Reduce"
                args: thread_result, redo
                ]

        inb out = create {elem_type dim=gridDim}
        inl out' = to_dev_tensor out

        run {
            stream blockDim gridDim
            kernel = cuda 
                inl from = blockIdx.x * blockDim.x + threadIdx.x
                inl by = gridDim.x * blockDim.x
                inl load i = map (in' i .get)
                inl thread_result = Loops.for {from near_to by state=dyn neutral_elem; body=inl {state i} -> redo state (load i)}

                inl redo = closure_of (inl a,b -> redo a b) ((ty,ty) => ty)
                inl block_result = cub_block_reduce thread_result redo
                if threadIdx.x = 0 then out' (blockIdx.x) .set block_result
            } |> ignore

        inl _ ->
            inl tns = to_host_tensor out
            inl load i = tns i .get
            Loops.for {from=0; near_to=length tns; state=dyn neutral_elem; body=inl {state i} -> redo state (load i)}
        |> Lazy.lazy // The lazy return here is because transfering to host would block the execution.
        |> ret

    {map map_redo}
    """) |> module_

let cuda_random =
    (
    "CudaRandom",[extern_],"The CudaRandom module.",
    """
inl ret ->
    open Extern
    use random = 
        inl generator_type = fs [text: "ManagedCuda.CudaRand.GeneratorType"]
        FS.Constructor (fs [text: "ManagedCuda.CudaRand.CudaRandDevice"]) (FS.StaticField generator_type .PseudoDefault generator_type)
    
    ret inl {d with stream Cuda CudaTensor} ->
        open Cuda
        open HostTensor
        open CudaTensor
        FS.Method random .SetStream (Stream.extract stream) unit
    
        inl fill_array distribution size1d ar =
            inl elem_type = ar.elem_type
            inl gen, dot = "Generate", "."
            match distribution with
            | .Uniform ->
                inl args = ar, SizeT size1d
                inl bits = 
                    match elem_type with
                    | _ : float32 -> "32" | _ : float64 -> "64"
                    | _ -> error_type ("Only 32/64 bit float types are supported. Try UInt if you need uint random numbers. Got: ", elem_type)
                macro.fs unit [arg: random; text: dot; text: gen; text: distribution; text: bits; args: args]
            | {dst=(.Normal | .LogNormal) & distribution stddev mean} ->
                match stddev with | _: float32 -> () | _ -> error_type "Standard deviation needs to be in float32."
                match mean with | _: float32 -> () | _ -> error_type "Mean needs to be in float32."

                inl args = ar, SizeT size1d, mean, stddev
                inl bits = 
                    match elem_type with
                    | _ : float32 -> "32" | _ : float64 -> "64"
                    | _ -> error_type ("Only 32/64 bit float types are supported. Try UInt if you need uint random numbers. Got: ", elem_type)
                macro.fs unit [arg: random; text: dot; text: gen; text: distribution; text: bits; args: args]
            | .UInt -> // every bit random
                inl args = ar, SizeT size1d
                inl bits =
                    match elem_type with
                    | _ : uint32 -> "32" | _ : uint64 -> "64"
                    | _ -> error_type "Only 32/64 bit uint types are supported."
                macro.fs unit [arg: random; text: dot; text: gen; text: bits; args: args]

        inl fill op (!zip in) =
            inl in' = to_1d in |> to_dev_tensor
            inl len = length in'
            in'.update_body (inl {ar} -> fill_array op len ar) |> ignore

        inl create_tensor op dsc ret =
            inb device_tensor = create dsc
            fill op device_tensor
            ret device_tensor

        {fill create_tensor}
    """) |> module_

let cuda_blas =
    (
    "CudaBlas",[extern_],"The CudaBlas module.",
    """
inl ret ->
    open Extern
    
    inl enum ty x = FS.StaticField ty x ty

    inl operation_type = fs [text: "ManagedCuda.CudaBlas.Operation"]
    inl to_operation = function
        | .T -> enum operation_type .Transpose
        | .nT -> enum operation_type .NonTranspose

    inl isT = function
        | .T -> true
        | _ -> false

    inl isnT = function
        | .nT -> true
        | _ -> false

    inl len {from near_to} = near_to - from
    inl rows x = x.dim |> inl a,b -> len a
    inl cols x = x.dim |> inl a,b -> len b

    inl assert_singleton x = 
        match x.bodies with
        | _ :: _ | {!block_toa_map} -> error_type "Expected a singleton tensor."
        | _ -> ()
    inl to_dev_tensor x = assert_contiguous x; assert_singleton x; to_dev_tensor x
    inl call m x = 
        inl native_type = fs [text: "ManagedCuda.CudaBlas.CudaBlasNativeMethods"]
        inl status_type = fs [text: "ManagedCuda.CudaBlas.CublasStatus"]
        inl assert_ok status = !MacroFs(unit,[text: "if "; arg: status; text: " <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException"; args: status])
        inl x = Tuple.map (function x : int64 -> unsafe_convert int32 x | x -> x) x
        FS.StaticMethod native_type m x status_type |> assert_ok

    use cublas =
        inl cublas_type = fs [text: "ManagedCuda.CudaBlas.CudaBlas"]
        inl pointer_mode_type = fs [text: "ManagedCuda.CudaBlas.PointerMode"]
        inl atomics_mode_type = fs [text: "ManagedCuda.CudaBlas.AtomicsMode"]
        FS.Constructor cublas_type (enum pointer_mode_type .Host, enum atomics_mode_type .Allowed)

    inl handle = FS.Method cublas .get_CublasHandle() (fs [text: "ManagedCuda.CudaBlas.CudaBlasHandle"])

    ret inl {d with stream Cuda CudaKernel CudaTensor} ->
        open Cuda
        open HostTensor
        open CudaTensor

        FS.Method cublas .set_Stream (Stream.extract stream) unit

        /// General matrix-matrix multiply from cuBLAS. Inplace version
        inl gemm' transa transb alpha (!to_dev_tensor A) (!to_dev_tensor B) beta (!to_dev_tensor C) =
            // -------

            // These two are meant to be called from inside gemm as they lack boundary checks.
            // I've added them to enhance gemm's vector handling capabilities for online learning
            // tasks.

            /// o <- alpha * op(A) * x + beta * o
            /// Matrix-vector multiplication. Inplace version.
            inl gemv transa alpha A x beta o =
                inl m,n = rows A, cols A
                inl lda = m
                call.cublasSgemv_v2(handle, to_operation transa, m, n, ref alpha, A.bodies.ar, lda, x.bodies.ar, 1, ref beta, o.bodies.ar, 1)

            // A <- alpha * x * yT + beta * A (outer product)
            inl ger alpha x y beta a =
                inl max (a,b) = max a b
                inl m = max (rows x, cols x)
                inl n = max (rows y, cols y)

                match beta with
                | 0.0f64 | 0.0f32 -> ()
                | _ -> CudaKernel.map (toa_map ((*) beta)) a a

                call.cublasSger_v2(handle, m, n, ref alpha, x.bodies.ar, 1, y.bodies.ar, 1, a.bodies.ar, m)

            // -------

            inl is_vector x = rows x = 1 || cols x = 1

            inl a_col = if isnT transa then cols A else rows A
            inl b_row = if isnT transb then rows B else cols B
            assert (a_col = b_row) "Colums of a does not match rows of b in GEMM."

            inl m = if isnT transa then rows A else cols A
            inl n = if isnT transb then cols B else rows B
            inl k = a_col
            inl lda = if isnT transa then m else k
            inl ldb = if isnT transb then k else n
            inl ldc = m
        
            assert (m = rows C && n = cols C) "Output matrix dimensions do not match in GEMM."

            // If is outer product call ger
            if a_col = 1 && b_row = 1 then ger alpha A B beta C
            // If the vector is on the right side or both are vectors call gemv normally.
            elif is_vector B then gemv transa alpha A B beta C
            // If the vector is on the left side call gemv with the arguments switched and transposed
            // It does not actually transpose them, just their views. The function should work regardless.
            elif is_vector A then
                inl optb = if isnT transb then .T else .nT
                gemv optb alpha B A beta C
            // Just do the standard matrix multiply
            else
                call.cublasSgemm_v2(handle, to_operation transa, to_operation transb, m, n, k, ref alpha, A.bodies.ar, lda, B.bodies.ar, ldb, ref beta, C.bodies.ar, ldc)

        {gemm'}
    """) |> module_
