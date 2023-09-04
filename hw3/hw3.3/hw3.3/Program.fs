module LambdaInterpreter

type LambdaTerm = 
    | Variable of string
    | Application of LambdaTerm * LambdaTerm
    | LambdaAbstraction of string * LambdaTerm

let rec getFreeVars term =
    match term with
    | Variable(var) -> Set.singleton(var)
    | Application(a, b) -> Set.union (getFreeVars a) (getFreeVars b)
    | LambdaAbstraction(a, b) -> (getFreeVars b) |> Set.remove a

let substitute term old replacement = 
    let freeVars = getFreeVars replacement
    let rec substituteResursive term bound = 
        match term with
        | Variable(var) -> 
            if (var = old && not (Set.contains var bound)) then replacement
            elif (Set.contains var freeVars && Set.contains var bound) then Variable("'" + var)
            else Variable(var)
        | Application(a, b) -> Application(substituteResursive a bound, substituteResursive b bound)
        | LambdaAbstraction(a, b) -> 
            if (Set.contains a freeVars) then LambdaAbstraction("'" + a, substituteResursive b (Set.add a bound))
            else LambdaAbstraction(a, substituteResursive b (Set.add a bound))
    substituteResursive term Set.empty

let rec reduce term = 
    match term with
    | Variable(a) -> Variable(a)
    | Application(LambdaAbstraction(a, c), b) -> reduce (substitute c a b)
    | Application(a, b) -> Application(reduce a, reduce b)
    | LambdaAbstraction(var, l) -> LambdaAbstraction(var, reduce l)
