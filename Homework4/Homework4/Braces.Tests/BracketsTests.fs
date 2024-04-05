module Brackets.Tests

open NUnit.Framework
open Brackets


let validBracketSequences () = [
    "()"; "[]"; "{}"; "([])"; "{()}"; "[{}]"; "({[]})"; "([]{})"; "(((())))"; "[]{}()"] |> List.map (fun (input) -> TestCaseData(input))


let invalidBracketSequences () = [
    "("; "]"; "}"; "(]"; "{)"; "[}"; "({[})"; "([]{)"; "(((())"; "]{}["] |> List.map (fun (input) -> TestCaseData(input))



[<TestCaseSource(nameof(validBracketSequences))>]
let validBracketSequencesShouldReturnTrue(sequence: string) =
    Assert.That(checkBrackets sequence, Is.EqualTo true)

[<TestCaseSource(nameof(invalidBracketSequences))>]
let invalidBracketSequencesShouldReturnFalse(sequence: string) =
    Assert.That(checkBrackets sequence, Is.EqualTo false)
