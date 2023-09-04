module NetWork.tests

open NUnit.Framework
open FsUnit
open NetWork

[<Test>]
let Test1 () =
    let os = OperationSystem
    let computer1 = Computer(OperationSystem.Windows, true, [])
    let computer2 = Computer("Linux", false, [])
    let computer3 = Computer("Windows", true, [])
    let computer4 = Computer("Linux", false, [])
    let computer5 = Computer("MacOS", false, [])
    computer1.Connections <- [computer2; computer5]
    computer2.Connections <- [computer3; computer4]
    computer3.Connections <- [computer4; computer5]
    let computers = [computer1; computer2; computer3; computer4; computer5]
    let probs = Map [("Windows", 0.9); ("Linux", 0.4); ("MacOS", 0.0); ("Others", 0.1)]
    let network = Network(computers, ProbabilityManager(probs))
    network.Run()
    let expected = [true; true; true; true; false]
    List.init 5 (fun index -> computers[index].IsInfected) |> should equal expected

[<Test>]
let testWithZeroProb () =
    let computer1 = Computer("Windows", true, [])
    let computer2 = Computer("Linux", false, [])
    let computer3 = Computer("Windows", true, [])
    let computer4 = Computer("Linux", false, [])
    let computer5 = Computer("MacOS", false, [])
    computer1.Connections <- [computer2; computer5]
    computer2.Connections <- [computer3; computer4]
    computer3.Connections <- [computer4; computer5]
    let computers = [computer1; computer2; computer3; computer4; computer5]
    let probs = Map [("Windows", 0.0); ("Linux", 0.0); ("MacOS", 0.0); ("Others", 0.0)]
    let network = Network(computers, ProbabilityManager(probs))
    network.Run()
    let expected = [true; false; true; false; false]
    List.init 5 (fun index -> computers[index].IsInfected) |> should equal expected

[<Test>]
let testWithOneProb () =
    let computer1 = Computer("Windows", true, [])
    let computer2 = Computer("Linux", false, [])
    let computer3 = Computer("Windows", true, [])
    let computer4 = Computer("Linux", false, [])
    let computer5 = Computer("MacOS", false, [])
    computer1.Connections <- [computer2; computer5]
    computer2.Connections <- [computer3; computer4]
    computer3.Connections <- [computer4; computer5]
    let computers = [computer1; computer2; computer3; computer4; computer5]
    let probs = Map [("Windows", 1.0); ("Linux", 1.0); ("MacOS", 1.0); ("Others", 1.0)]
    let network = Network(computers, ProbabilityManager(probs))
    network.Run()
    let expected = [true; true; true; true; true]
    List.init 5 (fun index -> computers[index].IsInfected) |> should equal expected
