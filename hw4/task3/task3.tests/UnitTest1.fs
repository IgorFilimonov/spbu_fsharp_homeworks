module PhoneBook.tests

open NUnit.Framework
open FsUnit
open PhoneBook
open System.IO

[<Test>]
let addTest () =
    let phonebook = Phonebook(Map.empty, Map.empty).Add("qqq", "qqqq")
    phonebook.NumbersByNames |> should equal (Map.empty.Add("qqq", "qqqq"))
    phonebook.NamesByNumbers |> should equal (Map.empty.Add("qqqq", "qqq"))

[<Test>]
let findByNameTest () =
    let phonebook = Phonebook(Map.empty, Map.empty).Add("qqq", "qqqq")
    phonebook.FindByName("qqq") |> should equal (Some("qqqq"))

[<Test>]
let findByNumberTest() =
    let phonebook = Phonebook(Map.empty, Map.empty).Add("qqq", "qqqq")
    phonebook.FindByNumber("qqqq") |> should equal (Some("qqq"))

[<Test>]
let loadFromFileTest() =
    let path = Path.Combine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../..")), "test.txt")
    let phonebook = Phonebook(Map.empty, Map.empty).Load(path.ToString())
    let numbersByNames = Map.empty.Add("qqq", "qqqq").Add("123", "345")
    let namesByNumbers = Map.empty.Add("qqqq", "qqq").Add("345", "123")
    (phonebook.NumbersByNames, phonebook.NamesByNumbers) |> should equal (numbersByNames, namesByNumbers)
