module QueueWithPriority.tests

open NUnit.Framework
open FsUnit
open QueueWithPriority

[<Test>]
let addTest () =
    let queue = PriorityQueue()
    queue.Insert((2, 10))
    queue.Insert((10, 20))
    let expected = [(2, 10); (10, 20)]
    queue.Values |> List.sort |> should equal expected

[<Test>]
let extractTest () =
    let queue = PriorityQueue()
    queue.Insert(10, 30)
    queue.Insert(20, 20)
    queue.Insert(30, 10)
    queue.ExtractMin()
    let expected = [(20, 20); (30, 10)]
    queue.Values |> List.sort |> should equal expected

[<Test>]
let testWithRepeates () =
    let queue = PriorityQueue()
    queue.Insert(20, 20)
    queue.Insert(10, 30)
    queue.Insert(30, 10)
    queue.Insert(10, 30)
    queue.ExtractMin()
    let expected = [(10, 30); (20, 20); (30, 10)]
    queue.Values |> List.sort |> should equal expected
