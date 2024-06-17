/// Module containing various functions and types for testing purposes.
module TestLibrary

open System

/// Calculates the sum of even Fibonacci numbers up to a given maximum.
/// 
/// `max` - The maximum value up to which the Fibonacci numbers should be calculated.
/// 
/// Returns the sum of even Fibonacci numbers.
let sumEvenFibonacci max =
    Seq.unfold (fun (a, b) -> Some(a, (b, a + b))) (0, 1)
    |> Seq.filter (fun n -> n % 2 = 0) 
    |> Seq.takeWhile (fun n -> n <= max)
    |> Seq.sum 

/// Prints a square made of asterisks (*) with the specified side length.
///
/// `n` - The side length of the square.
///
/// Returns a string representation of the square.
let printSquare n =
    let fullLine = String.replicate n "*"
    let emptyLine = "*" + (String.replicate (n - 2) " ") + "*"
    let lines =
        [fullLine]
        @ List.replicate (n - 2) emptyLine
        @ [fullLine]
    
    
    String.concat "\n" lines

/// Represents a priority queue.
type PriorityQueue<'T>(comparer: 'T -> 'T -> int) =
    let mutable elements: 'T list = []

    /// Adds an item to the priority queue.
    ///
    /// `item` - The item to be added.
    member this.Enqueue(item: 'T) =
        elements <- List.sortWith comparer (item :: elements)

    /// Removes and returns the item with the highest priority from the priority queue.
    ///
    /// Returns the item with the highest priority.
    member this.Dequeue() =
        match elements with
        | [] ->  raise (new InvalidOperationException("Queue is empty"))
        | head :: tail ->
            elements <- tail
            Some head

    /// Returns the item with the highest priority from the priority queue without removing it.
    ///
    /// Returns `Some item` if the priority queue is not empty; otherwise, `None`.
    member this.Peek() =
        match elements with
        | [] -> None
        | head :: _ -> Some head