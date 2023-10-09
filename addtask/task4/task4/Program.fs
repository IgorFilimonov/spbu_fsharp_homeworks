module LazyComputation

open System.Threading

type ILazy<'a> =
    abstract member Get: unit -> 'a

type Lazy<'a>(supplier) =
    let mutable result = None
    interface ILazy<'a> with
        member _.Get() =
            if (result.IsNone) then result <- Some(supplier())
            result.Value

type LazyWithLock<'a>(supplier) =
    [<VolatileField>]
    let mutable result = None
    let locker = obj()
    interface ILazy<'a> with
        member _.Get() =
            if result.IsNone then lock locker (fun () ->
                if result.IsNone then result <- Some(supplier())
                result.Value)
            else
                result.Value

type LockFreeLazy<'a>(supplier) =
    let mutable result = None
    interface ILazy<'a> with
        member _.Get() =
            if result.IsNone then
                let value = Some(supplier())
                Interlocked.CompareExchange(&result, value, None) |> ignore
            result.Value

type LazyFactory =
    static member Lazy supplier = Lazy<'a>(supplier) :> ILazy<'a>
    static member LazyWithLock supplier = LazyWithLock<'a>(supplier) :> ILazy<'a>
    static member LockFreeLazy supplier = LockFreeLazy<'a>(supplier) :> ILazy<'a>
