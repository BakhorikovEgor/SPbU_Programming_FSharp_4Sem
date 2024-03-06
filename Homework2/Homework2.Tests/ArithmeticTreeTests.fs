module Homework2.ArithmeticTree.Tests

open NUnit.Framework
open FsUnit

let expressionsTestCases =
    [ Remainder(Value 10, Value 5), 0
      Plus(Value 5, Value 5), 10
      Multiply(Value -10, Value 2), -20
      Multiply(Value 0, Value 10), 0
      Value 5, 5
      Remainder(Plus(Value 5, Multiply(Value 2, Value 2)), Remainder(Value 7, Value 4)), 0 ]
    |> List.map (fun (expression, expected) -> TestCaseData(expression, expected))


[<TestCaseSource(nameof(expressionsTestCases))>]
let arithmeticTreeEvaluationTests expression expected =
    evaluateArithmeticTree expression |> should equal expected