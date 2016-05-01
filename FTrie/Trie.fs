[<RequireQualifiedAccess>]
module Trie

type Trie(c:Option<char>, words:string seq, eow:bool) =
    member this.value = c
    member this.eow = eow
    member this.children = words
                           |> Seq.groupBy(fun word -> word.[0])
                           |> Seq.map(fun (ch, w) -> 
                                (ch, new Trie(
                                    Option.Some(ch), 
                                    w |> Seq.filter (fun word -> word.Length > 1) |> Seq.map (fun word -> word.Substring(1)),
                                    w |> Seq.exists (fun word -> word.Length = 1))))
                           |> Map.ofSeq

    new(words:string seq) = Trie(Option.None, words, false)

let getWords(trie:Trie) : string seq =
    let rec getWordsInternal(trie:Trie, substring:string) : string seq =
        seq {
            if trie.children.Count = 0 then yield substring
            else
                if (trie.eow) then yield substring
                yield! trie.children |> Map.toSeq |> Seq.collect(fun (c,t) -> getWordsInternal(t, substring+c.ToString()))
        }

    getWordsInternal(trie, "")

let getPrefix(trie:Trie, prefix:string) =
    let rec getTrie (curr:Trie, currVal:string) =
        if currVal.Length = 0 then Option.Some(curr)
        else if not(curr.children.ContainsKey(currVal.[0])) then Option.None
        else getTrie(curr.children.Item(currVal.[0]), currVal.Substring(1))
    
    getTrie(trie, prefix)

let getWordsUnderString(trie:Trie, value:string) =
    let t = getPrefix(trie, value)
    match t with
    | Option.None -> Seq.empty
    | _ -> getWords(t.Value) |> Seq.map (fun w -> value + w)

let isPrefix(trie:Trie, prefix:string) =
    let endTrie = getPrefix(trie,prefix)
    Option.isSome(endTrie)

let isWord(trie:Trie, word:string) =
    let endTrie = getPrefix(trie, word);
    let result = endTrie.IsSome && endTrie.Value.eow
    result