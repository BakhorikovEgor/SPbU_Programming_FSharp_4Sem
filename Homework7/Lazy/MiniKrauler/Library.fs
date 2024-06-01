module Crawler

open System.Threading
open System.Net.Http
open System.Text.RegularExpressions


let downloadPageAsync (url: string, httpClient: HttpClient) =
    async {
        let request = new HttpRequestMessage(HttpMethod.Get, url) 
        let! response = httpClient.SendAsync(request, CancellationToken.None) |> Async.AwaitTask
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        return content
    }


let getLinks (html: string) : string[] =
    let pattern = "<a\\s+(?:[^>]*?\\s+)?href=\"(https://[^\"]*)\""
    Regex.Matches(html, pattern)
    |> Seq.cast<Match>
    |> Seq.map (_.Groups.[1].Value)
    |> Seq.toArray


let processPage (url: string, httpClient: HttpClient) =
    async {
        let! content = downloadPageAsync (url, httpClient)
        let links = getLinks content
        let! results = Async.Parallel (links |> Array.map (fun link -> async {
            let! content = downloadPageAsync (link, httpClient)
            return link, content.Length}))

        return results;
    }

let printPageSizes (url: string, httpClient: HttpClient) =
    async {
        let! sizes = processPage (url, httpClient)
        sizes |> Array.iter (fun (link, length) ->
            printfn "%s — %d" link length)
    }