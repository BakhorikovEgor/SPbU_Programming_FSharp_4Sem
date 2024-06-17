module Brackets

open System.Collections.Generic


let checkBrackets (s: string) =
    let rec check lst chars =
        match chars, lst with
        | [], [] -> true
        | [], _ -> false
        | c::cs, _ when c = '(' || c = '[' || c = '{' -> check (c::lst) cs
        | c::cs, '('::ls when c = ')' -> check ls cs
        | c::cs, '['::ls when c = ']' -> check ls cs
        | c::cs, '{'::ls when c = '}' -> check ls cs
        | c::cs, _ -> false

    s |> Seq.toList |> check []

