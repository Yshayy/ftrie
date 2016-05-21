namespace FTrie.Tests
    open NUnit.Framework

    [<TestFixture>]
    module WithWordTests = 
            
        [<Test>]
        let ``when adding a word with no letters in common with existing words it is added successfully``() =
            let t1 = Trie.Trie(["abc"])
            let t2 = t1.withWord("def");

            let t1List = List.ofSeq (t1.getWords())
            let t2List = List.ofSeq (t2.getWords()) |> List.sort

            Assert.AreEqual(["abc"], t1List)
            Assert.AreEqual(["abc";"def"], t2List)

        [<Test>]
        let ``when adding a word with first letter in common with existing words it is added successfully``() =
            let t1 = Trie.Trie(["abc"])
            let t2 = t1.withWord("ade");

            let t1List = List.ofSeq (t1.getWords()) 
            let t2List = List.ofSeq (t2.getWords()) |> List.sort

            Assert.AreEqual(["abc"], t1List)
            Assert.AreEqual(["abc";"ade"], t2List)

        [<Test>]
        let ``when adding a word with first 2 letters in common with existing words it is added successfully``() =
            let t1 = Trie.Trie(["abc"])
            let t2 = t1.withWord("abd");

            let t1List = List.ofSeq (t1.getWords()) 
            let t2List = List.ofSeq (t2.getWords()) |> List.sort

            Assert.AreEqual(["abc"], t1List)
            Assert.AreEqual(["abc";"abd"], t2List)

        [<Test>]
        let ``when adding a word with all letters in common with existing words it is not duplicated``() =
            let t1 = Trie.Trie(["abc"])
            let t2 = t1.withWord("abc");

            let t1List = List.ofSeq (t1.getWords()) 
            let t2List = List.ofSeq (t2.getWords()) |> List.sort

            Assert.AreEqual(["abc"], t1List)
            Assert.AreEqual(["abc"], t2List)