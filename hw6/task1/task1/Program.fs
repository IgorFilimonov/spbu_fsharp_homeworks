module ComputeWithPrecision

open System

type ComputationBuilder(precision: int) =
    let round (value: float) = Math.Round(value, precision)
    member this.Bind(value, func) = func (round value)
    member this.Return(value) = round value
