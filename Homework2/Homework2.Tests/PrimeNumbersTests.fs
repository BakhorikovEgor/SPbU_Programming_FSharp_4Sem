module Homework2.PrimeNumbers.Tests

open NUnit.Framework
open FsUnit

[<Test>]
let IsPrimeTests () =
    let primes = [2; 3; 5; 7; 11; 13; 17; 19; 23; 29]
    primes |> List.iter (fun n -> isPrime n |> should equal true)

    let nonPrimes = [0; 1; 4; 6; 8; 9; 10; 12; 14; 15; 16]
    nonPrimes |> List.iter (fun n -> isPrime n |> should equal false)


[<Test>]
let GeneratePrimeNumbersTests ()=
    let first10Primes = generatePrimeNumbers |> Seq.take 10 |> Seq.toList
    let expectedPrimes = [2; 3; 5; 7; 11; 13; 17; 19; 23; 29]

    first10Primes |> should equal expectedPrimes