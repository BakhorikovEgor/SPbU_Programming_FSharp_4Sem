module Homework2.EvenNumbers

let modPositive dividend divisor =
    let rawMod = dividend % divisor
    if rawMod < 0 then
        rawMod + abs divisor
    else
        rawMod
        
let countEvenUsingMap = List.map (fun x -> modPositive (x + 1) 2) >> List.sum

let countEvenUsingFilter = List.filter (fun x -> x % 2 = 0) >> List.length

let countEvenUsingFold = List.fold (fun state elem -> if elem % 2 = 0 then state + 1 else state) 0
