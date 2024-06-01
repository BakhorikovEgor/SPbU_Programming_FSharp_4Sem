module PhoneBookTests

open NUnit.Framework
open PhoneBook
open FsUnit
open System.IO


[<Test>]
let StoreAddedEntryCorrectly () =
    let phoneBook = emptyPhonebook
    let name = "Alice"
    let phone = "12345"
    let updatedPhoneBook = add (new Entry(name, phone)) phoneBook
    updatedPhoneBook |> List.exists (fun x -> x.name = name) |> ignore
    updatedPhoneBook |> List.exists (fun x -> x.phone = phone) |> ignore

[<Test>]
let FindPhoneByNameReturnCorrectPhone () =
    let phoneBook = emptyPhonebook
    let name = "Alice"
    let phone = "12345"
    let updatedPhoneBook = add (new Entry(name, phone)) phoneBook
    let foundPhone = findPhoneByName name updatedPhoneBook
    foundPhone[0] |> should equal phone

[<Test>]
let FindNameByPhoneReturnCorrectName () =
    let phoneBook = emptyPhonebook
    let name = "Alice"
    let phone = "12345"
    let updatedPhoneBook = add (new Entry(name, phone)) phoneBook
    let foundName = findNameByPhone phone updatedPhoneBook
    foundName[0] |> should equal name

[<Test>]
let SaveAndLoadReturnCorrectData () =
    let tempFilePath = Path.GetTempFileName()
    let phoneBook = emptyPhonebook |>  add (new Entry("Alice", "1234")) |> add (new Entry("Bob", "67890"))
    savePhoneBookToFile tempFilePath phoneBook
    let loadedPhoneBook = loadPhoneBookFromFile tempFilePath
    phoneBook |> should equal loadedPhoneBook 
    File.Delete(tempFilePath) 
