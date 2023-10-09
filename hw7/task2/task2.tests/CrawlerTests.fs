module Crawler.tests

open NUnit.Framework
open FsUnit
open Crawler
open System

[<Test>]
let SeSiteTest () =
    let url = "https://se.math.spbu.ru/practice"
    let expected = [
        Some ("https://oops.math.spbu.ru/SE/alumni", 49175)
        Some ("https://oops.math.spbu.ru/SE/alumni", 49175)
    ]
    getPagesInfo url
    |> (Async.RunSynchronously)
    |> Option.get
    |> (Async.RunSynchronously)
    |> Seq.toList
    |> should equal expected

[<Test>]
let IncorrectLinkTest () =
    (fun () -> getPagesInfo "incorrect" |> Async.RunSynchronously |> ignore)
    |> should throw typeof<InvalidOperationException>
