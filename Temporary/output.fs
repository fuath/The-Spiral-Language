module SpiralExample.Main
let cuda_kernels = """
#include "cub/cub.cuh"

extern "C" {
    struct Env0 {
        long long int mem_0;
    };
    __device__ __forceinline__ Env0 make_Env0(long long int mem_0){
        Env0 tmp;
        tmp.mem_0 = mem_0;
        return tmp;
    }
    struct Tuple2 {
        float mem_0;
        char mem_1;
        long long int mem_2;
    };
    __device__ __forceinline__ Tuple2 make_Tuple2(float mem_0, char mem_1, long long int mem_2){
        Tuple2 tmp;
        tmp.mem_0 = mem_0;
        tmp.mem_1 = mem_1;
        tmp.mem_2 = mem_2;
        return tmp;
    }
    struct Tuple3 {
        float mem_0;
        Tuple2 mem_1;
    };
    __device__ __forceinline__ Tuple3 make_Tuple3(float mem_0, Tuple2 mem_1){
        Tuple3 tmp;
        tmp.mem_0 = mem_0;
        tmp.mem_1 = mem_1;
        return tmp;
    }
    struct Env1 {
        long long int mem_0;
        Tuple3 mem_1;
    };
    __device__ __forceinline__ Env1 make_Env1(long long int mem_0, Tuple3 mem_1){
        Env1 tmp;
        tmp.mem_0 = mem_0;
        tmp.mem_1 = mem_1;
        return tmp;
    }
    struct Tuple6 {
        float mem_0;
        float mem_1;
    };
    __device__ __forceinline__ Tuple6 make_Tuple6(float mem_0, float mem_1){
        Tuple6 tmp;
        tmp.mem_0 = mem_0;
        tmp.mem_1 = mem_1;
        return tmp;
    }
    typedef float(*FunPointer4)(float, float);
    struct Tuple7 {
        Tuple2 mem_0;
        Tuple2 mem_1;
    };
    __device__ __forceinline__ Tuple7 make_Tuple7(Tuple2 mem_0, Tuple2 mem_1){
        Tuple7 tmp;
        tmp.mem_0 = mem_0;
        tmp.mem_1 = mem_1;
        return tmp;
    }
    typedef Tuple2(*FunPointer5)(Tuple2, Tuple2);
    __global__ void method_5(float * var_0, float * var_1, float * var_2, long long int * var_3);
    __device__ char method_6(Env0 * var_0);
    __device__ char method_7(Env1 * var_0);
    __device__ float method_8(float var_0, float var_1);
    __device__ Tuple2 method_9(Tuple2 var_0, Tuple2 var_1);
    
    __global__ void method_5(float * var_0, float * var_1, float * var_2, long long int * var_3) {
        long long int var_4 = threadIdx.x;
        long long int var_5 = threadIdx.y;
        long long int var_6 = threadIdx.z;
        long long int var_7 = blockIdx.x;
        long long int var_8 = blockIdx.y;
        long long int var_9 = blockIdx.z;
        long long int var_10 = (var_5 + var_8);
        Env0 var_11[1];
        var_11[0] = (make_Env0(var_10));
        while (method_6(var_11)) {
            Env0 var_13 = var_11[0];
            long long int var_14 = var_13.mem_0;
            long long int var_15 = (var_14 + 6);
            char var_16 = (var_14 >= 0);
            char var_18;
            if (var_16) {
                var_18 = (var_14 < 6);
            } else {
                var_18 = 0;
            }
            char var_19 = (var_18 == 0);
            if (var_19) {
                // "Argument out of bounds."
            } else {
            }
            long long int var_20 = (var_14 * 10);
            char var_22;
            if (var_16) {
                var_22 = (var_14 < 6);
            } else {
                var_22 = 0;
            }
            char var_23 = (var_22 == 0);
            if (var_23) {
                // "Argument out of bounds."
            } else {
            }
            float var_24 = var_1[var_14];
            long long int var_25 = (var_4 + var_7);
            float var_26 = -1000;
            float var_27 = __int_as_float(0xff800000);
            char var_28 = 0;
            long long int var_29 = 0;
            Env1 var_30[1];
            var_30[0] = (make_Env1(var_25, make_Tuple3(var_26, make_Tuple2(var_27, var_28, var_29))));
            while (method_7(var_30)) {
                Env1 var_32 = var_30[0];
                long long int var_33 = var_32.mem_0;
                Tuple3 var_34 = var_32.mem_1;
                float var_35 = var_34.mem_0;
                Tuple2 var_36 = var_34.mem_1;
                long long int var_37 = (var_33 + 1);
                float var_38 = var_36.mem_0;
                char var_39 = var_36.mem_1;
                long long int var_40 = var_36.mem_2;
                char var_41 = (var_33 >= 0);
                char var_43;
                if (var_41) {
                    var_43 = (var_33 < 10);
                } else {
                    var_43 = 0;
                }
                char var_44 = (var_43 == 0);
                if (var_44) {
                    // "Argument out of bounds."
                } else {
                }
                long long int var_45 = (var_20 + var_33);
                float var_46 = var_0[var_45];
                float var_47[1];
                var_47[0] = var_46;
                float var_48[1];
                float var_49[1];
                float var_50 = var_49[0];
                FunPointer4 var_53 = method_8;
                cub::BlockScan<float,1>().InclusiveScan(var_47, var_48, var_53, var_50);
                float var_54 = var_48[0];
                char var_55 = (var_54 < var_24);
                FunPointer5 var_58 = method_9;
                Tuple2 var_59 = cub::BlockReduce<Tuple2,1>().Reduce(make_Tuple2(var_54, var_55, var_33), var_58);
                float var_60 = var_59.mem_0;
                char var_61 = var_59.mem_1;
                long long int var_62 = var_59.mem_2;
                char var_63 = (var_39 == 1);
                Tuple2 var_68;
                if (var_63) {
                    char var_64 = (var_61 == 1);
                    if (var_64) {
                        char var_65 = (var_38 < var_60);
                        if (var_65) {
                            var_68 = make_Tuple2(var_60, var_61, var_62);
                        } else {
                            var_68 = make_Tuple2(var_38, var_39, var_40);
                        }
                    } else {
                        var_68 = make_Tuple2(var_38, var_39, var_40);
                    }
                } else {
                    var_68 = make_Tuple2(var_60, var_61, var_62);
                }
                float var_69 = var_68.mem_0;
                char var_70 = var_68.mem_1;
                long long int var_71 = var_68.mem_2;
                var_30[0] = (make_Env1(var_37, make_Tuple3(var_50, make_Tuple2(var_69, var_70, var_71))));
            }
            Env1 var_72 = var_30[0];
            long long int var_73 = var_72.mem_0;
            Tuple3 var_74 = var_72.mem_1;
            float var_75 = var_74.mem_0;
            Tuple2 var_76 = var_74.mem_1;
            float var_77 = var_76.mem_0;
            char var_78 = var_76.mem_1;
            long long int var_79 = var_76.mem_2;
            char var_80 = (var_4 == 0);
            if (var_80) {
                char var_82;
                if (var_16) {
                    var_82 = (var_14 < 6);
                } else {
                    var_82 = 0;
                }
                char var_83 = (var_82 == 0);
                if (var_83) {
                    // "Argument out of bounds."
                } else {
                }
                float var_84 = var_2[var_14];
                long long int var_85 = var_3[var_14];
                var_2[var_14] = var_77;
                var_3[var_14] = var_79;
            } else {
            }
            var_11[0] = (make_Env0(var_15));
        }
        Env0 var_86 = var_11[0];
        long long int var_87 = var_86.mem_0;
    }
    __device__ char method_6(Env0 * var_0) {
        Env0 var_1 = var_0[0];
        long long int var_2 = var_1.mem_0;
        return (var_2 < 6);
    }
    __device__ char method_7(Env1 * var_0) {
        Env1 var_1 = var_0[0];
        long long int var_2 = var_1.mem_0;
        Tuple3 var_3 = var_1.mem_1;
        float var_4 = var_3.mem_0;
        Tuple2 var_5 = var_3.mem_1;
        return (var_2 < 10);
    }
    __device__ float method_8(float var_0, float var_1) {
        return var_1;
    }
    __device__ Tuple2 method_9(Tuple2 var_0, Tuple2 var_1) {
        float var_2 = var_0.mem_0;
        char var_3 = var_0.mem_1;
        long long int var_4 = var_0.mem_2;
        float var_5 = var_1.mem_0;
        char var_6 = var_1.mem_1;
        long long int var_7 = var_1.mem_2;
        char var_8 = (var_3 == 1);
        Tuple2 var_13;
        if (var_8) {
            char var_9 = (var_6 == 1);
            if (var_9) {
                char var_10 = (var_2 < var_5);
                if (var_10) {
                    var_13 = make_Tuple2(var_5, var_6, var_7);
                } else {
                    var_13 = make_Tuple2(var_2, var_3, var_4);
                }
            } else {
                var_13 = make_Tuple2(var_2, var_3, var_4);
            }
        } else {
            var_13 = make_Tuple2(var_5, var_6, var_7);
        }
        float var_14 = var_13.mem_0;
        char var_15 = var_13.mem_1;
        long long int var_16 = var_13.mem_2;
        return make_Tuple2(var_14, var_15, var_16);
    }
}
"""

