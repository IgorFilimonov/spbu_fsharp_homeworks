module TreeMap.tests

open NUnit.Framework
open FsUnit
open TreeMap

[<Test>]
let Test1 () =
    let tree = Tree(2, Tip(3), Tip(4))
    let func x = x * x
    let expected = Tree(4, Tip(9), Tip(16))
    treeMap tree func |> should equal expected

[<Test>]
let test2 () =
    let tree = Tree(1, Tree(2, Tip(4), Tip(3)), Tip(5))
    let func x = x + 1
    let expected = Tree(2, Tree(3, Tip(5), Tip(4)), Tip(6))
    treeMap tree func |> should equal expected
