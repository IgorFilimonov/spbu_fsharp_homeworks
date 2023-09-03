module CountEven.tests

open NUnit.Framework
open FsUnit
open FsCheck
open CountEven

[<Test>]
let checkEqualityTest () =
    let checkEquality (list: List<int>) = (countEven1 list = countEven2 list) && (countEven2 list = countEven3 list)
    Check.QuickThrowOnFailure checkEquality    

[<Test>]
let countEven1Test () =
    let list = [1; 2; 3]
    countEven1 list |> should equal 1

[<Test>]
let countEven2Test () =
    let list = [0; 0; 3; 4]
    countEven2 list |> should equal 3

[<Test>]
let countEven3Test() =
    let list = [6; 6; -2; 0]
    countEven3 list |> should equal 4
