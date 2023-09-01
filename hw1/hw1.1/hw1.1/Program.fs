let factorial number =
    let rec tailRecursiveFactorial number acc =
        if number <= 1 then
            acc
        else
            tailRecursiveFactorial (number - 1) (acc * number)
    tailRecursiveFactorial number 1

printfn "Enter a number you want to get a factorial of:"
let number = System.Console.ReadLine() |> int
if (number < 0) then
    printfn "There's no factorial of a negative number"
else
    printfn "Factorial of %d = %d" number (factorial number)