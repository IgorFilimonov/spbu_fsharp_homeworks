module ParkingSimulation

open System.Threading
open System

type Parking() =
    let mutable size = 0
    member _.GetSize() = size
    member _.AddAndGetSize() = Interlocked.Increment(&size)
    member _.checkAndDelete(): Boolean =
        let mutable answer = false
        let mutable counter = 0
        while counter = 0 do
            let current = size
            if current = 0 then
                counter <- 1
            else
                let decremented = current - 1
                if Interlocked.CompareExchange(&size, decremented, current) = current then
                    counter <- 1
                    answer <- true
        answer