type Union0 =
    | Union0Case0 of Tuple1
    | Union0Case1
and Tuple1 =
    struct
    val mem_0: ManagedCuda.BasicTypes.CUdeviceptr
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and EnvStack2 =
    struct
    val mem_0: (Union0 ref)
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and Env3 =
    struct
    val mem_0: EnvStack2
    val mem_1: int64
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
let rec method_0 ((var_0: System.Diagnostics.DataReceivedEventArgs)): unit =
    let (var_1: string) = var_0.get_Data()
    let (var_2: string) = System.String.Format("{0}",var_1)
    System.Console.WriteLine(var_2)
and method_1((var_0: (Union0 ref))): ManagedCuda.BasicTypes.CUdeviceptr =
    let (var_1: Union0) = (!var_0)
    match var_1 with
    | Union0Case0(var_2) ->
        var_2.mem_0
    | Union0Case1 ->
        (failwith "A Cuda memory cell that has been disposed has been tried to be accessed.")
and method_2((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env3>), (var_2: uint64), (var_3: int64)): EnvStack2 =
    let (var_4: int32) = var_1.get_Count()
    let (var_5: bool) = (var_4 > 0)
    if var_5 then
        let (var_6: Env3) = var_1.Peek()
        let (var_7: EnvStack2) = var_6.mem_0
        let (var_8: int64) = var_6.mem_1
        let (var_9: (Union0 ref)) = var_7.mem_0
        let (var_10: Union0) = (!var_9)
        match var_10 with
        | Union0Case0(var_11) ->
            let (var_12: ManagedCuda.BasicTypes.CUdeviceptr) = var_11.mem_0
            method_3((var_12: ManagedCuda.BasicTypes.CUdeviceptr), (var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env3>), (var_7: EnvStack2), (var_8: int64))
        | Union0Case1 ->
            let (var_14: Env3) = var_1.Pop()
            let (var_15: EnvStack2) = var_14.mem_0
            let (var_16: int64) = var_14.mem_1
            method_2((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env3>), (var_2: uint64), (var_3: int64))
    else
        method_4((var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env3>))
