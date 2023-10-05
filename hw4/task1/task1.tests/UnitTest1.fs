module BracketSequence.tests

open NUnit.Framework
open FsUnit
open BracketSequence

[<Test>]
let Test1 () =
    let text = "(oqij)oqi[jeo]ifjw"
    let expected = true
    isBracketSeqCorrect text |> should equal expected

[<Test>]
let Test2 () =
    let text = "(2{1}2){2(3[4]5)1}"
    let expected = true
    isBracketSeqCorrect text |> should equal expected

[<Test>]
let Test3 () =
    let text = "{{)){}()]"
    let expected = false
    isBracketSeqCorrect text |> should equal expected

[<Test>]
let Test4 () =
    let text = "(oijoijweofj"
    let expected = false
    isBracketSeqCorrect text |> should equal expected