module StorageMachine.Stock.Stock

open StorageMachine
open Bin
open Stock

let stockPersistence = { new IStockDataAccess with
    
    member this.RetrieveAllBins () =
        SimulatedDatabase.retrieveBins ()
        |> Set.map (fun binIdentifier ->
            {
                Identifier = binIdentifier
                Content = SimulatedDatabase.retrieveStock () |> Map.tryFind binIdentifier
            }
        )
        |> Set.toList

    member this.StoreBin (bin) =
        let result = SimulatedDatabase.storeBin(bin)
        
        match result with
        | Error message -> result.ToString() 
        | Ok message -> "bin has been stored!"
}