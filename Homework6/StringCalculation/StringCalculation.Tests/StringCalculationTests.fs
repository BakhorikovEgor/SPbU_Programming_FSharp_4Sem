module StringCalculationTests

open NUnit.Framework
open FsUnit
open StringCalculation


[<Test>]
let ``Adding valid numbers should return Success``() =
    let result = calculate {
        let! x = "3"
        let! y = "4"
        return x + y
    }
    result |> should equal (Success 7)

[<Test>]
let ``Adding invalid input should return Error``() =
    let result = calculate {
        let! x = "3"
        let! y = "abc"
        return x + y
    }
    match result with
    | Error msg -> msg |> should startWith "Invalid number"
    | _ -> failwith "Expected error"