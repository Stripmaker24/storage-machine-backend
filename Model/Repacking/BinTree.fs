﻿module StorageMachine.Repacking.BinTree

open StorageMachine
open Common

type PackageType = 
    | BubbleWrap
    | CardBoard

type BinTree =
    | Bin of BinIdentifier * List<BinTree>
    | Product of PartNumber
    | PackagedProduct of PartNumber * PackageType

let rec productCount binTree =
    match binTree with
    | Bin (_, productsOrBins) -> List.sumBy productCount productsOrBins
    | Product _ | PackagedProduct _ -> 1
    