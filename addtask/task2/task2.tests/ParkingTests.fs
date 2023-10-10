module ParkingSimulation.tests

open NUnit.Framework
open ParkingSimulation
open System.Threading
open FsUnit

[<Test>]
let Test1 () =
    let testParking = Parking()
    let cde = new CountdownEvent(10000)
    let addingTasks = Seq.init 10000 (fun _ -> async { 
        testParking.AddAndGetSize()
        cde.Signal() })
    addingTasks
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore
    cde.Wait()
    testParking.GetSize() |> should equal 10000
    let cde1 = new CountdownEvent(5000)
    let mama = Seq.init 5000 (fun _ -> async {
        testParking.checkAndDelete() |> should equal true
        cde1.Signal() |> ignore})
    mama
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore
    cde1.Wait()
    testParking.GetSize() |> should equal 5000
