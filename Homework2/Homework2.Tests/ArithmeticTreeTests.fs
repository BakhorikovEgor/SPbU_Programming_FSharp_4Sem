module Homework2.ArithmeticTree.Tests

open NUnit.Framework
open FsUnit

let expressionsTestCases =
    [ BinaryExpression(UnaryExpression(Value, 10), Remainder, UnaryExpression(Value, 5)), 0
      BinaryExpression(UnaryExpression(Value, 5), Plus, UnaryExpression(Value, 5)), 10
      BinaryExpression(UnaryExpression(Value, -10), Multiply, UnaryExpression(Value, 2)), -20
      BinaryExpression(UnaryExpression(Value, 0), Multiply, UnaryExpression(Value, 10)), 0
      UnaryExpression(Value, 5), 5
      UnaryExpression(Negate, 5), -5
      BinaryExpression(
          (BinaryExpression(
              (UnaryExpression(Value, 5)),
              Plus,
              BinaryExpression(UnaryExpression(Value, 2), Multiply, UnaryExpression(Value, 2))
          )),
          Remainder,
          BinaryExpression(UnaryExpression(Value, 7), Remainder, UnaryExpression(Value, 4))
      ),
      0 ]
    |> List.map (fun (expression, expected) -> TestCaseData(expression, expected))


[<TestCaseSource(nameof (expressionsTestCases))>]
let arithmeticTreeEvaluationTests expression expected =
    evaluateArithmeticTree expression |> should equal expected
