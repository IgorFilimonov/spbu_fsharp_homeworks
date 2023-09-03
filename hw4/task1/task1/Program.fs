module BracketSequence

let isBracketSeqCorrect text =
    let openingBrackets = ['('; '['; '{']
    let mappedBrackets = Map [(')', '('); (']', '['); ('}', '{')]
    let rec loop index prevOpenings =
        if (index = String.length text) then
            true
        else
            match (openingBrackets |> List.contains(text[index])) with
            | true -> loop (index + 1) (text[index] :: prevOpenings)
            | false -> 
                match (mappedBrackets |> Map.tryFind(text[index])) with
                | Some bracket -> 
                    if (bracket <> prevOpenings.Head) then false
                    else loop (index + 1) prevOpenings.Tail
                | None -> loop (index + 1) prevOpenings
    loop 0 []