and method_10((var_0: ManagedCuda.CudaContext), (var_1: ManagedCuda.CudaStream), (var_2: uint64), (var_3: uint64), (var_4: System.Collections.Generic.Stack<Env3>), (var_5: EnvStack2), (var_6: int64), (var_7: int64), (var_8: int64), (var_9: int64), (var_10: int64), (var_11: int64), (var_12: int64), (var_13: int64)): unit =
    let (var_14: int64) = (var_11 - var_10)
    let (var_15: int64) = (var_13 - var_12)
    let (var_16: int64) = (var_14 * var_15)
    let (var_17: bool) = (var_10 < var_11)
    let (var_18: bool) = (var_17 = false)
    if var_18 then
        (failwith "Tensor needs to be at least size 1.")
    else
        ()
    let (var_19: bool) = (var_12 < var_13)
    let (var_20: bool) = (var_19 = false)
    if var_20 then
        (failwith "Tensor needs to be at least size 1.")
    else
        ()
    let (var_21: bool) = (0L = var_7)
    let (var_22: bool) = (var_21 = false)
    if var_22 then
        (failwith "The inner dimensions much have offsets of 0. They must not be 'view'ed. Consider reshaping a copy of the tensor instead")
    else
        ()
    let (var_23: bool) = (var_6 = 0L)
    let (var_24: bool) = (var_23 = false)
    if var_24 then
        (failwith "Only unviewed arrays are allowed for now.")
    else
        ()
    let (var_25: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(var_16))
    let (var_26: (Union0 ref)) = var_5.mem_0
    let (var_27: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_26: (Union0 ref)))
    var_0.CopyToHost(var_25, var_27)
    let (var_28: System.Text.StringBuilder) = System.Text.StringBuilder()
    let (var_29: string) = ""
    let (var_30: int64) = 0L
    method_11((var_28: System.Text.StringBuilder), (var_30: int64))
    let (var_31: System.Text.StringBuilder) = var_28.AppendLine("[|")
    method_12((var_28: System.Text.StringBuilder), (var_29: string), (var_25: (float32 [])), (var_6: int64), (var_7: int64), (var_8: int64), (var_9: int64), (var_10: int64), (var_11: int64), (var_12: int64), (var_13: int64))
    let (var_32: int64) = 0L
    method_11((var_28: System.Text.StringBuilder), (var_32: int64))
    let (var_33: System.Text.StringBuilder) = var_28.AppendLine("|]")
    let (var_34: string) = var_28.ToString()
    let (var_35: string) = System.String.Format("{0}",var_34)
    System.Console.WriteLine(var_35)
