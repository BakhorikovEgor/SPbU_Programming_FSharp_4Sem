module PhoneBook

open System
open System.IO

type PhoneBookEntry = { Name: string; Phone: string }
type PhoneBook = Map<string, string>

let emptyPhoneBook = Map.empty

let addEntry name phone (phoneBook: PhoneBook) =
    phoneBook.Add(name, phone)

let findPhoneByName name (phoneBook: PhoneBook) =
    match phoneBook.TryFind(name) with
    | Some(phone) -> phone
    | None -> "Entry not found"

let findNameByPhone phone (phoneBook: PhoneBook) =
    phoneBook |> Map.filter (fun _ p -> p = phone) |> Map.toList
    |> function
        | [] -> "Entry not found"
        | (name, _) :: _ -> name

let printPhoneBook (phoneBook: PhoneBook) =
    phoneBook |> Map.iter (fun name phone -> printfn "Name: %s, Phone: %s" name phone)

let savePhoneBookToFile filePath (phoneBook: PhoneBook) =
    let lines = phoneBook |> Map.toList |> List.map (fun (name, phone) -> name + "," + phone)
    File.WriteAllLines(filePath, lines)

let loadPhoneBookFromFile filePath =
    let lines = File.ReadAllLines(filePath)
    lines
    |> Array.fold (fun acc line ->
        let parts = line.Split([|','|], StringSplitOptions.RemoveEmptyEntries)
        if parts.Length = 2 then
            let name, phone = parts.[0].Trim(), parts.[1].Trim()
            Map.add name phone acc
        else acc
    ) emptyPhoneBook

let rec commandLoop phoneBook =
    printfn "Enter command:"
    match System.Console.ReadLine().ToLower().Split(' ') with
    | [|"exit"|] -> ()
    | [|"add"; name; phone|] -> 
        let updatedPhoneBook = addEntry name phone phoneBook
        commandLoop updatedPhoneBook
    | [|"findphone"; name|] ->
        let phone = findPhoneByName name phoneBook
        printfn "%s" phone
        commandLoop phoneBook
    | [|"findname"; phone|] ->
        let name = findNameByPhone phone phoneBook
        printfn "%s" name
        commandLoop phoneBook
    | [|"printall"|] ->
        printPhoneBook phoneBook
        commandLoop phoneBook
    | [|"save"; filePath|] ->
        savePhoneBookToFile filePath phoneBook
        printfn "Phonebook saved to %s" filePath
        commandLoop phoneBook
    | [|"load"; filePath|] ->
        let updatedPhoneBook = loadPhoneBookFromFile filePath
        printfn "Phonebook loaded from %s" filePath
        commandLoop updatedPhoneBook
    | _ -> 
        printfn "Invalid command"
        commandLoop phoneBook

[<EntryPoint>]
let main argv =
    printfn "Welcome to the Phone Book App!"
    commandLoop emptyPhoneBook
    0
