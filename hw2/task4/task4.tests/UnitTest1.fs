module PrimeSequence.tests

open NUnit.Framework
open FsUnit
open PrimeSequence

[<Test>]
let Test1 () =
    let expected = [2; 3; 5; 7; 11; 13; 17; 19; 23; 29]
    getSeqOfPrimes() |> Seq.take 10 |> Seq.toList |> should equal expected
