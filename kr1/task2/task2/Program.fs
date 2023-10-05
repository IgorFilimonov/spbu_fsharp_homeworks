module PrintSquare

let charListToString list =
    list |> List.fold (fun acc symbol -> acc + string symbol) ""

let getCharSquare sideLenght =
    let rec printSquareRec index acc =
        if (index < sideLenght) then
            if (index = 0 || index = sideLenght - 1) then
                let line = List.init sideLenght (fun _ -> '*') |> charListToString
                printSquareRec (index + 1) (line :: acc)
            else
                let line = List.init sideLenght (fun index -> if (index = 0 || index = sideLenght - 1) then '*' else ' ') |>
                           charListToString
                printSquareRec (index + 1) (line :: acc)
        else
            acc
    printSquareRec 0 []

let printCharSquare square =
    square |> List.iter (fun line -> printfn "%s" line)
