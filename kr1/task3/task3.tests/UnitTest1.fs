module QueueWithPriority.tests

open NUnit.Framework
open FsUnit
open QueueWithPriority

[<Test>]
let addTest () =
    let queue = PriorityQueue()
    queue.Insert(2)
    queue.Insert(10)
    let expected = [2; 10]
    queue.Values |> List.sort |> should equal expected

[<Test>]
let extractTest () =
    let queue = PriorityQueue()
    queue.Insert(10)
    queue.Insert(20)
    queue.Insert(30)
    queue.ExtractMin()
    let expected = [20; 30]
    queue.Values |> List.sort |> should equal expected
