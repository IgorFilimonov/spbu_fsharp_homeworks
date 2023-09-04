module LambdaInterpreter.tests

open NUnit.Framework
open FsUnit
open LambdaInterpreter

[<Test>]
let Test () =
    let tripleX = LambdaAbstraction("x", Application(Application(Variable("x"), Variable("x")), Variable("x")))
    let term = Application(LambdaAbstraction("x", Variable("y")), Application(tripleX, tripleX))
    let expected = Variable("y")
    reduce term |> should equal expected

[<Test>]
let TestWithAlphaConversion () =
    let term = Application(LambdaAbstraction("x", LambdaAbstraction("y", Application(Variable("x"), Variable("y")))), Variable("y"))
    let expected = LambdaAbstraction("'y", Application(Variable("y"), Variable("'y")))
    reduce term |> should equal expected

[<Test>]
let Test1 () =
    let term = Application(LambdaAbstraction("x", LambdaAbstraction("x", Variable("x"))), Variable("y"))
    let expected = LambdaAbstraction("x", Variable("x"))
    reduce term |> should equal expected
