namespace FTrie.Tests
    open NUnit.Framework

    [<TestFixture>]
    module ContainsTests = 
            
        [<Test>]
        let ``construct with word then check for word returns true``() =
            let word = "abc"
            let t = Trie.Trie([word]);
            Assert.IsTrue(t.contains(word))

        [<Test>]
        let ``construct with word and other words then check for word returns true``() =
            let word = "abc"
            let t = Trie.Trie([word;"def";"ghi"]);
            Assert.IsTrue(t.contains(word))

        [<Test>]
        let ``construct without word then check for word returns false``() =
            let word = "abc"
            let t = Trie.Trie(["def";"ghi"]);
            Assert.IsFalse(t.contains(word))

        [<Test>]
        let ``check for empty word returns false``() =
            let word = ""
            let t = Trie.Trie(["def";"ghi"]);
            Assert.IsFalse(t.contains(word))

        [<Test>]
        let ``check for empty even when empty word explicitly added returns false``() =
            let word = ""
            let t = Trie.Trie([word;"def";"ghi"]);
            Assert.IsFalse(t.contains(word))