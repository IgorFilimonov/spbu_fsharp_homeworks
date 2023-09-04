module QueueWithPriority

type PriorityQueue() =
    member val Values: int list = [] with get, set
    member this.Insert value =
        this.Values <- (value :: this.Values)
    member this.ExtractMin() =
        if (this.Values = []) then
            failwith "Queue is empty"
        let min = this.Values |> List.min // got minimum, now we need to delete it from queue
        let rec deleteFromList value list acc =
            match list with
            | [] -> acc
            | head :: tail ->
                if (head = value) then
                    acc @ tail
                else
                    deleteFromList value tail (head :: acc)
        this.Values <- deleteFromList min this.Values []
