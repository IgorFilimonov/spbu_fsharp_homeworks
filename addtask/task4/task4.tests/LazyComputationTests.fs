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

let testMultithreadedLazy (lazyComp: (unit -> 'a) -> ILazy<'a>) =
    let counter = ref 0
    let concreteLazy = lazyComp(fun () -> Interlocked.Increment(counter))
    let tasks = Seq.init 5 (fun _ -> async { return concreteLazy.Get() |> should equal 1 })
    tasks
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

[<Test>]
let LazyWithLockTest () =
    testMultithreadedLazy LazyFactory.LazyWithLock

[<Test>]
let LockFreeLazyTest () =
    testMultithreadedLazy LazyFactory.LockFreeLazy
