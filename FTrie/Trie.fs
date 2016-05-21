[<RequireQualifiedAccess>]
module Trie

type Trie(c:Option<char>, words:string seq) =
    let childMap = words
                   |> Seq.filter(fun w -> w.Length > 0)
                   |> Seq.groupBy(fun word -> word.[0])
                   |> Seq.map(fun (ch, w) -> 
                       (ch, new Trie(
                           Option.Some(ch), 
                           w |> Seq.map (fun word -> word.Substring(1)))))
                   |> Map.ofSeq

    new(words:string seq) = Trie(Option.None, words)
    member this.value = c
    member this.eow = this.value.IsSome && words |> Seq.exists (fun word -> word.Length = 0)
    member this.children = childMap

    // follows a given prefix down the tree and returns the node at the end
    member private this.getPrefixTrie prefix =
        let rec getTrie (curr:Trie, currVal:string) =
            if currVal.Length = 0 then Option.Some(curr)
            else if not(curr.children.ContainsKey(currVal.[0])) then Option.None
            else getTrie(curr.children.Item(currVal.[0]), currVal.Substring(1))
    
        getTrie(this, prefix)

    member this.withWord word =
        if this.value.IsSome then invalidOp "Cannot add a word to a non-root Trie node"
        else
            Trie(Option.None, word :: List.ofSeq words)

    member this.getWords() =
        let rec getWordsInternal(trie:Trie, substring) : string seq =
            seq {
                if trie.children.Count = 0 then yield substring
                else
                    if (trie.eow) then yield substring
                    yield! trie.children |> Map.toSeq |> Seq.collect(fun (c,t) -> getWordsInternal(t, substring+c.ToString()))
            }

        getWordsInternal(this, "")

    member this.isPrefix(prefix:string) =
        let endTrie = this.getPrefixTrie(prefix)
        Option.isSome(endTrie)
    
    member this.getWordsPrefixedBy(value:string) =
        let t = this.getPrefixTrie(value)
        match t with
        | Option.None -> Seq.empty
        | _ -> t.Value.getWords() |> Seq.map (fun w -> value + w)

    member this.contains(word:string) =
        let endTrie = this.getPrefixTrie(word);
        let result = endTrie.IsSome && endTrie.Value.eow
        result







