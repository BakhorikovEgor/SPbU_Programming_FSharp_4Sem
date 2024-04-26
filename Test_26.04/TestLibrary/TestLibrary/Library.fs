module TestLibrary

let sumEvenFibonacci max =
    Seq.unfold (fun (a, b) -> Some(a, (b, a + b))) (0, 1)
    |> Seq.filter (fun n -> n % 2 = 0) 
    |> Seq.takeWhile (fun n -> n <= max)
    |> Seq.sum 

let printSquare n =
    let fullLine = String.replicate n "*"
    let emptyLine = "*" + (String.replicate (n - 2) " ") + "*"
    let lines =
        [fullLine]
        @ List.replicate (n - 2) emptyLine
        @ [fullLine]
    
    String.concat "\n" lines


type ConcurrentStack<'T>() =    
    let mutable stack : List<'T> = []

    member this.Push value =
      lock stack (fun () -> 
         stack <- value :: stack)

    member this.TryPop() =
      lock stack (fun () ->
         match stack with
         | result :: newStack ->
            stack <- newStack
            Some(result)
         | [] -> None
      )

type PriorityQueue<'T>(comparer: 'T -> 'T -> int) =
    let mutable elements: 'T list = []

    member this.Enqueue(item: 'T) =
        elements <- List.sortWith comparer (item :: elements)

    member this.Dequeue() =
        match elements with
        | [] -> failwith "Queue is empty"
        | head :: tail ->
            elements <- tail
            head

    member this.Peek() =
        match elements with
        | [] -> None
        | head :: _ -> Some head