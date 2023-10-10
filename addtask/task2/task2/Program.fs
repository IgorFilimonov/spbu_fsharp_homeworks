module ParkingSimulation

open System.Threading
open System

type Parking(size: int) =
    let mutable parkedCars = 0

    member _.GetCarsAmount() = parkedCars

    member _.CheckAndAdd() = 
        let mutable answer = false
        let mutable isDone = false
        while not isDone do 
            let current = parkedCars
            if current = size then
                isDone <- true
            else
                let incremented = current + 1
                if Interlocked.CompareExchange(&parkedCars, incremented, current) = current then
                    isDone <- true
                    answer <- true
        answer

    member _.CheckAndDelete(): Boolean =
        let mutable answer = false
        let mutable isDone = false
        while not isDone do
            let current = parkedCars
            if current = 0 then
                isDone <- true
            else
                let decremented = current - 1
                if Interlocked.CompareExchange(&parkedCars, decremented, current) = current then
                    isDone <- true
                    answer <- true
        answer
