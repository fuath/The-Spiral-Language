Error trace on line: 30, column: 9 in file "CudaBlas".
    use cublas =
        ^
Error trace on line: 78, column: 5 in file "CudaBlas".
    ret <| s.module_add .CudaBlas {gemm' gemm}
    ^
Error trace on line: 107, column: 12 in file "Core".
inl (<|) a b = a b
           ^
Error trace on line: 6, column: 9 in file "CudaModules".
    inb s = CudaBlas s
        ^
Error trace on line: 7, column: 9 in file "CudaModules".
    inl s = Region s |> CudaStream |> CudaTensor |> CudaKernel
        ^
Error trace on line: 46, column: 36 in file "Region".
inl create' {region_module_name} s ret =
                                   ^
Error trace on line: 47, column: 9 in file "Region".
    inl s = s region_module_name .create
        ^
Error trace on line: 48, column: 13 in file "Region".
    inl r = ret s
            ^
Error trace on line: 8, column: 9 in file "CudaModules".
    inb s = s.RegionMem.create'
        ^
Error trace on line: 46, column: 36 in file "Region".
inl create' {region_module_name} s ret =
                                   ^
Error trace on line: 47, column: 9 in file "Region".
    inl s = s region_module_name .create
        ^
Error trace on line: 48, column: 13 in file "Region".
    inl r = ret s
            ^
Error trace on line: 9, column: 9 in file "CudaModules".
    inb s = s.RegionStream.create'
        ^
Error trace on line: 10, column: 5 in file "CudaModules".
    ret s
    ^
Error trace on line: 2, column: 5 in file "kernel3".
inb s = CudaModules (1024*1024)
    ^
Error trace on line: 4, column: 5 in file "kernel3".
inl inner_size = 8
    ^
Error trace on line: 5, column: 5 in file "kernel3".
inl outer_size = 8
    ^
Error trace on line: 7, column: 5 in file "kernel3".
inl h = HostTensor.init inner_size (const (2,2))
    ^
Error trace on line: 8, column: 5 in file "kernel3".
inl h' = HostTensor.init (outer_size,inner_size) (inl a b -> a,b)
    ^
Error trace on line: 9, column: 5 in file "kernel3".
inl a1 = s.CudaTensor.from_host_tensor h
    ^
Error trace on line: 10, column: 5 in file "kernel3".
inl a2 = s.CudaTensor.from_host_tensor h'
    ^
Error trace on line: 11, column: 5 in file "kernel3".
inl o1 = s.CudaKernel.d2_replicate_map (inl a b -> a, b) a1 a2
    ^
Error trace on line: 12, column: 10 in file "kernel3".
inl o2 = s.CudaKernel.d2_replicate_map (inl a _ -> a) a1 outer_size
         ^
Error trace on line: 374, column: 36 in file "CudaKernel".
inl d2_replicate_map w f (!zip in) in' =
                                   ^
Error trace on line: 375, column: 9 in file "CudaKernel".
    inl in' =
        ^
Error trace on line: 381, column: 9 in file "CudaKernel".
    inl out = w.CudaTensor.create {elem_type=type f in.elem_type in'.elem_type; dim=in'.dim}
        ^
Error trace on line: 382, column: 5 in file "CudaKernel".
    d2_replicate_map' w (inl a b _ -> f a b) in in' out
    ^
Error trace on line: 344, column: 48 in file "CudaKernel".
inl d2_replicate_map' w f (!zip in) (!zip in') (!zip out) =
                                               ^
Error trace on line: 345, column: 9 in file "CudaKernel".
    inl dim_in :: () = in.dim
        ^
Error trace on line: 346, column: 9 in file "CudaKernel".
    inl dim_in'_a, dim_in'_b = in'.dim
        ^
Error trace on line: 351, column: 9 in file "CudaKernel".
    inl blockDimX = min warp_size (s dim_in)
        ^
Error trace on line: 352, column: 9 in file "CudaKernel".
    inl blockDimY = min 32 (s dim_in'_a)
        ^
Error trace on line: 353, column: 9 in file "CudaKernel".
    inl gridDim = min 64 (divup (s dim_in) blockDimX)
        ^
Error trace on line: 354, column: 9 in file "CudaKernel".
    inl in = in.to_dev_tensor
        ^
Error trace on line: 355, column: 15 in file "CudaKernel".
    inl in' = in'.to_dev_tensor
              ^
Error trace on line: 185, column: 7 in file "HostTensor".
    | .(_) & x -> 
      ^
Error trace on line: 186, column: 9 in file "HostTensor".
        if module_has_member data x then data x
        ^
Error trace on line: 187, column: 14 in file "HostTensor".
        else data.methods x data
             ^
Cannot find a member named to_dev_tensor inside the module.