and method_17((var_0: ManagedCuda.CudaContext), (var_1: ManagedCuda.CudaStream), (var_2: uint64), (var_3: uint64), (var_4: System.Collections.Generic.Stack<Env3>), (var_5: EnvStack2), (var_6: int64), (var_7: int64), (var_8: EnvStack2), (var_9: int64), (var_10: int64), (var_11: int64), (var_12: int64)): unit =
    let (var_13: int64) = (var_12 - var_11)
    let (var_14: bool) = (var_11 < var_12)
    let (var_15: bool) = (var_14 = false)
    if var_15 then
        (failwith "Tensor needs to be at least size 1.")
    else
        ()
    let (var_16: bool) = (var_6 = 0L)
    let (var_17: bool) = (var_16 = false)
    if var_17 then
        (failwith "Only unviewed arrays are allowed for now.")
    else
        ()
    let (var_18: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(var_13))
    let (var_19: (Union0 ref)) = var_5.mem_0
    let (var_20: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_19: (Union0 ref)))
    var_0.CopyToHost(var_18, var_20)
    let (var_21: bool) = (var_9 = 0L)
    let (var_22: bool) = (var_21 = false)
    if var_22 then
        (failwith "Only unviewed arrays are allowed for now.")
    else
        ()
    let (var_23: (int64 [])) = Array.zeroCreate<int64> (System.Convert.ToInt32(var_13))
    let (var_24: (Union0 ref)) = var_8.mem_0
    let (var_25: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_24: (Union0 ref)))
    var_0.CopyToHost(var_23, var_25)
    let (var_26: System.Text.StringBuilder) = System.Text.StringBuilder()
    let (var_27: string) = ""
    let (var_28: int64) = 0L
    method_11((var_26: System.Text.StringBuilder), (var_28: int64))
    let (var_29: System.Text.StringBuilder) = var_26.Append("[|")
    let (var_30: string) = method_18((var_26: System.Text.StringBuilder), (var_18: (float32 [])), (var_6: int64), (var_7: int64), (var_23: (int64 [])), (var_9: int64), (var_10: int64), (var_11: int64), (var_12: int64), (var_27: string))
    let (var_31: System.Text.StringBuilder) = var_26.AppendLine("|]")
    let (var_32: string) = var_26.ToString()
    let (var_33: string) = System.String.Format("{0}",var_32)
    System.Console.WriteLine(var_33)
