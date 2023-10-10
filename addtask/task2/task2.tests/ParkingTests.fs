module ParkingSimulation.tests

open NUnit.Framework
open ParkingSimulation
open System.Threading
open FsUnit

[<Test>]
let AmountOfSpacesAlwaysCorrect () =
    let testParking = Parking(100)

    let amountOfSpacesAlwaysCorrectTemplate func expected =
        let cde = new CountdownEvent(200)
        let addingTasks = Seq.init 200 (fun _ -> async { 
            func |> ignore
            cde.Signal() |> ignore })
        addingTasks
        |> Async.Parallel
        |> Async.RunSynchronously
        |> ignore
        cde.Wait()
        testParking.GetCarsAmount() |> should equal expected

    amountOfSpacesAlwaysCorrectTemplate testParking.CheckAndAdd 100

    amountOfSpacesAlwaysCorrectTemplate testParking.CheckAndDelete 0
