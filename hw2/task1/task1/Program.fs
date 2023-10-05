module CountEven

let countEven1 list = List.map (fun value -> if (value % 2 = 0) then 1 else 0) list |> List.sum

let countEven2 list = (List.filter (fun value -> value % 2 = 0) list) |> List.length

let countEven3 list = List.fold (fun acc value -> if value % 2 = 0 then acc + 1 else acc) 0 list
