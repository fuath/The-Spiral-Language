module SpiralExample.Main
let cuda_kernels = """
#include "cub/cub.cuh"

extern "C" {
    
}
"""

type EnvStack0 =
    struct
    val mem_0: (uint64 ref)
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and Env1 =
    struct
    val mem_0: EnvStack0
    val mem_1: uint64
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and EnvStack2 =
    struct
    val mem_0: (int64 ref)
    val mem_1: EnvStack0
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and EnvStack3 =
    struct
    val mem_0: (int64 ref)
    val mem_1: EnvStack5
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and EnvStack4 =
    struct
    val mem_0: EnvStack2
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and EnvStack5 =
    struct
    val mem_0: (bool ref)
    val mem_1: ManagedCuda.CudaStream
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
let rec method_0 ((var_0: System.Diagnostics.DataReceivedEventArgs)): unit =
    let (var_1: string) = var_0.get_Data()
    let (var_2: string) = System.String.Format("{0}",var_1)
    System.Console.WriteLine(var_2)
and method_1((var_0: ResizeArray<Env1>), (var_1: EnvStack0), (var_2: uint64), (var_3: ResizeArray<Env1>)): unit =
    let (var_5: (Env1 -> bool)) = method_2
    let (var_6: int32) = var_3.RemoveAll <| System.Predicate(var_5)
    let (var_8: (Env1 -> (Env1 -> int32))) = method_3
    let (var_9: System.Comparison<Env1>) = System.Comparison<Env1>(var_8)
    var_3.Sort(var_9)
    var_0.Clear()
    let (var_10: int32) = var_3.get_Count()
    let (var_11: int32) = 0
    let (var_12: Env1) = method_6((var_0: ResizeArray<Env1>), (var_10: int32), (var_1: EnvStack0), (var_11: int32))
    let (var_13: EnvStack0) = var_12.mem_0
    let (var_14: uint64) = var_12.mem_1
    let (var_15: (uint64 ref)) = var_13.mem_0
    let (var_16: uint64) = method_5((var_15: (uint64 ref)))
    let (var_17: uint64) = (var_16 + var_14)
    let (var_18: uint64) = method_5((var_15: (uint64 ref)))
    let (var_19: uint64) = (var_18 - var_17)
    let (var_20: uint64) = method_5((var_15: (uint64 ref)))
    let (var_21: uint64) = (var_20 + 256UL)
    let (var_22: uint64) = (var_21 - 1UL)
    let (var_23: uint64) = (var_22 / 256UL)
    let (var_24: uint64) = (var_23 * 256UL)
    let (var_25: uint64) = (var_24 - var_20)
    let (var_26: bool) = (var_19 >= var_25)
    if var_26 then
        let (var_27: (uint64 ref)) = (ref var_24)
        let (var_28: EnvStack0) = EnvStack0((var_27: (uint64 ref)))
        let (var_29: uint64) = (var_19 - var_25)
        var_0.Add((Env1(var_28, var_29)))
    else
        ()
and method_8((var_0: ResizeArray<EnvStack3>)): EnvStack3 =
    let (var_1: ManagedCuda.CudaStream) = ManagedCuda.CudaStream()
    let (var_2: (bool ref)) = (ref true)
    let (var_3: EnvStack5) = EnvStack5((var_2: (bool ref)), (var_1: ManagedCuda.CudaStream))
    let (var_4: (int64 ref)) = (ref 0L)
    let (var_5: EnvStack3) = EnvStack3((var_4: (int64 ref)), (var_3: EnvStack5))
    method_9((var_5: EnvStack3), (var_0: ResizeArray<EnvStack3>))
    var_5
