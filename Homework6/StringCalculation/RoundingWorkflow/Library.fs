module RoundingWorkflow

open System

type RoundingBuilder(precision: int) =
    member this.Bind(x: double, f) = f (Math.Round(x, precision))
    member this.Return(x: double ) = Math.Round(x, precision)

let rounding precision = RoundingBuilder(precision)