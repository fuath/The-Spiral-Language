﻿module Learning.Experimental.Tests

open Spiral.Lib
open Spiral.Tests
open System.IO
open Spiral.Types
open Learning.Cuda

let cfg = Spiral.Types.cfg_default

let relative_ng1 =
    "relative_ng1",[cuda_modules;learning;mnist],"Does the full training work with Mnist using relative natural gradient?",
    """
inb s = CudaModules (1024*1024*1024)

inl float = float32
open Learning float

inl minibatch_size = 128
inl { test_images test_labels train_images train_labels} =
    inl mnist_path = @"C:\ML Datasets\Mnist"
    Mnist.load_mnist_tensors mnist_path
    |> s.CudaTensor.from_host_tensors
    |> module_map (inl _ x -> x.round_split' minibatch_size)

inl input_size = 784
inl hidden_size = 10

inl network = 
    open Feedforward.Layer

    inl label = input .label hidden_size
    inl network =
        input .input input_size 
        |> rng {size=256; lr=0.3f32}
        |> linear {size=hidden_size; lr=0.3f32}
        |> init s
    inl train = error Error.softmax_cross_entropy label network
    inl test = parallel (train, accuracy label network)
    {train test}

Loops.for' {from=0; near_to=10;body=inl {next} -> 
    open Feedforward.Pass
    open Body

    inl cost =
        for {
            data={input=train_images; label=train_labels}
            body=train { network=network.train }
            } s

    Console.printfn "Training: {0}" cost

    if nan_is cost then
        Console.writeline "Training diverged. Aborting..."
    else
        inl cost, ac, max_ac =
            for {
                data={input=test_images; label=test_labels}
                body=test { network=network.test }
                } s 

        Console.printfn "Testing: {0}({1}/{2})" (cost, ac, max_ac)
        next ()
    }
    """

//output_test_to_temp cfg (Path.Combine(__SOURCE_DIRECTORY__ , @"..\Temporary\output.fs")) relative_ng1
//|> printfn "%s"
//|> ignore
