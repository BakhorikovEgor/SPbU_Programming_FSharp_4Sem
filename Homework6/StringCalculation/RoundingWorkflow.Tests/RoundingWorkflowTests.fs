module RoundingWorkflowTests

open NUnit.Framework
open FsUnit
open RoundingWorkflow

[<Test>]
let DivisionWithRoundingFloat_ShouldWorkCorrect() =
    let result = rounding 3 {
        let! a = 1.0
        let! b = 3.0
        return  a / b
    }
    result |> should equal 0.333

[<Test>]
let InvalidPrecision_ShouldThrowExceptiob() =
    (fun () ->  
        rounding -1 {
            let! a = 1.0
            let! b = 2.0
            return  a / b
        } 
        |> ignore)
    |> should throw typeof<System.ArgumentOutOfRangeException>
