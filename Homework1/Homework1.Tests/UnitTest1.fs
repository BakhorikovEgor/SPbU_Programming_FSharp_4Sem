module Homework1.Tests

open NUnit.Framework

let testCasesForReverseList =
    [ yield [| [ 1; 2; 3 ]; [ 3; 2; 1 ] |]
      yield [| []; [] |]
      yield [| [ 1 ]; [ 1 ] |] ]

let correctTestCasesForSeriesOfDegrees =
    [ (1, 2, [ 2; 4; 8 ])
      (0, 0, [ 1 ])
      (0, 2, [ 1; 2; 4 ])
      (2, 0, [ 4 ]) ]
    |> List.map (fun (n, m, ls) -> TestCaseData(n, m, ls))

let incorrectTestCasesForSeriesOfDegrees () =
    [ yield [| -1; 1 |]
      yield [| 1; -1 |]
      yield [| -1; -1 |] ]

let correctTestCasesForFindFirst =
    [ ([ 5; 3; 1 ], 3, Some 1)
      ([ 1; 1; 1 ], 1, Some 0)
      ([ 0; 0; 10 ], 10, Some 2) ]
    |> List.map (fun (ls, n, m) -> TestCaseData(ls, n, m))

let incorrectTestCasesForFindFirst =
    [ ([], 1)
      ([ -1; 7; 12 ], 11) ]
    |> List.map (fun (ls, n) -> TestCaseData(ls, n))


[<TestCase(0, 1)>]
[<TestCase(0, 1)>]
[<TestCase(1, 1)>]
[<TestCase(5, 120)>]
[<TestCase(6, 720)>]
let FactorialCorrectDataTests (number, expected) =
    Assert.That((Factorial number).Value, Is.EqualTo expected)


[<TestCase(-1)>]
[<TestCase(-2)>]
[<TestCase(-10)>]
[<TestCase(-100)>]
[<TestCase(-1234)>]
let FactorialIncorrectDataTests number =
    Assert.That(Factorial number, Is.EqualTo None)


[<TestCase(1, 0)>]
[<TestCase(2, 1)>]
[<TestCase(3, 1)>]
[<TestCase(9, 21)>]
[<TestCase(25, 46368)>]
let FibonacciCorrectDataTests (number, expected) =
    Assert.That((Fibonacci number).Value, Is.EqualTo expected)

[<TestCase(0)>]
[<TestCase(-1)>]
[<TestCase(-2)>]
[<TestCase(-12)>]
[<TestCase(-100)>]
let FibonacciIncorrectDataTests number =
    Assert.That(Fibonacci number, Is.EqualTo None)


[<TestCaseSource("testCasesForReverseList")>]
let ReverseListTests list expected =
    Assert.That(ReverseList list, Is.EqualTo expected)


[<TestCaseSource("correctTestCasesForSeriesOfDegrees")>]
let SeriesOfDegreesCorrectDataTests (n, m, expected) =
    Assert.That((SeriesOfDegrees n m).Value, Is.EqualTo expected)


[<TestCaseSource("incorrectTestCasesForSeriesOfDegrees")>]
let SeriesOfDegreesIncorrectDataTests (n, m) =
    Assert.That(SeriesOfDegrees n m, Is.EqualTo None)


[<TestCaseSource("correctTestCasesForFindFirst")>]
let FindFirstCorrectDataTests (list, number, expected) =
    Assert.That(FindFirst list number, Is.EqualTo expected)


[<TestCaseSource("incorrectTestCasesForFindFirst")>]
let FindFirstIncorrectDataTests (list, number) =
    Assert.That(FindFirst list number, Is.EqualTo None)
