namespace FTrie.Tests
    open NUnit.Framework
    open Trie

    [<TestFixture>]
    module GetWordsTests = 
            
        [<Test>]
        let ``create with one word and get words should return that word``() =
            let words = ["abc"]
            let t = Trie(words);
            Assert.AreEqual(words, t.getWords() |> Seq.sort);

        [<Test>]
        let ``create with three words and get words should return those words``() =
            let words = ["abc";"def";"ghi"]
            let t = Trie(words);
            Assert.AreEqual(words, t.getWords() |> Seq.sort);
            
        [<Test>]
        let ``create with three words with common first letter and get words should return those words``() =
            let words = ["abc";"aef";"ahi"]
            let t = Trie(words);
            Assert.AreEqual(words, t.getWords() |> Seq.sort);
            
        [<Test>]
        let ``create with three words one of them empty and get words should not return empty word``() =
            let words = ["";"def";"ghi"]
            let t = Trie(words);
            Assert.AreEqual(["def";"ghi"], t.getWords() |> Seq.sort);