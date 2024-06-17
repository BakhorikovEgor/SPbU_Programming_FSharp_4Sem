module LibraryTests

open NUnit.Framework
open FsUnit
open System

[<TestCase(10, 10)>]
[<TestCase(50, 44)>]
[<TestCase(1000000, 1089154)>]
let ``sumEvenFibonacci should calculate the sum of even Fibonacci numbers correctly`` (maxValue, expected) =
    TestLibrary.sumEvenFibonacci maxValue|> should equal expected


[<TestCase(2, "**\n**")>]
[<TestCase(5, "*****\n*   *\n*   *\n*   *\n*****")>]
let ``printSquare should generate a square of asterisks correctly`` (sideLength, expected) =
    TestLibrary.printSquare sideLength|> should equal expected

[<Test>]
let ``PriorityQueue.Peek should return None when the queue is empty`` () =
    let priorityQueue = TestLibrary.PriorityQueue<int>(fun x y -> compare y x)

    priorityQueue.Peek() |> should equal None

[<Test>]
let``PriorityQueue.Dequeue should remove and return the item with the highest priority`` () =
    let priorityQueue = TestLibrary.PriorityQueue<int>(fun x y -> compare y x)
    priorityQueue.Enqueue(5)
    priorityQueue.Enqueue(10)
    priorityQueue.Enqueue(3)

    priorityQueue.Dequeue() |> should equal (Some 10)

[<Test>]
let ``PriorityQueue.Dequeue should throw an exception when the queue is empty`` () =
    let priorityQueue = TestLibrary.PriorityQueue<int>(fun x y -> compare y x)
    
    (fun () -> priorityQueue.Dequeue() |> ignore) |> should throw typeof<InvalidOperationException>

[<Test>]
let ``PriorityQueue.Peek should return the item with the highest priority without removing it`` () =
    let priorityQueue = TestLibrary.PriorityQueue<int>(fun x y -> compare y x)
    priorityQueue.Enqueue(5)
    priorityQueue.Enqueue(10)
    priorityQueue.Enqueue(3)

    let result = priorityQueue.Peek()
    
    priorityQueue.Dequeue() |> should equal result
