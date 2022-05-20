module StorageMachine.Repacking.Repacking

open StorageMachine
open Common
open BinTree

type IBinTreeDataAccess =
    abstract RetrieveBinTree : BinIdentifier -> Option<BinTree>

let viewBinTree (dataAccess : IBinTreeDataAccess) (bin : BinIdentifier) : Option<BinTree> =
    dataAccess.RetrieveBinTree bin

let productCount (dataAccess : IBinTreeDataAccess) (bin : BinIdentifier) : Option<int> =
    let binTree = dataAccess.RetrieveBinTree bin
    binTree |> Option.map BinTree.productCount