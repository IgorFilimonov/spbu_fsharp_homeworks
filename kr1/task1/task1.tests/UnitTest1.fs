module GetMin.tests

open NUnit.Framework
open FsUnit
open GetMin

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