and method_3((var_0: ManagedCuda.BasicTypes.CUdeviceptr), (var_1: uint64), (var_2: uint64), (var_3: int64), (var_4: System.Collections.Generic.Stack<Env3>), (var_5: EnvStack2), (var_6: int64)): EnvStack2 =
    let (var_7: ManagedCuda.BasicTypes.SizeT) = var_0.Pointer
    let (var_8: uint64) = uint64 var_7
    let (var_9: uint64) = uint64 var_6
    let (var_10: int64) = (var_3 % 256L)
    let (var_11: int64) = (var_3 - var_10)
    let (var_12: int64) = (var_11 + 256L)
    let (var_13: uint64) = (var_8 + var_9)
    let (var_14: uint64) = (var_1 + var_2)
    let (var_15: uint64) = uint64 var_12
    let (var_16: uint64) = (var_14 - var_13)
    let (var_17: bool) = (var_15 <= var_16)
    let (var_18: bool) = (var_17 = false)
    if var_18 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_19: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_13)
    let (var_20: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_19)
    let (var_21: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_20))))
    let (var_22: EnvStack2) = EnvStack2((var_21: (Union0 ref)))
    var_4.Push((Env3(var_22, var_12)))
    var_22
and method_4((var_0: uint64), (var_1: uint64), (var_2: int64), (var_3: System.Collections.Generic.Stack<Env3>)): EnvStack2 =
    let (var_4: int64) = (var_2 % 256L)
    let (var_5: int64) = (var_2 - var_4)
    let (var_6: int64) = (var_5 + 256L)
    let (var_7: uint64) = (var_0 + var_1)
    let (var_8: uint64) = uint64 var_6
    let (var_9: uint64) = (var_7 - var_0)
    let (var_10: bool) = (var_8 <= var_9)
    let (var_11: bool) = (var_10 = false)
    if var_11 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_12: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_0)
    let (var_13: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_12)
    let (var_14: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_13))))
    let (var_15: EnvStack2) = EnvStack2((var_14: (Union0 ref)))
    var_3.Push((Env3(var_15, var_6)))
    var_15
and method_11((var_0: System.Text.StringBuilder), (var_1: int64)): unit =
    let (var_2: bool) = (var_1 < 0L)
    if var_2 then
        let (var_3: System.Text.StringBuilder) = var_0.Append(' ')
        let (var_4: int64) = (var_1 + 1L)
        method_11((var_0: System.Text.StringBuilder), (var_4: int64))
    else
        ()
and method_12((var_0: System.Text.StringBuilder), (var_1: string), (var_2: (float32 [])), (var_3: int64), (var_4: int64), (var_5: int64), (var_6: int64), (var_7: int64), (var_8: int64), (var_9: int64), (var_10: int64)): unit =
    let (var_11: bool) = (var_7 < var_8)
    if var_11 then
        let (var_12: bool) = (var_7 >= var_7)
        let (var_13: bool) = (var_12 = false)
        if var_13 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_14: int64) = (var_3 + var_4)
        let (var_15: int64) = 0L
        method_13((var_0: System.Text.StringBuilder), (var_15: int64))
        let (var_16: System.Text.StringBuilder) = var_0.Append("[|")
        let (var_17: string) = method_14((var_0: System.Text.StringBuilder), (var_2: (float32 [])), (var_14: int64), (var_6: int64), (var_9: int64), (var_10: int64), (var_1: string))
        let (var_18: System.Text.StringBuilder) = var_0.AppendLine("|]")
        let (var_19: int64) = (var_7 + 1L)
        method_16((var_0: System.Text.StringBuilder), (var_1: string), (var_2: (float32 [])), (var_3: int64), (var_4: int64), (var_5: int64), (var_6: int64), (var_7: int64), (var_8: int64), (var_9: int64), (var_10: int64), (var_19: int64))
    else
        ()
