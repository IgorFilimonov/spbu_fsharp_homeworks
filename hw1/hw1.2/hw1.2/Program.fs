let fibonacci index =
    let rec fibonacciRecursive iterationsLeft prev1 prev2 =
        match iterationsLeft with
        | 0 -> prev1 + prev2
        | _ -> fibonacciRecursive (iterationsLeft - 1) prev2 (prev1 + prev2)
    match index with
    | 0 -> 0
    | 1 -> 1
    | _ -> fibonacciRecursive (index - 2) 0 1

printfn "Enter an index you want a Fibonacci number of:"
let index = System.Console.ReadLine() |> int
if (index < 0) then
    printfn "Index has to be not less than 0"
else
    printfn "Your number: %d" (fibonacci index)
