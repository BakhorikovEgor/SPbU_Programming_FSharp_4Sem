module PhoneBook

open System
open System.IO

type Entry =
    struct
        val name: string
        val phone: string
        new(name, phone) = { name = name; phone = phone }
    end

type Phonebook = Entry list

let add entry (book: Phonebook) = entry :: book

let emptyPhonebook: Phonebook = []

let findPhoneByName name (phonebook: Phonebook) =
    phonebook |> List.filter (fun x -> x.name = name) |> List.map (fun x -> x.phone)

let findNameByPhone phone (phonebook: Phonebook) =
    phonebook |> List.filter (fun x -> x.phone = phone) |> List.map (fun x -> x.name)

let printPhoneBook (phoneBook: Phonebook) =
    phoneBook |> List.iter (fun book -> printfn "Name: %s, Phone: %s" book.name book.phone)

let savePhoneBookToFile filePath (phoneBook: Phonebook) =
    let lines = phoneBook |> List.map (fun book -> book.name + "," + book.phone)
    File.WriteAllLines(filePath, lines)

let loadPhoneBookFromFile filePath =
    let lines = File.ReadAllLines(filePath)
    lines
    |> Array.fold (fun acc line ->
        let parts = line.Split([|','|], StringSplitOptions.RemoveEmptyEntries)
        if parts.Length = 2 then
            let name, phone = parts.[0].Trim(), parts.[1].Trim()
            let entry = new Entry (name, phone)
            
            entry :: acc
        else acc
    ) emptyPhonebook

let rec commandLoop phoneBook =
    printfn "Enter command:"
    match System.Console.ReadLine().ToLower().Split(' ') with
    | [|"exit"|] -> ()
    | [|"add"; name; phone|] -> 
        let updatedPhoneBook = add (new Entry(name, phone)) phoneBook
        commandLoop updatedPhoneBook
    | [|"findphone"; name|] ->
        let phone = findPhoneByName name phoneBook
        printfn "%s" phone[0]
        commandLoop phoneBook
    | [|"findname"; phone|] ->
        let name = findNameByPhone phone phoneBook
        printfn "%s" name[0]
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
    commandLoop emptyPhonebook
    0
