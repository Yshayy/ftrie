namespace FTrie.Tests
    open NUnit.Framework

    [<TestFixture>]
    module GetWordsUnderStringTests = 

        [<Test>]
        let ``when abc and abcd are added they both return when ab is called``() = 
            let t = Trie.Trie(["abc";"abcd"])

            let result = Trie.getWordsUnderString(t, "ab");
            Assert.AreEqual(2, Seq.length(result))
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("abc")))
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("abcd")))
            
        [<Test>]
        let ``when abc and def are added only def is returned when d is called``() =
            let t = Trie.Trie(["abc";"def"]);

            let result = Trie.getWordsUnderString(t, "d");
            Assert.AreEqual(1, Seq.length(result))
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("def")));

        [<Test>]
        let ``when abc and def are added only nothing is returned when g is called``() =
            let t = Trie.Trie(["abc";"def"])

            let result = Trie.getWordsUnderString(t, "g");
            Assert.AreEqual(0, Seq.length(result))

        [<Test>]
        let ``when abc and abcd are added nothing is returned when b is called``() =
            let t = Trie.Trie(["abc";"abcd"])

            let result = Trie.getWordsUnderString(t, "b");
            Assert.AreEqual(0, Seq.length(result))

        [<Test>]
        let ``when aaa and aaa are added only one instance is returned when aa is called``() =
            let t = Trie.Trie(["aaa";"aaa"])

            let result = Trie.getWordsUnderString(t, "aa")
            Assert.AreEqual(1, result |> Seq.length);

        [<Test>]
        let ``when abc and abcde are added only abcde is returned when searching abcd``() =
            let t = Trie.Trie(["abcde";"abc"])

            let result = Trie.getWordsUnderString(t, "abcd")
            Assert.AreEqual(1, result |> Seq.length);
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("abcde")))