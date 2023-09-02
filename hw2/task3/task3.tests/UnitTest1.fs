module ExpressionTree.tests

open NUnit.Framework
open FsUnit
open ExpressionTree

[<Test>]
let Test1 () =
    let tree = Tree(Addition, Tip(1), Tip(2))
    let expected = 3
    evaluate tree |> should equal expected

[<Test>]
let Test2 () =
    let tree = Tree(Addition, Tree(Division, Tip(2), Tip(5)), Tree(Subtraction, Tip(3), Tip(4)))
    let expected = -0.6
    evaluate tree |> should equal expected
