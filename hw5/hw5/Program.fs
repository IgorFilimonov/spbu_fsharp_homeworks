module NetWork

type Computer(os: , isInfected, connections: Computer list) = 
    let mutable mIsInfected = isInfected
    member val OS = os with get
    member _.IsInfected
        with get() = mIsInfected
        and set x = mIsInfected <- x
    member val Connections = connections with get, set

type ProbabilityManager(probs: Map<string, float>) =
    member _.Random = System.Random()
    member this.ApplyProbability(computer: Computer) =
        let randomNumber = this.Random.NextDouble()
        let osProbability = this.GetProbability(computer)
        (0.0 < randomNumber && randomNumber <= osProbability)
    member _.GetProbability(computer: Computer) =
        match Map.tryFind computer.OS probs with
        | Some x -> x
        | None -> Map.find "Others" probs

type OperationSystem = 
    | Windows = 1
    | Linux = 2
    | MacOS = 3
    | Others = 4

type Network (computers: Computer list, probManager: ProbabilityManager) =
    let rec run infected =
        let rec getNewInfected (infected: Computer list) acc =
            match infected with
            | [] -> acc
            | h :: t -> 
                let candidatesToGetInfected = h.Connections |> List.filter (fun computer -> not computer.IsInfected)
                let rec loop (candidates: Computer list) (newInfected: Computer list) keepParent =
                    match candidates with
                    | [] -> (newInfected, keepParent)
                    | h :: t -> 
                        if (probManager.ApplyProbability(h)) then loop t (h :: newInfected) keepParent
                        elif (probManager.GetProbability(h) = 0) then loop t newInfected keepParent
                        else loop t newInfected true
                let (newInfected, keepParent) = loop candidatesToGetInfected [] false
                newInfected |> List.iter (fun computer -> computer.IsInfected <- true)
                if (keepParent) then
                    getNewInfected t (h :: newInfected @ acc)
                else
                    getNewInfected t (newInfected @ acc)
                            
        let newInfected = getNewInfected infected []
        Network.PrintState(computers)
        if (newInfected <> []) then
            run newInfected            

    member _.Run() =
        let infected = computers |> List.filter (fun computer -> computer.IsInfected)
        run infected

    static member PrintState(computers: Computer list) =
        computers |> List.iter (fun computer -> printf "%s " (if computer.IsInfected then "Infected" else "NotInfected"))
        printfn ""
