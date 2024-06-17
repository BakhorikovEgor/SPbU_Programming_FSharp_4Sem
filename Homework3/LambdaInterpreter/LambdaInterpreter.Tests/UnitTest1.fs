module LambdaInterpreter.Tests 

open LambdaInterpreter
open FsUnit;
open NUnit.Framework

let tests () =
    [
        Var "x", Var "x"
        Application(Var "x", Var "y"), Application(Var "x", Var "y")
        Application(Abstraction("x", Var "x"), Var "z"), Var "z"
        Application(Abstraction("x", Var "y"), Application(Abstraction("x", Abstraction("x", Var "x")), Abstraction("x", Application(Var "x", Var "x")))), Var "y"

    ]|> List.map (fun (input, result) -> TestCaseData(input, result))


[<TestCaseSource(nameof tests)>]
let reduceTests data expected  =  
     LambdaInterpreter.reduce data |> should equal expected