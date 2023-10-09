module StringComputation

open System

type StringComputationBuilder() =
    let parse (s: string) =
        match Int32.TryParse s with
        | true, value -> Some value
        | _ -> None
    member _.Bind(value, func) =
        match parse value with
        | None -> None
        | Some value -> func value
    member _.Return(value) = Some value
