module Brackets.Tests

open NUnit.Framework
open FsUnit
open Brackets


let validBracketSequences () = [
    "()"; "[]"; "{}"; "([])"; "{()}"; "[{}]"; "({[]})"; "([]{})"; "(((())))"; "[]{}()"] |> List.map (fun (input) -> TestCaseData(input))


let invalidBracketSequences () = [
    "("; "]"; "}"; "(]"; "{)"; "[}"; "({[})"; "([]{)"; "(((())"; "]{}["] |> List.map (fun (input) -> TestCaseData(input))



[<TestCaseSource(nameof(validBracketSequences))>]
let validBracketSequencesShouldReturnTrue(sequence: string) =
    checkBrackets sequence |> should be True
    Assert.IsTrue(checkBrackets sequence)

[<TestCaseSource(nameof(invalidBracketSequences))>]
let invalidBracketSequencesShouldReturnFalse(sequence: string) =
    checkBrackets sequence |> should be False
