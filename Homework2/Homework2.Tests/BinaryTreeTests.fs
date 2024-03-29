module Homework2.BinaryTree.Tests

open NUnit.Framework
open FsUnit

let binaryTreeTestCases  =
    [
        ((fun _ -> 1) : int -> int), Node(3, Leaf, Leaf), Node(1, Leaf, Leaf)
        ((fun x -> x / 10) : int -> int), Node(10, Node(20, Leaf, Leaf), Leaf), Node(1, Node(2, Leaf, Leaf), Leaf)
        ((fun _ -> 0) : int -> int), Leaf, Leaf
    ]   |> List.map (fun (f, tree, expected) -> TestCaseData(f, tree, expected)) 
 
 
[<TestCaseSource(nameof(binaryTreeTestCases))>]
let binaryTreeMapTests (f : int -> int) tree expected =
    binaryTreeMap f tree |> should equal expected