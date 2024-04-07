module PointFree

let pointFreeFunc = List.map << (*) 

//Шаги 
//      List.map (fun y -> y * x) l ->
//      List.map (fun y -> y * x) ->
//      List.map ((*) x) ->
//      x |> (List.map << (*)) ->
//      List.map << (*)