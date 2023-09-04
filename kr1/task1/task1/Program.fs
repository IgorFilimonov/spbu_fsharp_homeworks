module GetMin

let getMin list =
    let minusList = list |> List.fold (fun acc value -> -value :: acc) []
    -(minusList |> List.max)
