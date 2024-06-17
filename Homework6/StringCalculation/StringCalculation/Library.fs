module StringCalculation

type Result<'T> =
    | Success of 'T
    | Error of string


type CalculationBuilder() =
    member this.Bind(x: string, f: int -> Result<'T>) =
        match System.Int32.TryParse(x) with
        | (true, num) -> f num
        | (false, _) -> Error "Invalid number"

    member this.Return(x: int) = Success x

let calculate = CalculationBuilder()