type Computer(os, isInfected, connections: Computer list) = 
    let mutable mIsInfected = isInfected
    member val OS = os with get
    member _.IsInfected
        with get() = mIsInfected
        and set x = mIsInfected <- x
    member val Connections = connections with get

type ProbabilityManager(probs: Map<string, float>) =
    member _.Random = System.Random()
    member this.ApplyProbability(computer: Computer) =
        let randomNumber = this.Random.NextDouble()
        let osProbability = this.GetProbability(computer)
        if (0.0 < randomNumber && randomNumber <= osProbability) then
            true
        else
            false
    member _.GetProbability(computer: Computer) =
        match Map.tryFind computer.OS probs with
        | Some x -> x
        | None -> Map.find "Others" probs

type Network (computers: Computer list, probManager: ProbabilityManager) =
    let rec run infected =
        let rec getNewInfected (infected: Computer list, acc) =
            match infected with
            | [] -> acc
            | h :: t -> 
                let conditionToGetInfected (computer: Computer) =
                    not computer.IsInfected && probManager.ApplyProbability(computer)
                let infectedNeighbours = h.Connections |> List.filter (fun computer -> conditionToGetInfected(computer)) // not infected yet
                infectedNeighbours |> List.iter (fun computer -> computer.IsInfected <- true) // now they're infected
                getNewInfected (t, acc @ infectedNeighbours)
        let newInfected = getNewInfected (infected, [])
        if (newInfected = []) then
            let checkIfAllNeighboursAreImmune (comp: Computer) = comp.Connections 
                                                                 |> List.fold (fun acc comp -> 
                                                                 acc && probManager.GetProbability(comp) = 0) true
            let isExpansionOver = infected |> List.fold (fun acc comp -> acc && checkIfAllNeighboursAreImmune(comp) = true) true
            

    member _.Run =
        let infected = computers |> List.filter (fun computer -> computer.IsInfected)
        run infected

    member _.PrintState =
        computers |> List.iter (fun computer -> printf "%s " (if computer.IsInfected then "Infected" else "NotInfected"))
        printfn ""

let test = System.Random()
for i in [1..5] do printfn "%f" (test.NextDouble())
let test1 = Map [("1", "2")]
printfn "%s" (Map.find "1" test1)
