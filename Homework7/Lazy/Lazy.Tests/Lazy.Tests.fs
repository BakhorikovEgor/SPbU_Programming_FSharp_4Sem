module LazyTests

open NUnit.Framework
open Lasy
open System.Threading
open FsUnit


let allRealizations  = 
    [ (fun l -> SingleThreadLazy l :> obj ILazy)
      (fun l -> MultiThreadLazy l :> obj ILazy)
      (fun l -> LockFreeLazy l :> obj ILazy) ]
    |> List.map (fun a -> TestCaseData(a))

let multiThreadLazy =
    [ (fun f -> MultiThreadLazy f :> obj ILazy)
      (fun f -> LockFreeLazy f :> obj ILazy) ]
    |> List.map (fun a -> TestCaseData(a))





[<TestCaseSource("allRealizations")>]
[<Repeat(20)>]
let CounterShouldBeIncrementedOnce (lazySupplier: (unit -> obj) -> obj ILazy) =
    let counter = ref 0
    let supplier = fun _ -> Interlocked.Increment(counter) 
                            obj()

    let l = lazySupplier supplier

    Seq.initInfinite (fun _ -> l.Get())
    |> Seq.take 100
    |> Seq.distinct
    |> Seq.length |> should equal 1

    counter.Value |> should equal 1


[<TestCaseSource("multiThreadLazy")>]
[<Repeat(20)>]
let  CounterShouldReturnSameValueOnceMultiThread (lazySupplier: (unit -> obj) -> obj ILazy) =
    let counter = ref 0
    let supplier = fun _ -> Interlocked.Increment(counter) 
                            obj()

    let l = lazySupplier supplier

    Seq.initInfinite (fun _ -> async { return l.Get()})
    |> Seq.take 100
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.distinct
    |> Seq.length |> should equal 1


[<Test>]
[<Repeat(20)>]
let  MultiThreadLazyShouldUseSupplierOnce () =
    let counter = ref 0
    let supplier = fun _ -> Interlocked.Increment(counter) 
                            obj()

    let l = MultiThreadLazy supplier :> obj ILazy

    Seq.initInfinite (fun _ -> async { return l.Get()})
    |> Seq.take 100
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.distinct
    |> Seq.length |> should equal 1

    counter.Value |> should equal 1