module LazyComputation.tests

open NUnit.Framework
open FsUnit
open LazyComputation
open System.Threading

[<Test>]
let LazyTest () =
    let mutable counter = 0
    let supplier = fun () -> 
        counter <- counter + 1
        counter
    let simpleLazy = LazyFactory.Lazy(supplier)
    let result1 = simpleLazy.Get()
    let result2 = simpleLazy.Get()
    (result1, result2) |> should equal (1, 1)

[<Test>]
let LazyWithLockTest () =
    let counter = ref 0
    let lazyWithLock = LazyFactory.LazyWithLock(fun () -> Interlocked.Increment(counter))
    let tasks = Seq.init 5 (fun _ -> async { return lazyWithLock.Get() |> should equal 1 })
    tasks
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

[<Test>]
let LockFreeLazyTest () =
    let counter = ref 0
    let lockFreeLazy = LazyFactory.LockFreeLazy(fun () -> Interlocked.Increment(counter))
    let tasks = Seq.init 5 (fun _ -> async { return lockFreeLazy.Get() |> should equal 1 })
    tasks
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore
