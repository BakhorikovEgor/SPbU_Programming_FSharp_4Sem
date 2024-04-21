module PointFreeTests

open NUnit.Framework
open FsCheck.NUnit
open PointFree

[<TestFixture>]
type TestMultiplication() =

    [<Property>]
    let CompareRealizations_ShouldBeEqual (x: int, l: int list) =
        let expected = List.map (fun y -> y * x) l
        let actual = pointFreeFunc x l
        expected = actual