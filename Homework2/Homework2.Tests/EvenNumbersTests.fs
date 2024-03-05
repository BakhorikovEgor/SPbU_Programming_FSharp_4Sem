module Homework2.EvenNumbers.Tests

open NUnit.Framework
open Homework2.EvenNumbers
open FsUnit
open FsCheck

let evenNumbersTestCases () = 
    [
        [], 0
        [1; 2; 3; 4; 5], 2
        [2; 4; 6; 8], 4
        [1; 3; 5], 0
    ] |> List.map (fun (list, expected) -> TestCaseData(list, expected))



[<TestCaseSource(nameof(evenNumbersTestCases))>]
let CountEvenUsingMapTest list expected =
        countEvenUsingMap list |> should equal expected
        

[<Test>]
let MapAndFilterCountEquivalent () =
    let check (x : List<int>)  = countEvenUsingMap x = countEvenUsingFilter x
    Check.QuickThrowOnFailure check

 
[<Test>]
let MapAndFoldrCountEquivalent () =
    let check (x : List<int>)  = countEvenUsingMap x = countEvenUsingFold x
    Check.QuickThrowOnFailure check