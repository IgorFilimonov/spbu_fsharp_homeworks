module MinDist.tests

open NUnit.Framework
open FsUnit
open MinDist

[<Test>]
let testWithTree () =
    let tree = Tree(1, Tree(2, Tip(3), Tip(4)), Tip(5))
    let expected = 1
    getMinDist tree |> should equal expected

[<Test>]
let testWithTip () =
    let tree = Tip(1)
    let expected = 0
    getMinDist tree |> should equal expected
