[<RequireQualifiedAccess>]
module Trie

type Trie(c:Option<char>) =
    member val children = Map.empty with get, set
    member this.value = c
    member val eow = false with get, set
    

let rec insert(trie:Trie, value:string) =
    if value.Length = 0 then trie.eow <- true
    else
        let firstLetter = value.[0]
        let secondLetter = match value.Length with
                           | 1 -> Option.None
                           | _ -> Option.Some(value.[1])

        if (not(trie.children.ContainsKey(firstLetter))) then do
            trie.children <- trie.children.Add(firstLetter, new Trie(Option.Some(firstLetter)))
    
        insert (trie.children.Item(firstLetter), value.Substring(1))

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