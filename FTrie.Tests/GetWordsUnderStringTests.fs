namespace FTrie.Tests
    open NUnit.Framework

    [<TestFixture>]
    module GetWordsUnderStringTests = 

        [<Test>]
        let ``when abc and abcd are added they both return when ab is called``() = 
            let mutable t = Trie.Trie(Option.None)
            Trie.insert(t, "abc")
            Trie.insert(t, "abcd")

            let result = Trie.getWordsUnderString(t, "ab");
            Assert.AreEqual(2, Seq.length(result))
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("abc")))
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("abcd")))
            
        [<Test>]
        let ``when abc and def are added only def is returned when d is called``() =
            let mutable t = Trie.Trie(Option.None)
            Trie.insert(t, "abc")
            Trie.insert(t, "def")

            let result = Trie.getWordsUnderString(t, "d");
            Assert.AreEqual(1, Seq.length(result))
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("def")));

        [<Test>]
        let ``when abc and def are added only nothing is returned when g is called``() =
            let mutable t = Trie.Trie(Option.None)
            Trie.insert(t, "abc")
            Trie.insert(t, "def")

            let result = Trie.getWordsUnderString(t, "g");
            Assert.AreEqual(0, Seq.length(result))

        [<Test>]
        let ``when abc and abcd are added nothing is returned when b is called``() =
            let mutable t = Trie.Trie(Option.None)
            Trie.insert(t, "abc")
            Trie.insert(t, "abcd")

            let result = Trie.getWordsUnderString(t, "b");
            Assert.AreEqual(0, Seq.length(result))