open System.Collections.Generic

let checkBrackets (s: string) =
    let stack = Stack<char>()
    let bracketPairs = dict [ '(', ')'; '[', ']'; '{', '}' ]
    let openingBrackets = Set.ofSeq bracketPairs.Keys
    let closingBrackets = Set.ofSeq bracketPairs.Values
    
    s |> Seq.fold (fun acc c ->
        match acc, openingBrackets.Contains(c), closingBrackets.Contains(c) with
        | false, _, _ -> false
        | _, true, _ -> 
            stack.Push(c)
            acc
        | _, _, true when stack.Count > 0 && bracketPairs.[stack.Peek()] = c -> 
            stack.Pop() |> ignore
            acc
        | _, _, true -> false
        | _ -> acc) true && stack.Count = 0