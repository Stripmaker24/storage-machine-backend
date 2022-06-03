module StorageMachine.Stock.Stock

open FSharp.Control.Tasks
open Giraffe
open Microsoft.AspNetCore.Http
open Thoth.Json.Net
open Thoth.Json.Giraffe
open Stock
open Bin

let binOverview (next: HttpFunc) (ctx: HttpContext) =
    task {
        let dataAccess = ctx.GetService<IStockDataAccess> ()
        let bins = Stock.binOverview dataAccess
        return! ThothSerializer.RespondJsonSeq bins Serialization.encoderBin next ctx 
    }

let stockOverview (next: HttpFunc) (ctx: HttpContext) =
    task {
        let dataAccess = ctx.GetService<IStockDataAccess> ()
        let bins = Stock.stockOverview dataAccess
        return! ThothSerializer.RespondJsonSeq bins Serialization.encoderBin next ctx 
    }

let productsInStock (next: HttpFunc) (ctx: HttpContext) =
    task {
        let dataAccess = ctx.GetService<IStockDataAccess> ()
        let bins = Stock.binOverview dataAccess
        let productsOverview = Stock.productsInStock (bins)
        return! ThothSerializer.RespondJson productsOverview Serialization.encoderProductsOverview next ctx 
    }

let AddBin(next: HttpFunc)(ctx: HttpContext) =
    task{
        let! decoderResult = ThothSerializer.ReadBody ctx Serialization.decoderBin
        match decoderResult with
        | Error message -> return! RequestErrors.BAD_REQUEST message next ctx
        | Ok bin -> 
            let dataAccess = ctx.GetService<IStockDataAccess>()
            let result = dataAccess.StoreBin bin
            match result with
            | Ok _ -> return! Successful.created (text " Good job!" ) next ctx
            | Error message -> return! RequestErrors.notAcceptable (text message) earlyReturn ctx
            
            
    }

let handlers : HttpHandler =
    choose [
        GET >=> route "/bins" >=> binOverview
        GET >=> route "/stock" >=> stockOverview
        GET >=> route "/stock/products" >=> productsInStock
        POST >=> route "/bins" >=> AddBin
    ]