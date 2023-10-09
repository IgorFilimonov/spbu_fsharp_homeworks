module Crawler

open System.Net.Http
open System.Text.RegularExpressions
open System

let getPagesInfo (url: string) =
    async {
        try
            let linkPattern = Regex("<a href=\"(https?:\/\/\S+\W*)\"[^>]*>", RegexOptions.Compiled)
            let client = new HttpClient()
            let! mainPage = (client.GetStringAsync url) |> Async.AwaitTask

            // Gets all matching the pattern links from the main page
            let links = mainPage |> linkPattern.Matches |> Seq.map (fun i -> i.Groups[1].Value)

            // Creates a task for each link
            let tasks = links |> Seq.map (fun (link: string) ->
                async {
                    try
                        let! result = Async.AwaitTask(client.GetStringAsync(link))
                        return Some(link, result.Length)
                    with _ ->
                        return None
                })
            return Some(tasks |> Async.Parallel)
        with :? Net.WebException ->
            raise (InvalidOperationException "Incorrect url")
            return None
    }
