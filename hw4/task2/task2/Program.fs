﻿open FsCheck

let startingFunc x l = List.map (fun y -> y * x) l
let func1 x = List.map (fun y -> y * x) // -> (fun y -> (*) x y)
let func2 x = List.map ((*) x)
let func3 = (*) >> List.map

let checkEquality x l = startingFunc x l = func3 x l
Check.QuickThrowOnFailure checkEquality
