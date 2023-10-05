module GetMin.tests

open NUnit.Framework
open FsUnit
open GetMin
open System

[<Test>]
let Test1 () =
    let list = [1; 3; 2; -1; 10]
    let expected = -1
    getMin list |> should equal expected

[<Test>]
let Test2 () =
    let list = [10; -10; 20; -30]
    let expected = -30
    getMin list |> should equal expected

[<Test>]
let minTest () =
    let list: int list = [Int32.MinValue]
    let expected = Int32.MinValue
    getMin list |> should equal expected

[<Test>]
let maxTest () =
    let list = [Int32.MaxValue]
    let expected = Int32.MaxValue
    getMin list |> should equal expected