and method_18((var_0: System.Text.StringBuilder), (var_1: (float32 [])), (var_2: int64), (var_3: int64), (var_4: (int64 [])), (var_5: int64), (var_6: int64), (var_7: int64), (var_8: int64), (var_9: string)): string =
    let (var_10: bool) = (var_7 < var_8)
    if var_10 then
        let (var_11: System.Text.StringBuilder) = var_0.Append(var_9)
        let (var_12: bool) = (var_7 >= var_7)
        let (var_13: bool) = (var_12 = false)
        if var_13 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_14: float32) = var_1.[int32 var_2]
        let (var_15: int64) = var_4.[int32 var_5]
        let (var_16: string) = System.String.Format("{0}",var_15)
        let (var_17: string) = System.String.Format("{0}",var_14)
        let (var_18: string) = String.concat ", " [|var_17; var_16|]
        let (var_19: string) = System.String.Format("[{0}]",var_18)
        let (var_20: System.Text.StringBuilder) = var_0.Append(var_19)
        let (var_21: string) = "; "
        let (var_22: int64) = (var_7 + 1L)
        method_19((var_0: System.Text.StringBuilder), (var_1: (float32 [])), (var_2: int64), (var_3: int64), (var_4: (int64 [])), (var_5: int64), (var_6: int64), (var_7: int64), (var_8: int64), (var_21: string), (var_22: int64))
    else
        var_9
and method_13((var_0: System.Text.StringBuilder), (var_1: int64)): unit =
    let (var_2: bool) = (var_1 < 4L)
    if var_2 then
        let (var_3: System.Text.StringBuilder) = var_0.Append(' ')
        let (var_4: int64) = (var_1 + 1L)
        method_13((var_0: System.Text.StringBuilder), (var_4: int64))
    else
        ()
and method_14((var_0: System.Text.StringBuilder), (var_1: (float32 [])), (var_2: int64), (var_3: int64), (var_4: int64), (var_5: int64), (var_6: string)): string =
    let (var_7: bool) = (var_4 < var_5)
    if var_7 then
        let (var_8: System.Text.StringBuilder) = var_0.Append(var_6)
        let (var_9: bool) = (var_4 >= var_4)
        let (var_10: bool) = (var_9 = false)
        if var_10 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_11: float32) = var_1.[int32 var_2]
        let (var_12: string) = System.String.Format("{0}",var_11)
        let (var_13: System.Text.StringBuilder) = var_0.Append(var_12)
        let (var_14: string) = "; "
        let (var_15: int64) = (var_4 + 1L)
        method_15((var_0: System.Text.StringBuilder), (var_1: (float32 [])), (var_2: int64), (var_3: int64), (var_4: int64), (var_5: int64), (var_14: string), (var_15: int64))
    else
        var_6
and method_16((var_0: System.Text.StringBuilder), (var_1: string), (var_2: (float32 [])), (var_3: int64), (var_4: int64), (var_5: int64), (var_6: int64), (var_7: int64), (var_8: int64), (var_9: int64), (var_10: int64), (var_11: int64)): unit =
    let (var_12: bool) = (var_11 < var_8)
    if var_12 then
        let (var_13: bool) = (var_11 >= var_7)
        let (var_14: bool) = (var_13 = false)
        if var_14 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_15: int64) = (var_11 - var_7)
        let (var_16: int64) = (var_15 * var_5)
        let (var_17: int64) = (var_3 + var_16)
        let (var_18: int64) = (var_17 + var_4)
        let (var_19: int64) = 0L
        method_13((var_0: System.Text.StringBuilder), (var_19: int64))
        let (var_20: System.Text.StringBuilder) = var_0.Append("[|")
        let (var_21: string) = method_14((var_0: System.Text.StringBuilder), (var_2: (float32 [])), (var_18: int64), (var_6: int64), (var_9: int64), (var_10: int64), (var_1: string))
        let (var_22: System.Text.StringBuilder) = var_0.AppendLine("|]")
        let (var_23: int64) = (var_11 + 1L)
        method_16((var_0: System.Text.StringBuilder), (var_1: string), (var_2: (float32 [])), (var_3: int64), (var_4: int64), (var_5: int64), (var_6: int64), (var_7: int64), (var_8: int64), (var_9: int64), (var_10: int64), (var_23: int64))
    else
        ()
