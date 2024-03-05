module Homework2.ParseTreeEvaluation

type ArithmeticTree =
    | Value of int
    | Plus of ArithmeticTree * ArithmeticTree
    | Multiply of ArithmeticTree * ArithmeticTree
    | Remainder of ArithmeticTree * ArithmeticTree

let rec evaluateArithmeticTree =
    function
    | Value x -> x
    | Plus(leftOperand, rightOperand) -> (evaluateArithmeticTree leftOperand) + (evaluateArithmeticTree rightOperand)
    | Multiply(leftOperand, rightOperand) ->
        (evaluateArithmeticTree leftOperand) * (evaluateArithmeticTree rightOperand)
    | Remainder(leftOperand, rightOperand) ->
        (evaluateArithmeticTree leftOperand) % (evaluateArithmeticTree rightOperand)