open System
open System.Text.RegularExpressions

[<Literal>] 
let usage = "usage: <compress|expand> string"
let minLength = 3
let chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

let rec characterAndCountTuples data = 
    seq { if not (Seq.isEmpty data) then
             let firstCharacter = Seq.head data
             let runLength = Seq.length (Seq.takeWhile (fun x -> firstCharacter = x) data)
             yield firstCharacter, runLength
             yield! characterAndCountTuples (Seq.skip runLength data) }

let rec encodeTuple (character, length) = 
    if (length <= 0) then
         ""
    else if (length <= minLength && not ("~".Equals  character)) then
         String.replicate length character
    else if (length <= String.length chars) then
         let countChar = (chars.Chars ((length - 1) % 26))
         sprintf "~%c%s" countChar character
    else
         sprintf "~Z%s%s" character (encodeTuple (character, (length - 26)))
        
let compress data =
    characterAndCountTuples data
    |> Seq.fold(fun acc (character, length) -> acc + encodeTuple (character, length)) ""

let rec expandSequence data = 
    seq { if not (Seq.isEmpty data) then
             let firstCharacter = Seq.head data
             if ("~".Equals firstCharacter) then                 
                  let repeatCount = chars.IndexOf ((Seq.nth 1 data).Chars 0)
                  let characterToRepeat = Seq.nth 2 data
                  yield String.replicate repeatCount characterToRepeat
                  yield! expandSequence (Seq.skip 3 data)
             else
                  yield firstCharacter
                  yield! expandSequence (Seq.skip 1 data) }            

let expand data = 
    expandSequence data
    |> String.Concat ;;

let decode str =
    [ for m in Regex.Matches(str, "(\d+)(.)") -> m ]
    |> List.map (fun m -> Int32.Parse(m.Groups.[1].Value), m.Groups.[2].Value)
    |> List.fold (fun acc (len, s) -> acc + String.replicate len s) "" 

let encodeOrDecode command data = 
    match command with
    | "compress" -> compress data
    | "expand" -> expand data
    | _ -> usage

[<EntryPoint>]
let main argv = 
    let length = Array.length argv
    let output = 
        match length with
        | 2 -> encodeOrDecode argv.[0] [for c in argv.[1] -> sprintf "%c" c]
        | _ -> usage
    printfn "%s" output
    0