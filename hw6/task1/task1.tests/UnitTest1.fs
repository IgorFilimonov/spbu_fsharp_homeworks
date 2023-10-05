module ComputeWithPrecision.tests

open NUnit.Framework
open FsUnit
open ComputeWithPrecision

[<Test>]
let ``2.0 / 12.0 / 3.5 is approximately 0.048`` () =
    let rounding = ComputationBuilder
    let result = 
        rounding 3 {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        }
    let expectedResult = 0.048
    result |> should equal expectedResult
