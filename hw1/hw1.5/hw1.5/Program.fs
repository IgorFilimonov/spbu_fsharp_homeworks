let rec findNumber number list index =
    match list with
    | [] -> None
    | head :: tail ->
        if (head = number) then
            Some index
        else
            findNumber number tail (index + 1)

printfn "Enter a list of numbers:"
let listOfStrings = System.Console.ReadLine().Split ' ' |> Array.toList
let list = List.map (fun elem -> elem |> int) listOfStrings
printfn "Enter a number:"
let number = System.Console.ReadLine() |> int
match (findNumber number list 0) with
| None -> printfn "There is no this number in the list"
| Some x -> printfn "First position of number in this list: %d" x