and method_19((var_0: System.Text.StringBuilder), (var_1: (float32 [])), (var_2: int64), (var_3: int64), (var_4: (int64 [])), (var_5: int64), (var_6: int64), (var_7: int64), (var_8: int64), (var_9: string), (var_10: int64)): string =
    let (var_11: bool) = (var_10 < var_8)
    if var_11 then
        let (var_12: System.Text.StringBuilder) = var_0.Append(var_9)
        let (var_13: bool) = (var_10 >= var_7)
        let (var_14: bool) = (var_13 = false)
        if var_14 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_15: int64) = (var_10 - var_7)
        let (var_16: int64) = (var_15 * var_3)
        let (var_17: int64) = (var_2 + var_16)
        let (var_18: int64) = (var_15 * var_6)
        let (var_19: int64) = (var_5 + var_18)
        let (var_20: float32) = var_1.[int32 var_17]
        let (var_21: int64) = var_4.[int32 var_19]
        let (var_22: string) = System.String.Format("{0}",var_21)
        let (var_23: string) = System.String.Format("{0}",var_20)
        let (var_24: string) = String.concat ", " [|var_23; var_22|]
        let (var_25: string) = System.String.Format("[{0}]",var_24)
        let (var_26: System.Text.StringBuilder) = var_0.Append(var_25)
        let (var_27: string) = "; "
        let (var_28: int64) = (var_10 + 1L)
        method_19((var_0: System.Text.StringBuilder), (var_1: (float32 [])), (var_2: int64), (var_3: int64), (var_4: (int64 [])), (var_5: int64), (var_6: int64), (var_7: int64), (var_8: int64), (var_27: string), (var_28: int64))
    else
        var_9
