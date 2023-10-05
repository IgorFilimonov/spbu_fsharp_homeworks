module PhoneBook

open System.IO

let displayHints = 
    printfn ("What do you want to do?\n\
            1. Add person (name and phone number)\n\
            2. Find phone number by name\n\
            3. Find name by phone number\n\
            4. Print all data\n\
            5. Save data in file\n\
            6. Load data from file\n\
            0. Exit")

type Phonebook(numbersByNames: Map<string, string>, namesByNumbers: Map<string, string>) =
    member _.NumbersByNames with get() = numbersByNames
    member _.NamesByNumbers with get() = namesByNumbers
    member _.Add(name, number) = 
        let add name number =
            if (Map.tryFind number numbersByNames = None && Map.tryFind name namesByNumbers = None) then
                (Map.add name number numbersByNames, Map.add number name namesByNumbers)
            else
                printfn "Either name or number is already in the book"
                (numbersByNames, namesByNumbers)
        Phonebook(add name number)
    member _.FindByName(name) = Map.tryFind name numbersByNames
    member _.FindByNumber(number) = Map.tryFind number namesByNumbers
    member _.Print =
        for person in numbersByNames do
            printfn "%s %s" person.Key person.Value
    member _.Save(path) =
        use streamWriter = File.CreateText(path)
        Map.iter (fun name number -> streamWriter.WriteLine $"{name} {number}") |> ignore
        streamWriter.Close
    member this.Load(path) =
        if (not (File.Exists(path))) then printfn "This file doesn't exist"; Phonebook(Map.empty, Map.empty)
        else
            use streamReader = File.OpenText(path)
            let rec loop (phonebook: Phonebook) =
                let line = streamReader.ReadLine()
                if (line <> null) then
                    let nameAndPhone = line.Split ' '
                    loop (phonebook.Add(nameAndPhone[0], nameAndPhone[1]))
                else
                    phonebook
            let newPhonebook = loop this
            streamReader.Close |> ignore
            newPhonebook
 
type Command =
    | Add = 1
    | FindByName = 2
    | FindByNumber = 3
    | Print = 4
    | Save = 5
    | Load = 6
    | Exit = 0

let rec run (phonebook: Phonebook) =
    displayHints
    let command = System.Console.ReadLine() |> int |> enum<Command>
    match command with
    | Command.Add -> 
        printfn "Enter a name:"
        let name = System.Console.ReadLine()
        printfn "Enter a number:"
        let number = System.Console.ReadLine()
        run (phonebook.Add(name, number))
    | Command.FindByName ->
        printfn "Enter a name:"
        let name = System.Console.ReadLine()
        let number = phonebook.FindByName(name)
        match number with
        | Some x -> printfn "Found number: %s" x
        | None -> printfn "No person in this book has this name"
        run phonebook
    | Command.FindByNumber ->
        printfn "Enter a number:"
        let number = System.Console.ReadLine()
        let name = phonebook.FindByNumber(number)
        match name with
        | Some x -> printfn "Found name: %s" x
        | None -> printfn "No person in this book has this number"
        run phonebook
    | Command.Print -> phonebook.Print; run phonebook
    | Command.Save -> 
        printfn "Enter a path:"
        let path = System.Console.ReadLine()
        phonebook.Save(path) |> ignore
        run phonebook
    | Command.Load ->
        printfn "Enter a path:"
        let path = System.Console.ReadLine()
        run (phonebook.Load(path))
    | _ -> printfn "Incorrect command"

run (Phonebook(Map.empty, Map.empty))
