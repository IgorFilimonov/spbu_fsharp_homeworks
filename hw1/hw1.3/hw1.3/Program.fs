let rec reverseList list acc =
    match list with
    | [] -> acc
    | head :: tail -> reverseList tail (head :: acc)

printfn "Enter a list you want to reverse:"
let list = System.Console.ReadLine().Split ' ' |> Array.toList
printfn "Reversed list: %A" (reverseList list [])
