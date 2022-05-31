module StorageMachine.Stock.Stock

open StorageMachine

open Common
open Bin

type Product = Product of PartNumber

type Quantity = int

let allProducts bins : List<Product> =
    bins
    |> Seq.choose (fun n -> if Bin.isNotEmpty(n) = true then Some n.Content.Value else None)
    |> Seq.map Product
    |> Seq.toList

let totalQuantity (products:List<Product>) : Map<Product, Quantity> =
    products
    |> Seq.countBy id
    |> Map.ofSeq