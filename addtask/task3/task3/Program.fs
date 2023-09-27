module MinDist

type Tree<'a> =
    | Tree of 'a * Tree<'a> * Tree<'a>
    | Tip of 'a

let rec getMinDist tree =
    match tree with
    | Tip _ -> 0
    | Tree (_, left, right) -> (min (getMinDist left) (getMinDist right)) + 1
