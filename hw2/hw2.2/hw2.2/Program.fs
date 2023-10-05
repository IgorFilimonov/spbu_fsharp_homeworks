module TreeMap

type BinaryTree<'a> =
    | Tree of 'a * BinaryTree<'a> * BinaryTree<'a>
    | Tip of 'a

let rec treeMap tree func =
    match tree with
    | Tree(elem, subTree1, subTree2) -> Tree(func elem, treeMap subTree1 func, treeMap subTree2 func)
    | Tip elem -> Tip(func elem)
