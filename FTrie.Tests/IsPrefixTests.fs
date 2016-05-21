namespace FTrie.Tests
    open NUnit.Framework

    [<TestFixture>]
    module IsPrefixTests = 
            
        [<Test>]
        let ``use valid prefix one letter should return true``() =
            let words = ["abc"]
            let t = Trie.Trie(words);
            Assert.IsTrue(t.isPrefix("a"))

        [<Test>]
        let ``use valid prefix two letters should return true``() =
            let words = ["abc"]
            let t = Trie.Trie(words);
            Assert.IsTrue(t.isPrefix("ab"))

        [<Test>]
        let ``use valid prefix whole word should return true``() =
            let words = ["abc"]
            let t = Trie.Trie(words);
            Assert.IsTrue(t.isPrefix("abc"))

        [<Test>]
        let ``use invalid prefix whole word should return false``() =
            let words = ["abc"]
            let t = Trie.Trie(words);
            Assert.IsFalse(t.isPrefix("def"))

        [<Test>]
        let ``use empty prefix whole word should return true``() =
            let words = ["abc"]
            let t = Trie.Trie(words);
            Assert.IsTrue(t.isPrefix(""))