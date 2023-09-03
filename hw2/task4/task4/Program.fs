module PrimeSequence

let isPrime number =
    let rec isPrimeRecursive divider =
        if (divider * divider > number) then
            true
        elif (number % divider = 0) then
            false
        else
            isPrimeRecursive (divider + 1)
    isPrimeRecursive 2

let findPrimeByIndex index =
    let rec findPrimeByIndexRecursive leftToFind current =
        if (isPrime current = true) then
            if (leftToFind = 1) then
                current
            else
                findPrimeByIndexRecursive (leftToFind - 1) (current + 1)
        else findPrimeByIndexRecursive leftToFind (current + 1)
    findPrimeByIndexRecursive (index + 1) 2

let getSeqOfPrimes() = Seq.initInfinite (fun index -> findPrimeByIndex index)
printfn "%A" (getSeqOfPrimes() |> Seq.take 10 |> Seq.toList)
