module StorageMachine.Stock.Stock

open StorageMachine

open Common
open Bin

type Product = Product of PartNumber

type Quantity = int

let allProducts bins : List<Product> =
    bins
    |> Seq.choose ()
    |> Seq.map Product
    |> Seq.toList

let totalQuantity products : Map<Product, Quantity> =
    products
    |> failwith "Exercise 0: Fill this in to complete this function. Use type inference as a guide."
    |> Map.ofSeq