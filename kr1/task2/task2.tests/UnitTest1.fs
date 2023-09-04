module PrintSquare.tests

open NUnit.Framework
open FsUnit
open PrintSquare

[<Test>]
let square0Test () =
    let square = getCharSquare 0
    let expected: string list = []
    square |> should equal expected

[<Test>]
let square1Test() =
    let square = getCharSquare 1
    let expected = ["*"]
    square |> should equal expected

[<Test>]
let square4Test() =
    let square = getCharSquare 4
    let expected = ["****"; "*  *"; "*  *"; "****"]
    square |> should equal expected
