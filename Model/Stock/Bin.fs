module StorageMachine.Stock.Bin

open StorageMachine
open Common

type Bin = {
    Identifier : BinIdentifier
    Content : Option<PartNumber>
}

let isEmpty bin =
    match bin with
    | { Content = None } -> true
    | _ -> false

let isNotEmpty = not << isEmpty