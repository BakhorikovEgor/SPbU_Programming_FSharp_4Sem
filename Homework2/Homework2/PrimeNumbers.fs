module Homework2.PrimeNumbers

let isPrime number  =
    if number < 2 then false
    else
        let rec helper =
            function
            | divisor when divisor * divisor > number ->  true
            | divisor when number % divisor = 0 ->  false
            | divisor -> helper (divisor + 1)
        helper 2
   
let generatePrimeNumbers = 
    Seq.initInfinite id |> Seq.skip 2 |> Seq.filter isPrime