module Homework2.ArithmeticTree

type BinaryOperator =
    | Plus
    | Multiply
    | Remainder


type UnaryOperation =
    | Value
    | Negate


type Expression =
    | UnaryExpression of UnaryOperation * int
    | BinaryExpression of Expression * BinaryOperator * Expression


let rec evaluateArithmeticTree =
    function
    | UnaryExpression(operator, operand) ->
        match operator with
        | Value -> operand
        | Negate -> -operand

    | BinaryExpression(leftOperand, operator, rightOperand) ->
        match operator with
        | Multiply -> (evaluateArithmeticTree leftOperand) * (evaluateArithmeticTree rightOperand)
        | Plus -> (evaluateArithmeticTree leftOperand) + (evaluateArithmeticTree rightOperand)
        | Remainder -> (evaluateArithmeticTree leftOperand) % (evaluateArithmeticTree rightOperand)
