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
            if (var = old) then replacement
            elif (Set.contains var bound) then Variable("'" + var)
            else Variable(var)
        | Application(a, b) -> Application(substituteResursive a bound, substituteResursive b bound)
        | LambdaAbstraction(a, b) -> 
            if (Set.contains a freeVars) then LambdaAbstraction("'" + a, substituteResursive b (Set.add a bound))
            else LambdaAbstraction(a, substituteResursive b bound)
    substituteResursive term Set.empty

let rec reduce term = 
    match term with
    | Variable(a) -> Variable(a)
    | Application(LambdaAbstraction(a, c), b) -> reduce (substitute c a b)
    | Application(a, b) -> Application(reduce a, reduce b)
    | LambdaAbstraction(var, l) -> LambdaAbstraction(var, reduce l)

let tripleX = LambdaAbstraction("x", Application(Application(Variable("x"), Variable("x")), Variable("x")))
//let term = Application(LambdaAbstraction("x", Variable("y")), Application(tripleX, tripleX))
let term = Application(LambdaAbstraction("x", Application(Variable("x"), Variable("y"))), Application(Variable("x"), Variable("y")))
let result = reduce term
if (result = Variable("y")) then printfn "YES" else printfn "spdlaspo[dl"