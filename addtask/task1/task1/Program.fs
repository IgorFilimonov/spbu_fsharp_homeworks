open FsCheck

let getTail l =
    match l with
        | [] -> []
        | _ -> List.tail l

let startingFunc g l = List.map g (getTail l)
let func1 g = getTail >> List.map g
let func2 g = (>>) (List.map g) getTail
let func3: (int -> int) -> 'a list -> 'b list = ((>>) getTail) << List.map

let checkEquality g l = startingFunc g l = func3 g l
Check.QuickThrowOnFailure checkEquality
