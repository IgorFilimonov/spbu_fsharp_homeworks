let factorial number =
    if (number < 0) then None
    else 
        let rec tailRecursiveFactorial number acc =
            if number <= 1 then
                acc
            else
                tailRecursiveFactorial (number - 1) (acc * number)
        Some (tailRecursiveFactorial number 1)

printfn "Enter a number you want to get a factorial of:"
let number = System.Console.ReadLine() |> int
match factorial number with
| Some x -> printfn "Factorial of %d = %d" number x
| None -> printfn "There's no factorial of a negative number"