and method_10((var_0: (int64 [])), (var_1: (int64 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 8L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        var_0.[int32 var_2] <- 2L
        var_1.[int32 var_2] <- 2L
        let (var_6: int64) = (var_2 + 1L)
        method_10((var_0: (int64 [])), (var_1: (int64 [])), (var_6: int64))
    else
        ()
and method_11((var_0: (int64 [])), (var_1: (int64 [])), (var_2: int64)): unit =
    let (var_3: bool) = (var_2 < 8L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: int64) = (var_2 * 8L)
        let (var_7: int64) = 0L
        method_12((var_2: int64), (var_0: (int64 [])), (var_6: int64), (var_1: (int64 [])), (var_7: int64))
        let (var_8: int64) = (var_2 + 1L)
        method_11((var_0: (int64 [])), (var_1: (int64 [])), (var_8: int64))
    else
        ()
and method_13((var_0: ManagedCuda.CudaContext), (var_1: ManagedCuda.BasicTypes.CUmodule), (var_2: ResizeArray<EnvStack2>), (var_3: ResizeArray<Env1>), (var_4: EnvStack0), (var_5: uint64), (var_6: ResizeArray<Env1>), (var_7: EnvStack3), (var_8: int64), (var_9: (int64 [])), (var_10: int64), (var_11: int64)): EnvStack4 =
    let (var_12: int64) = (var_8 * var_11)
    let (var_13: System.Runtime.InteropServices.GCHandle) = System.Runtime.InteropServices.GCHandle.Alloc(var_9,System.Runtime.InteropServices.GCHandleType.Pinned)
    let (var_14: int64) = var_13.AddrOfPinnedObject().ToInt64()
    let (var_15: uint64) = (uint64 var_14)
    let (var_16: int64) = (var_10 * 8L)
    let (var_17: uint64) = (uint64 var_16)
    let (var_18: uint64) = (var_17 + var_15)
    let (var_19: int64) = (var_12 * 8L)
    let (var_20: EnvStack2) = method_14((var_2: ResizeArray<EnvStack2>), (var_3: ResizeArray<Env1>), (var_4: EnvStack0), (var_5: uint64), (var_6: ResizeArray<Env1>), (var_19: int64))
    let (var_21: EnvStack4) = EnvStack4((var_20: EnvStack2))
    let (var_22: EnvStack2) = var_21.mem_0
    let (var_23: (int64 ref)) = var_22.mem_0
    let (var_24: EnvStack0) = var_22.mem_1
    let (var_25: (uint64 ref)) = var_24.mem_0
    let (var_26: uint64) = method_5((var_25: (uint64 ref)))
    let (var_27: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_26)
    let (var_28: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_27)
    let (var_29: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_18)
    let (var_30: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_29)
    let (var_31: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_19)
    let (var_32: ManagedCuda.BasicTypes.CUResult) = ManagedCuda.DriverAPINativeMethods.SynchronousMemcpy_v2.cuMemcpy(var_28, var_30, var_31)
    if var_32 <> ManagedCuda.BasicTypes.CUResult.Success then raise <| new ManagedCuda.CudaException(var_32)
    var_13.Free()
    var_21
and method_17((var_0: ManagedCuda.CudaContext), (var_1: ManagedCuda.BasicTypes.CUmodule), (var_2: ResizeArray<EnvStack2>), (var_3: ResizeArray<Env1>), (var_4: EnvStack0), (var_5: uint64), (var_6: ResizeArray<Env1>), (var_7: EnvStack3), (var_8: int64), (var_9: (int64 [])), (var_10: int64), (var_11: int64), (var_12: int64)): EnvStack4 =
    let (var_13: int64) = (var_8 * var_11)
    let (var_14: System.Runtime.InteropServices.GCHandle) = System.Runtime.InteropServices.GCHandle.Alloc(var_9,System.Runtime.InteropServices.GCHandleType.Pinned)
    let (var_15: int64) = var_14.AddrOfPinnedObject().ToInt64()
    let (var_16: uint64) = (uint64 var_15)
    let (var_17: int64) = (var_10 * 8L)
    let (var_18: uint64) = (uint64 var_17)
    let (var_19: uint64) = (var_18 + var_16)
    let (var_20: int64) = (var_13 * 8L)
    let (var_21: EnvStack2) = method_14((var_2: ResizeArray<EnvStack2>), (var_3: ResizeArray<Env1>), (var_4: EnvStack0), (var_5: uint64), (var_6: ResizeArray<Env1>), (var_20: int64))
    let (var_22: EnvStack4) = EnvStack4((var_21: EnvStack2))
    let (var_23: EnvStack2) = var_22.mem_0
    let (var_24: (int64 ref)) = var_23.mem_0
    let (var_25: EnvStack0) = var_23.mem_1
    let (var_26: (uint64 ref)) = var_25.mem_0
    let (var_27: uint64) = method_5((var_26: (uint64 ref)))
    let (var_28: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_27)
    let (var_29: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_28)
    let (var_30: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_19)
    let (var_31: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_30)
    let (var_32: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_20)
    let (var_33: ManagedCuda.BasicTypes.CUResult) = ManagedCuda.DriverAPINativeMethods.SynchronousMemcpy_v2.cuMemcpy(var_29, var_31, var_32)
    if var_33 <> ManagedCuda.BasicTypes.CUResult.Success then raise <| new ManagedCuda.CudaException(var_33)
    var_14.Free()
    var_22
and method_18((var_0: ResizeArray<EnvStack3>)): unit =
    let (var_2: (EnvStack3 -> unit)) = method_19
    var_0.ForEach <| System.Action<_>(var_2)
    var_0.Clear()
and method_20((var_0: ResizeArray<EnvStack2>)): unit =
    let (var_2: (EnvStack2 -> unit)) = method_21
    var_0.ForEach <| System.Action<_>(var_2)
    var_0.Clear()
and method_5((var_0: (uint64 ref))): uint64 =
    let (var_1: uint64) = (!var_0)
    let (var_2: bool) = (var_1 <> 0UL)
    let (var_3: bool) = (var_2 = false)
    if var_3 then
        (failwith "A Cuda memory cell that has been disposed has been tried to be accessed.")
    else
        ()
    var_1
and method_2 ((var_0: Env1)): bool =
    let (var_1: EnvStack0) = var_0.mem_0
    let (var_2: uint64) = var_0.mem_1
    let (var_3: (uint64 ref)) = var_1.mem_0
    let (var_4: uint64) = (!var_3)
    (var_4 = 0UL)
and method_3 ((var_0: Env1)): (Env1 -> int32) =
    let (var_1: EnvStack0) = var_0.mem_0
    let (var_2: uint64) = var_0.mem_1
    method_4((var_1: EnvStack0))
and method_6((var_0: ResizeArray<Env1>), (var_1: int32), (var_2: EnvStack0), (var_3: int32)): Env1 =
    let (var_4: bool) = (var_3 < var_1)
    if var_4 then
        let (var_5: Env1) = var_0.[var_3]
        let (var_6: EnvStack0) = var_5.mem_0
        let (var_7: uint64) = var_5.mem_1
        let (var_8: (uint64 ref)) = var_2.mem_0
        let (var_9: uint64) = method_5((var_8: (uint64 ref)))
        let (var_10: uint64) = method_5((var_8: (uint64 ref)))
        let (var_11: uint64) = (var_10 - var_9)
        let (var_12: uint64) = method_5((var_8: (uint64 ref)))
        let (var_13: uint64) = (var_12 + 256UL)
        let (var_14: uint64) = (var_13 - 1UL)
        let (var_15: uint64) = (var_14 / 256UL)
        let (var_16: uint64) = (var_15 * 256UL)
        let (var_17: uint64) = (var_16 - var_12)
        let (var_18: bool) = (var_11 >= var_17)
        if var_18 then
            let (var_19: (uint64 ref)) = (ref var_16)
            let (var_20: EnvStack0) = EnvStack0((var_19: (uint64 ref)))
            let (var_21: uint64) = (var_11 - var_17)
            var_0.Add((Env1(var_20, var_21)))
        else
            ()
        let (var_22: int32) = (var_3 + 1)
        method_7((var_0: ResizeArray<Env1>), (var_1: int32), (var_6: EnvStack0), (var_7: uint64), (var_22: int32))
    else
        (Env1(var_2, 0UL))
and method_9((var_0: EnvStack3), (var_1: ResizeArray<EnvStack3>)): unit =
    let (var_2: (int64 ref)) = var_0.mem_0
    let (var_3: EnvStack5) = var_0.mem_1
    let (var_4: int64) = (!var_2)
    let (var_5: int64) = (var_4 + 1L)
    var_2 := var_5
    var_1.Add(var_0)
and method_12((var_0: int64), (var_1: (int64 [])), (var_2: int64), (var_3: (int64 [])), (var_4: int64)): unit =
    let (var_5: bool) = (var_4 < 8L)
    if var_5 then
        let (var_6: bool) = (var_4 >= 0L)
        let (var_7: bool) = (var_6 = false)
        if var_7 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_8: int64) = (var_2 + var_4)
        var_1.[int32 var_8] <- var_0
        var_3.[int32 var_8] <- var_4
        let (var_9: int64) = (var_4 + 1L)
        method_12((var_0: int64), (var_1: (int64 [])), (var_2: int64), (var_3: (int64 [])), (var_9: int64))
    else
        ()
and method_14((var_0: ResizeArray<EnvStack2>), (var_1: ResizeArray<Env1>), (var_2: EnvStack0), (var_3: uint64), (var_4: ResizeArray<Env1>), (var_5: int64)): EnvStack2 =
    let (var_6: uint64) = (uint64 var_5)
    let (var_7: uint64) = (var_6 + 256UL)
    let (var_8: uint64) = (var_7 - 1UL)
    let (var_9: uint64) = (var_8 / 256UL)
    let (var_10: uint64) = (var_9 * 256UL)
    let (var_11: EnvStack0) = method_15((var_1: ResizeArray<Env1>), (var_2: EnvStack0), (var_3: uint64), (var_4: ResizeArray<Env1>), (var_10: uint64))
    let (var_12: (int64 ref)) = (ref 0L)
    let (var_13: EnvStack2) = EnvStack2((var_12: (int64 ref)), (var_11: EnvStack0))
    method_16((var_13: EnvStack2), (var_0: ResizeArray<EnvStack2>))
    var_13
and method_19 ((var_0: EnvStack3)): unit =
    let (var_1: (int64 ref)) = var_0.mem_0
    let (var_2: EnvStack5) = var_0.mem_1
    let (var_3: int64) = (!var_1)
    let (var_4: int64) = (var_3 - 1L)
    var_1 := var_4
    let (var_5: int64) = (!var_1)
    let (var_6: bool) = (var_5 = 0L)
    if var_6 then
        let (var_7: (bool ref)) = var_2.mem_0
        let (var_8: ManagedCuda.CudaStream) = var_2.mem_1
        var_8.Dispose()
        var_7 := false
    else
        ()
and method_21 ((var_0: EnvStack2)): unit =
    let (var_1: (int64 ref)) = var_0.mem_0
    let (var_2: EnvStack0) = var_0.mem_1
    let (var_3: int64) = (!var_1)
    let (var_4: int64) = (var_3 - 1L)
    var_1 := var_4
    let (var_5: int64) = (!var_1)
    let (var_6: bool) = (var_5 = 0L)
    if var_6 then
        let (var_7: (uint64 ref)) = var_2.mem_0
        var_7 := 0UL
    else
        ()
and method_4 ((var_1: EnvStack0)) ((var_0: Env1)): int32 =
    let (var_2: EnvStack0) = var_0.mem_0
    let (var_3: uint64) = var_0.mem_1
    let (var_4: (uint64 ref)) = var_1.mem_0
    let (var_5: uint64) = method_5((var_4: (uint64 ref)))
    let (var_6: (uint64 ref)) = var_2.mem_0
    let (var_7: uint64) = method_5((var_6: (uint64 ref)))
    let (var_8: bool) = (var_5 < var_7)
    if var_8 then
        -1
    else
        let (var_9: bool) = (var_5 = var_7)
        if var_9 then
            0
        else
            1
and method_7((var_0: ResizeArray<Env1>), (var_1: int32), (var_2: EnvStack0), (var_3: uint64), (var_4: int32)): Env1 =
    let (var_5: bool) = (var_4 < var_1)
    if var_5 then
        let (var_6: Env1) = var_0.[var_4]
        let (var_7: EnvStack0) = var_6.mem_0
        let (var_8: uint64) = var_6.mem_1
        let (var_9: (uint64 ref)) = var_2.mem_0
        let (var_10: uint64) = method_5((var_9: (uint64 ref)))
        let (var_11: uint64) = (var_10 + var_3)
        let (var_12: uint64) = method_5((var_9: (uint64 ref)))
        let (var_13: uint64) = (var_12 - var_11)
        let (var_14: uint64) = method_5((var_9: (uint64 ref)))
        let (var_15: uint64) = (var_14 + 256UL)
        let (var_16: uint64) = (var_15 - 1UL)
        let (var_17: uint64) = (var_16 / 256UL)
        let (var_18: uint64) = (var_17 * 256UL)
        let (var_19: uint64) = (var_18 - var_14)
        let (var_20: bool) = (var_13 >= var_19)
        if var_20 then
            let (var_21: (uint64 ref)) = (ref var_18)
            let (var_22: EnvStack0) = EnvStack0((var_21: (uint64 ref)))
            let (var_23: uint64) = (var_13 - var_19)
            var_0.Add((Env1(var_22, var_23)))
        else
            ()
        let (var_24: int32) = (var_4 + 1)
        method_7((var_0: ResizeArray<Env1>), (var_1: int32), (var_7: EnvStack0), (var_8: uint64), (var_24: int32))
    else
        (Env1(var_2, var_3))
and method_15((var_0: ResizeArray<Env1>), (var_1: EnvStack0), (var_2: uint64), (var_3: ResizeArray<Env1>), (var_4: uint64)): EnvStack0 =
    let (var_5: Env1) = var_0.[0]
    let (var_6: EnvStack0) = var_5.mem_0
    let (var_7: uint64) = var_5.mem_1
    let (var_8: bool) = (var_4 <= var_7)
    let (var_44: Env1) =
        if var_8 then
            let (var_9: (uint64 ref)) = var_6.mem_0
            let (var_10: uint64) = (!var_9)
            let (var_11: uint64) = (var_10 + var_4)
            let (var_12: (uint64 ref)) = (ref var_11)
            let (var_13: EnvStack0) = EnvStack0((var_12: (uint64 ref)))
            let (var_14: uint64) = (var_7 - var_4)
            var_0.[0] <- (Env1(var_13, var_14))
            (Env1(var_6, var_4))
        else
            let (var_16: (Env1 -> (Env1 -> int32))) = method_3
            let (var_17: System.Comparison<Env1>) = System.Comparison<Env1>(var_16)
            var_0.Sort(var_17)
            let (var_18: Env1) = var_0.[0]
            let (var_19: EnvStack0) = var_18.mem_0
            let (var_20: uint64) = var_18.mem_1
            let (var_21: bool) = (var_4 <= var_20)
            if var_21 then
                let (var_22: (uint64 ref)) = var_19.mem_0
                let (var_23: uint64) = (!var_22)
                let (var_24: uint64) = (var_23 + var_4)
                let (var_25: (uint64 ref)) = (ref var_24)
                let (var_26: EnvStack0) = EnvStack0((var_25: (uint64 ref)))
                let (var_27: uint64) = (var_20 - var_4)
                var_0.[0] <- (Env1(var_26, var_27))
                (Env1(var_19, var_4))
            else
                method_1((var_0: ResizeArray<Env1>), (var_1: EnvStack0), (var_2: uint64), (var_3: ResizeArray<Env1>))
                let (var_29: (Env1 -> (Env1 -> int32))) = method_3
                let (var_30: System.Comparison<Env1>) = System.Comparison<Env1>(var_29)
                var_0.Sort(var_30)
                let (var_31: Env1) = var_0.[0]
                let (var_32: EnvStack0) = var_31.mem_0
                let (var_33: uint64) = var_31.mem_1
                let (var_34: bool) = (var_4 <= var_33)
                if var_34 then
                    let (var_35: (uint64 ref)) = var_32.mem_0
                    let (var_36: uint64) = (!var_35)
                    let (var_37: uint64) = (var_36 + var_4)
                    let (var_38: (uint64 ref)) = (ref var_37)
                    let (var_39: EnvStack0) = EnvStack0((var_38: (uint64 ref)))
                    let (var_40: uint64) = (var_33 - var_4)
                    var_0.[0] <- (Env1(var_39, var_40))
                    (Env1(var_32, var_4))
                else
                    (failwith "Out of memory in the designated section.")
    let (var_45: EnvStack0) = var_44.mem_0
    let (var_46: uint64) = var_44.mem_1
    var_45
and method_16((var_0: EnvStack2), (var_1: ResizeArray<EnvStack2>)): unit =
    let (var_2: (int64 ref)) = var_0.mem_0
    let (var_3: EnvStack0) = var_0.mem_1
    let (var_4: int64) = (!var_2)
    let (var_5: int64) = (var_4 + 1L)
    var_2 := var_5
    var_1.Add(var_0)
let (var_0: string) = cuda_kernels
let (var_1: ManagedCuda.CudaContext) = ManagedCuda.CudaContext(false)
var_1.Synchronize()
let (var_2: string) = System.Environment.get_CurrentDirectory()
let (var_3: string) = System.IO.Path.Combine(var_2, "nvcc_router.bat")
let (var_4: System.Diagnostics.ProcessStartInfo) = System.Diagnostics.ProcessStartInfo()
var_4.set_RedirectStandardOutput(true)
var_4.set_RedirectStandardError(true)
var_4.set_UseShellExecute(false)
var_4.set_FileName(var_3)
let (var_5: System.Diagnostics.Process) = System.Diagnostics.Process()
var_5.set_StartInfo(var_4)
let (var_7: (System.Diagnostics.DataReceivedEventArgs -> unit)) = method_0
var_5.OutputDataReceived.Add(var_7)
var_5.ErrorDataReceived.Add(var_7)
let (var_8: string) = System.IO.Path.Combine("C:/Program Files (x86)/Microsoft Visual Studio/2017/Community", "VC/Auxiliary/Build/vcvars64.bat")
let (var_9: string) = System.IO.Path.Combine("C:/Program Files (x86)/Microsoft Visual Studio/2017/Community", "VC/Tools/MSVC/14.11.25503/bin/Hostx64/x64")
let (var_10: string) = System.IO.Path.Combine("C:/Program Files/NVIDIA GPU Computing Toolkit/CUDA/v9.0", "include")
let (var_11: string) = System.IO.Path.Combine("C:/Program Files (x86)/Microsoft Visual Studio/2017/Community", "VC/Tools/MSVC/14.11.25503/include")
let (var_12: string) = System.IO.Path.Combine("C:/Program Files/NVIDIA GPU Computing Toolkit/CUDA/v9.0", "bin/nvcc.exe")
let (var_13: string) = System.IO.Path.Combine(var_2, "cuda_kernels.ptx")
let (var_14: string) = System.IO.Path.Combine(var_2, "cuda_kernels.cu")
let (var_15: bool) = System.IO.File.Exists(var_14)
if var_15 then
    System.IO.File.Delete(var_14)
else
    ()
System.IO.File.WriteAllText(var_14, var_0)
let (var_16: bool) = System.IO.File.Exists(var_3)
if var_16 then
    System.IO.File.Delete(var_3)
else
    ()
let (var_17: System.IO.FileStream) = System.IO.File.OpenWrite(var_3)
let (var_18: System.IO.StreamWriter) = System.IO.StreamWriter(var_17)
var_18.WriteLine("SETLOCAL")
let (var_19: string) = String.concat "" [|"CALL "; "\""; var_8; "\""|]
var_18.WriteLine(var_19)
let (var_20: string) = String.concat "" [|"SET PATH=%PATH%;"; "\""; var_9; "\""|]
var_18.WriteLine(var_20)
let (var_21: string) = String.concat "" [|"\""; var_12; "\" -gencode=arch=compute_52,code=\\\"sm_52,compute_52\\\" --use-local-env --cl-version 2017 -I\""; var_10; "\" -I\"C:/cub-1.7.4\" -I\""; var_11; "\" --keep-dir \""; var_2; "\" -maxrregcount=0  --machine 64 -ptx -cudart static  -o \""; var_13; "\" \""; var_14; "\""|]
var_18.WriteLine(var_21)
var_18.Dispose()
var_17.Dispose()
let (var_22: System.Diagnostics.Stopwatch) = System.Diagnostics.Stopwatch.StartNew()
let (var_23: bool) = var_5.Start()
let (var_24: bool) = (var_23 = false)
if var_24 then
    (failwith "NVCC failed to run.")
else
    ()
var_5.BeginOutputReadLine()
var_5.BeginErrorReadLine()
var_5.WaitForExit()
let (var_25: int32) = var_5.get_ExitCode()
let (var_26: bool) = (var_25 = 0)
let (var_27: bool) = (var_26 = false)
if var_27 then
    let (var_28: string) = System.String.Format("{0}",var_25)
    let (var_29: string) = String.concat ", " [|"NVCC failed compilation."; var_28|]
    let (var_30: string) = System.String.Format("[{0}]",var_29)
    (failwith var_30)
else
    ()
let (var_31: System.TimeSpan) = var_22.get_Elapsed()
printfn "The time it took to compile the Cuda kernels is: %A" var_31
let (var_32: ManagedCuda.BasicTypes.CUmodule) = var_1.LoadModulePTX(var_13)
var_5.Dispose()
let (var_33: string) = String.concat "" [|"Compiled the kernels into the following directory: "; var_2|]
let (var_34: string) = System.String.Format("{0}",var_33)
System.Console.WriteLine(var_34)
let (var_35: uint64) = 1048576UL
let (var_36: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_35)
let (var_37: ManagedCuda.BasicTypes.CUdeviceptr) = var_1.AllocateMemory(var_36)
let (var_38: uint64) = uint64 var_37
let (var_39: (uint64 ref)) = (ref var_38)
let (var_40: EnvStack0) = EnvStack0((var_39: (uint64 ref)))
let (var_41: ResizeArray<Env1>) = ResizeArray<Env1>()
let (var_42: ResizeArray<Env1>) = ResizeArray<Env1>()
method_1((var_41: ResizeArray<Env1>), (var_40: EnvStack0), (var_35: uint64), (var_42: ResizeArray<Env1>))
let (var_46: ResizeArray<EnvStack2>) = ResizeArray<EnvStack2>()
let (var_53: ResizeArray<EnvStack3>) = ResizeArray<EnvStack3>()
let (var_54: EnvStack3) = method_8((var_53: ResizeArray<EnvStack3>))
let (var_55: ManagedCuda.CudaRand.GeneratorType) = ManagedCuda.CudaRand.GeneratorType.PseudoDefault
let (var_56: ManagedCuda.CudaRand.CudaRandDevice) = ManagedCuda.CudaRand.CudaRandDevice(var_55)
let (var_57: ManagedCuda.CudaBlas.PointerMode) = ManagedCuda.CudaBlas.PointerMode.Host
let (var_58: ManagedCuda.CudaBlas.AtomicsMode) = ManagedCuda.CudaBlas.AtomicsMode.Allowed
let (var_59: ManagedCuda.CudaBlas.CudaBlas) = ManagedCuda.CudaBlas.CudaBlas(var_57, var_58)
let (var_60: ManagedCuda.CudaBlas.CudaBlasHandle) = var_59.get_CublasHandle()
let (var_61: (int64 [])) = Array.zeroCreate<int64> (System.Convert.ToInt32(8L))
let (var_62: (int64 [])) = Array.zeroCreate<int64> (System.Convert.ToInt32(8L))
let (var_63: int64) = 0L
method_10((var_61: (int64 [])), (var_62: (int64 [])), (var_63: int64))
let (var_64: (int64 [])) = Array.zeroCreate<int64> (System.Convert.ToInt32(64L))
let (var_65: (int64 [])) = Array.zeroCreate<int64> (System.Convert.ToInt32(64L))
let (var_66: int64) = 0L
method_11((var_64: (int64 [])), (var_65: (int64 [])), (var_66: int64))
let (var_67: int64) = 8L
let (var_68: int64) = 0L
let (var_69: int64) = 1L
let (var_70: EnvStack4) = method_13((var_1: ManagedCuda.CudaContext), (var_32: ManagedCuda.BasicTypes.CUmodule), (var_46: ResizeArray<EnvStack2>), (var_41: ResizeArray<Env1>), (var_40: EnvStack0), (var_35: uint64), (var_42: ResizeArray<Env1>), (var_54: EnvStack3), (var_67: int64), (var_61: (int64 [])), (var_68: int64), (var_69: int64))
let (var_71: int64) = 0L
let (var_72: int64) = 1L
let (var_73: EnvStack4) = method_13((var_1: ManagedCuda.CudaContext), (var_32: ManagedCuda.BasicTypes.CUmodule), (var_46: ResizeArray<EnvStack2>), (var_41: ResizeArray<Env1>), (var_40: EnvStack0), (var_35: uint64), (var_42: ResizeArray<Env1>), (var_54: EnvStack3), (var_67: int64), (var_62: (int64 [])), (var_71: int64), (var_72: int64))
let (var_74: int64) = 8L
let (var_75: int64) = 0L
let (var_76: int64) = 8L
let (var_77: int64) = 1L
let (var_78: EnvStack4) = method_17((var_1: ManagedCuda.CudaContext), (var_32: ManagedCuda.BasicTypes.CUmodule), (var_46: ResizeArray<EnvStack2>), (var_41: ResizeArray<Env1>), (var_40: EnvStack0), (var_35: uint64), (var_42: ResizeArray<Env1>), (var_54: EnvStack3), (var_74: int64), (var_64: (int64 [])), (var_75: int64), (var_76: int64), (var_77: int64))
let (var_79: int64) = 0L
let (var_80: int64) = 8L
let (var_81: int64) = 1L
let (var_82: EnvStack4) = method_17((var_1: ManagedCuda.CudaContext), (var_32: ManagedCuda.BasicTypes.CUmodule), (var_46: ResizeArray<EnvStack2>), (var_41: ResizeArray<Env1>), (var_40: EnvStack0), (var_35: uint64), (var_42: ResizeArray<Env1>), (var_54: EnvStack3), (var_74: int64), (var_65: (int64 [])), (var_79: int64), (var_80: int64), (var_81: int64))
var_59.Dispose()
var_56.Dispose()
method_18((var_53: ResizeArray<EnvStack3>))
method_20((var_46: ResizeArray<EnvStack2>))
let (var_83: (uint64 ref)) = var_40.mem_0
let (var_84: uint64) = method_5((var_83: (uint64 ref)))
let (var_85: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_84)
let (var_86: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_85)
var_1.FreeMemory(var_86)
var_83 := 0UL
var_1.Dispose()

