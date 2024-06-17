module LocalNetwork.Tests

open NUnit.Framework
open LocalNetwork


[<Test>]
let ``All Nearest Computers Should Be Infected If Infection Probability Is 100``() =
    let osWin = OS("Windows", 100)
    let osLinux = OS("Linux", 100)
    let computers = [| 
        PC(osWin, false); 
        PC(osLinux, false); 
        PC(osWin, true)
    |]
    let adjacencyMatrix = array2D [[| false; true; false |]; [| true; false; true |]; [| false; true; false |]]
    let network = Network(computers, adjacencyMatrix)
    network.SpreadInfection(100)
    network.PrintNetworkState()
    Assert.IsTrue(computers[1].IsInfected)

[<Test>]
let ``No Computers Should Be Infected If Infection Probability Is 0``() =
    let osWin = OS("Windows", 0)
    let osLinux = OS("Linux", 0)
    let computers = [| 
        PC(osWin, false); 
        PC(osLinux, false); 
        PC(osWin, false)
    |]
    let adjacencyMatrix = array2D [[| false; true; false |]; [| true; false; true |]; [| false; true; false |]]
    let network = Network(computers, adjacencyMatrix)
    network.SpreadInfection(0)
    network.PrintNetworkState()
    Assert.IsTrue(computers |> Array.forall (fun pc -> not pc.IsInfected))

[<Test>]
let ``Infection Spreads Correctly``() =
    let osWin = OS("Windows", 50)
    let osLinux = OS("Linux", 50)
    let computers = [| 
        PC(osWin, true); 
        PC(osLinux, false); 
        PC(osWin, false)
    |]
    let adjacencyMatrix = array2D [[| false; true; true |]; [| true; false; false |]; [| true; false; false |]]
    let network = Network(computers, adjacencyMatrix)
    network.SpreadInfection(50)
    network.PrintNetworkState()
    Assert.IsTrue(computers[1].IsInfected && computers[2].IsInfected)