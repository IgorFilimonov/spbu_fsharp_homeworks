module StringComputation.tests

open NUnit.Framework
open FsUnit
open StringComputation

[<Test>]
let ``"1" + "2" = Some 3`` () =
    let calculate = StringComputationBuilder()
    let result =
        calculate {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        }
    let expectedResult = Some 3
    result |> should equal expectedResult

[<Test>]
let ``"1" + "Ú" = None`` () =
    let calculate = StringComputationBuilder()
    let result =
        calculate {
            let! x = "1"
            let! y = "Ú"
            let z = x + y
            return z
        }
    let expectedResult = None
    result |> should equal expectedResult
