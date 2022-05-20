module StorageMachine.HttpHandlers

open Giraffe
open StorageMachine.Stock
open StorageMachine.Repacking

let requestHandlers : HttpHandler =
    choose [
        route "/hello" >=> GET >=> text "Storage machine is running"
        route "/number" >=> POST >=> PostExample.processPost
        Stock.handlers
        Repacking.handlers
    ]