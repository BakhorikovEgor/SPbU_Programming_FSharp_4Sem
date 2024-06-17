module LocalNetwork

type OS(name: string, infectionProbability: int) =
    member val Name = name with get
    member val InfectionProbability = infectionProbability with get


type PC(os : OS, initiallyInfected: bool) =
    member val OS = os with get
    member val IsInfected = initiallyInfected with get, set


type Network(computers: PC[], adjacencyMatrix: bool[,]) =
    member val Computers = computers with get
    member val AdjacencyMatrix = adjacencyMatrix with get

    member this.SpreadInfection(infectionChance: int) =
        let n = Array.length this.Computers
        let nextInfected = Array.create n false
        for i in 0 .. n - 1 do
            if this.Computers[i].IsInfected then
                for j in 0 .. n - 1 do
                    if this.AdjacencyMatrix[i, j] && not this.Computers[j].IsInfected then
                        let infectionChance = this.Computers[j].OS.InfectionProbability
                        if infectionChance <= infectionChance then
                            nextInfected.[j] <- true
        for i in 0 .. n - 1 do
            if nextInfected[i] then
                this.Computers[i].IsInfected <- true


    member this.PrintNetworkState() =
        this.Computers
        |> Array.iteri (fun index computer ->
            printfn "Computer %d: OS = %s, Infected = %b" index computer.OS.Name computer.IsInfected)
