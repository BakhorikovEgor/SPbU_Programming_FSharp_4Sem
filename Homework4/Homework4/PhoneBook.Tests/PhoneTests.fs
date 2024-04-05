module PhoneBookTests

open NUnit.Framework
open PhoneBook
open FsUnit
open System.IO


[<Test>]
let StoreAddedEntryCorrectly () =
    let phoneBook = emptyPhoneBook
    let name = "Alice"
    let phone = "12345"
    let updatedPhoneBook = addEntry name phone phoneBook
    updatedPhoneBook.Keys |> should contain name
    updatedPhoneBook.Values |> should contain phone

[<Test>]
let FindPhoneByNameReturnCorrectPhone () =
    let phoneBook = emptyPhoneBook
    let name = "Alice"
    let phone = "12345"
    let updatedPhoneBook = addEntry name phone phoneBook
    let foundPhone = findPhoneByName name updatedPhoneBook
    foundPhone |> should equal phone

[<Test>]
let FindNameByPhoneReturnCorrectName () =
    let phoneBook = emptyPhoneBook
    let name = "Alice"
    let phone = "12345"
    let updatedPhoneBook = addEntry name phone phoneBook
    let foundName = findNameByPhone phone updatedPhoneBook
    foundName |> should equal name

[<Test>]
let SaveAndLoadReturnCorrectData () =
    let tempFilePath = Path.GetTempFileName()
    let phoneBook = emptyPhoneBook |> addEntry "Alice" "12345" |> addEntry "Bob" "67890"
    savePhoneBookToFile tempFilePath phoneBook
    let loadedPhoneBook = loadPhoneBookFromFile tempFilePath
    loadedPhoneBook |> should equal phoneBook
    File.Delete(tempFilePath) 