and method_15((var_0: System.Text.StringBuilder), (var_1: (float32 [])), (var_2: int64), (var_3: int64), (var_4: int64), (var_5: int64), (var_6: string), (var_7: int64)): string =
    let (var_8: bool) = (var_7 < var_5)
    if var_8 then
        let (var_9: System.Text.StringBuilder) = var_0.Append(var_6)
        let (var_10: bool) = (var_7 >= var_4)
        let (var_11: bool) = (var_10 = false)
        if var_11 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_12: int64) = (var_7 - var_4)
        let (var_13: int64) = (var_12 * var_3)
        let (var_14: int64) = (var_2 + var_13)
        let (var_15: float32) = var_1.[int32 var_14]
        let (var_16: string) = System.String.Format("{0}",var_15)
        let (var_17: System.Text.StringBuilder) = var_0.Append(var_16)
        let (var_18: string) = "; "
        let (var_19: int64) = (var_7 + 1L)
        method_15((var_0: System.Text.StringBuilder), (var_1: (float32 [])), (var_2: int64), (var_3: int64), (var_4: int64), (var_5: int64), (var_18: string), (var_19: int64))
    else
        var_6
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
let (var_35: ManagedCuda.CudaDeviceProperties) = var_1.GetDeviceInfo()
let (var_36: ManagedCuda.BasicTypes.SizeT) = var_35.get_TotalGlobalMemory()
let (var_37: int64) = int64 var_36
let (var_38: float) = float var_37
let (var_39: float) = (0.100000 * var_38)
let (var_40: int64) = int64 var_39
let (var_41: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_40)
let (var_42: ManagedCuda.BasicTypes.CUdeviceptr) = var_1.AllocateMemory(var_41)
let (var_43: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_42))))
let (var_44: EnvStack2) = EnvStack2((var_43: (Union0 ref)))
let (var_45: System.Collections.Generic.Stack<Env3>) = System.Collections.Generic.Stack<Env3>()
let (var_46: (Union0 ref)) = var_44.mem_0
let (var_47: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_46: (Union0 ref)))
let (var_48: ManagedCuda.BasicTypes.SizeT) = var_47.Pointer
let (var_49: uint64) = uint64 var_48
let (var_50: uint64) = uint64 var_40
let (var_51: ManagedCuda.CudaStream) = ManagedCuda.CudaStream()
let (var_52: ManagedCuda.CudaRand.GeneratorType) = ManagedCuda.CudaRand.GeneratorType.PseudoDefault
let (var_53: ManagedCuda.CudaRand.CudaRandDevice) = ManagedCuda.CudaRand.CudaRandDevice(var_52)
let (var_54: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
var_53.SetStream(var_54)
let (var_55: int64) = 240L
let (var_56: EnvStack2) = method_2((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_55: int64))
let (var_57: (Union0 ref)) = var_56.mem_0
let (var_58: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_57: (Union0 ref)))
let (var_59: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(60L)
var_53.GenerateNormal32(var_58, var_59, 0.000000f, 1.000000f)
let (var_60: int64) = 24L
let (var_61: EnvStack2) = method_2((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_60: int64))
let (var_62: (Union0 ref)) = var_61.mem_0
let (var_63: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_62: (Union0 ref)))
let (var_64: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(6L)
var_53.GenerateNormal32(var_63, var_64, 0.000000f, 0.000000f)
let (var_66: int64) = 24L
let (var_67: EnvStack2) = method_2((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_66: int64))
let (var_68: int64) = 48L
let (var_69: EnvStack2) = method_2((var_49: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_50: uint64), (var_68: int64))
let (var_70: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_57: (Union0 ref)))
let (var_71: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_62: (Union0 ref)))
let (var_72: (Union0 ref)) = var_67.mem_0
let (var_73: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_72: (Union0 ref)))
let (var_74: (Union0 ref)) = var_69.mem_0
let (var_75: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_74: (Union0 ref)))
// Cuda join point
// method_5((var_70: ManagedCuda.BasicTypes.CUdeviceptr), (var_71: ManagedCuda.BasicTypes.CUdeviceptr), (var_73: ManagedCuda.BasicTypes.CUdeviceptr), (var_75: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_76: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_5", var_32, var_1)
let (var_77: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 6u, 1u)
var_76.set_GridDimensions(var_77)
let (var_78: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_76.set_BlockDimensions(var_78)
let (var_79: ManagedCuda.BasicTypes.CUstream) = var_51.get_Stream()
let (var_81: (System.Object [])) = [|var_70; var_71; var_73; var_75|]: (System.Object [])
var_76.RunAsync(var_79, var_81)
let (var_82: int64) = 0L
let (var_83: int64) = 0L
let (var_84: int64) = 10L
let (var_85: int64) = 1L
let (var_86: int64) = 0L
let (var_87: int64) = 6L
let (var_88: int64) = 0L
let (var_89: int64) = 10L
method_10((var_1: ManagedCuda.CudaContext), (var_51: ManagedCuda.CudaStream), (var_49: uint64), (var_50: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_56: EnvStack2), (var_82: int64), (var_83: int64), (var_84: int64), (var_85: int64), (var_86: int64), (var_87: int64), (var_88: int64), (var_89: int64))
let (var_90: int64) = 0L
let (var_91: int64) = 1L
let (var_92: int64) = 0L
let (var_93: int64) = 1L
let (var_94: int64) = 0L
let (var_95: int64) = 6L
method_17((var_1: ManagedCuda.CudaContext), (var_51: ManagedCuda.CudaStream), (var_49: uint64), (var_50: uint64), (var_45: System.Collections.Generic.Stack<Env3>), (var_67: EnvStack2), (var_90: int64), (var_91: int64), (var_69: EnvStack2), (var_92: int64), (var_93: int64), (var_94: int64), (var_95: int64))
var_72 := Union0Case1
var_74 := Union0Case1
var_62 := Union0Case1
var_57 := Union0Case1
var_53.Dispose()
var_51.Dispose()
let (var_96: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_46: (Union0 ref)))
var_1.FreeMemory(var_96)
var_46 := Union0Case1
var_1.Dispose()

