namespace Lasy

open System.Threading

type ILazy<'a> =
    abstract member Get: unit -> 'a

type SingleThreadLazy<'a>(supplier: unit -> 'a) =
    let mutable value = None
    interface ILazy<'a> with
        member this.Get() =
            match value with
            | Some v -> v
            | None ->
                let result = supplier()
                value <- Some result
                result


type MultiThreadLazy<'a>(supplier: unit -> 'a) =
    let mutable value = None
    let lockObj = obj()
    interface ILazy<'a> with
        member this.Get() =
            lock lockObj (fun () ->
                match value with
                | Some v -> v
                | None ->
                    let result = supplier()
                    value <- Some result
                    result)


type LockFreeLazy<'a>(supplier: unit -> 'a) =
    let mutable value = None : Option<'a>
    let mutable isInitialized = 0
    interface ILazy<'a> with
        member this.Get() =
            if Interlocked.CompareExchange(&isInitialized, 1, 0) = 0 then
                let result = supplier()
                value <- Some result
                result
            else
                while value.IsNone do
                    Thread.SpinWait(1)
                value.Value