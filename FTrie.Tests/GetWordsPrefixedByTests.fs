namespace FTrie.Tests
    open NUnit.Framework
    open Trie

    [<TestFixture>]
    module GetWordsPrefixedByTests = 

        [<Test>]
        let ``when abc and abcd are added they both return when ab is called``() = 
            let t = Trie(["abc";"abcd"])

            let result = t.getWordsPrefixedBy("ab");
            Assert.AreEqual(2, Seq.length(result))
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("abc")))
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("abcd")))
            
        [<Test>]
        let ``when abc and def are added only def is returned when d is called``() =
            let t = Trie(["abc";"def"]);

            let result = t.getWordsPrefixedBy("d");
            Assert.AreEqual(1, Seq.length(result))
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("def")));

        [<Test>]
        let ``when abc and def are added only nothing is returned when g is called``() =
            let t = Trie(["abc";"def"])

            let result = t.getWordsPrefixedBy("g");
            Assert.AreEqual(0, Seq.length(result))

        [<Test>]
        let ``when abc and abcd are added nothing is returned when b is called``() =
            let t = Trie(["abc";"abcd"])

            let result = t.getWordsPrefixedBy("b");
            Assert.AreEqual(0, Seq.length(result))

        [<Test>]
        let ``when aaa and aaa are added only one instance is returned when aa is called``() =
            let t = Trie(["aaa";"aaa"])

            let result = t.getWordsPrefixedBy("aa")
            Assert.AreEqual(1, result |> Seq.length);

        [<Test>]
        let ``when abc and abcde are added only abcde is returned when searching abcd``() =
            let t = Trie(["abcde";"abc"])

            let result = t.getWordsPrefixedBy("abcd")
            Assert.AreEqual(1, result |> Seq.length);
            Assert.IsTrue(result |> Seq.exists(fun s -> s.Equals("abcde")))