module ExpressionTree

type Operation =
    | Addition
    | Subtraction
    | Multiplication
    | Division

type ExpressionTree =
    | Tree of Operation * ExpressionTree * ExpressionTree
    | Tip of float

let rec evaluate tree =
    match tree with
    | Tree(operation, tree1, tree2) ->
        match operation with
        | Addition -> (evaluate tree1) + (evaluate tree2)
        | Subtraction -> (evaluate tree1) - (evaluate tree2)
        | Multiplication -> (evaluate tree1) * (evaluate tree2)
        | Division -> (evaluate tree1) / (evaluate tree2)
    | Tip value -> value
