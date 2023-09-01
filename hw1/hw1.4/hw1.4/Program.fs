let rec getListOfPowersOf2 n m acc =
    if (m = -1) then acc
    else 
        match acc with
        | [] -> getListOfPowersOf2 n (m - 1) (2.0 ** ((n + m) |> float) :: acc)
        | head :: tail -> getListOfPowersOf2 n (m - 1) ((head / 2.0) :: acc)

printfn "Enter n and m:"
let input = System.Console.ReadLine().Split ' '
let n = input[0] |> int
let m = input[1] |> int
printfn "Required list: %A" (getListOfPowersOf2 n m [])
