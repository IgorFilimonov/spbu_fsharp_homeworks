module ComputeWithPrecision

open System

type ComputationBuilder(precision: int) =
    let round (value: float) = Math.Round(value, precision)
    member _.Bind(value, func) = func (round value)
    member _.Return(value) = round value
