module Homework1

let factorial number =
    if number < 0 then None else
        
    let rec helper acc =
        function
        | 0 -> Some(acc)
        | n -> helper (acc * n) (n - 1)

    helper 1 number


let fibonacci number =
    if number <= 0 then None else
    
    let rec helper curr next =
        function
        | 1 -> Some(curr)
        | n -> helper next (curr + next) (n - 1)
        
    helper 0 1 number
        
            
let reverseList list =
    let rec helper acc =
        function
        | [] -> acc
        | h :: t -> helper (h :: acc) t
    
    helper [] list
    
    
let seriesOfDegrees n m =
    if (n < 0 || m < 0) then None else
        
    let rec helper acc =
        function
        | 0 -> Some(acc)
        | x -> helper (List.head(acc) / 2 :: acc) (x - 1)
    
    helper [pown 2 (n + m)] m
 
 
let findFirst list number =
    let rec helper i =
        function
        | [] -> None
        | h :: _ when h = number -> Some(i)
        | _ :: t -> helper (i + 1) t
    
    helper 1 list
 