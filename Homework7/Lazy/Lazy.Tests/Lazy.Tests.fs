module LazyTests

open System
open System.Threading
open NUnit.Framework
open FsUnit
open Lasy


[<TestFixture>]
type LazyTests() =

    let supplier () =
        printfn "Computing value"
        42

    [<Test>]
    member _.``SingleThreadLazy should compute value only once``() =
        let l = SingleThreadLazy(supplier) :> ILazy<_>
        l.Get() |> should equal 42
        l.Get() |> should equal 42

    [<Test>]
    member _.``MultiThreadLazy should compute value only once``() =
        let l = MultiThreadLazy(supplier) :> ILazy<_>
        let results = Array.init 10 (fun _ -> l.Get())
        results |> Array.forall ((=) 42) |> should equal true

    [<Test>]
    member _.``LockFreeLazy should compute value only once``() =
        let l = LockFreeLazy(supplier) :> ILazy<_>
        let results = Array.init 10 (fun _ -> l.Get())
        results |> Array.forall ((=) 42) |> should equal true

    [<Test>]
    member _.``Concurrency test for LockFreeLazy``() =
        let l = LockFreeLazy(supplier) :> ILazy<_>
        let results = Array.zeroCreate 10
        let threads =
            [| for i in 0..9 ->
                Thread(ThreadStart(fun () -> results.[i] <- l.Get())) |]
        threads |> Array.iter (fun t -> t.Start())
        threads |> Array.iter (fun t -> t.Join())
        results |> Array.forall ((=) 42) |> should equal true

    [<Test>]
    member _.``SingleThreadLazy should work correctly with different values``() =
        let supplier1 () = 1
        let supplier2 () = "test"
        let lazyInt = SingleThreadLazy(supplier1) :> ILazy<_>
        let lazyString = SingleThreadLazy(supplier2) :> ILazy<_>
        lazyInt.Get() |> should equal 1
        lazyString.Get() |> should equal "test"

    [<Test>]
    member _.``MultiThreadLazy should work correctly with different values``() =
        let supplier1 () = 1
        let supplier2 () = "test"
        let lazyInt = MultiThreadLazy(supplier1) :> ILazy<_>
        let lazyString = MultiThreadLazy(supplier2) :> ILazy<_>
        let resultsInt = Array.init 10 (fun _ -> lazyInt.Get())
        let resultsString = Array.init 10 (fun _ -> lazyString.Get())
        resultsInt |> Array.forall ((=) 1) |> should equal true
        resultsString |> Array.forall ((=) "test") |> should equal true

    [<Test>]
    member _.``LockFreeLazy should work correctly with different values``() =
        let supplier1 () = 1
        let supplier2 () = "test"
        let lazyInt = LockFreeLazy(supplier1) :> ILazy<_>
        let lazyString = LockFreeLazy(supplier2) :> ILazy<_>
        let resultsInt = Array.init 10 (fun _ -> lazyInt.Get())
        let resultsString = Array.init 10 (fun _ -> lazyString.Get())
        resultsInt |> Array.forall ((=) 1) |> should equal true
        resultsString |> Array.forall ((=) "test") |> should equal true