module StringComputation

open System

type StringComputationBuilder() =
    let parse (s: string) =
        match Int32.TryParse s with
        | true, value -> Some value
        | _ -> None
    member this.Bind(value, func) =
        match parse value with
        | None -> None
        | Some value -> func value
    member this.Return(value) = Some value
