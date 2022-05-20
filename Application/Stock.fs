module StorageMachine.Stock.Stock

open StorageMachine
open Bin
open Stock

type IStockDataAccess =
    abstract RetrieveAllBins : unit -> List<Bin>

let binOverview (dataAccess : IStockDataAccess) : List<Bin> =
    dataAccess.RetrieveAllBins ()

let stockOverview (dataAccess : IStockDataAccess) : List<Bin> =
    let allBins = dataAccess.RetrieveAllBins ()
    let actualStock = allBins |> List.filter Bin.isNotEmpty
    actualStock

type ProductsOverview = Set<Product * Quantity>

let productsInStock(bins) : ProductsOverview =
    let products = Stock.allProducts (bins)
    products
    |> failwith "Exercise 0: Complete this implementation."