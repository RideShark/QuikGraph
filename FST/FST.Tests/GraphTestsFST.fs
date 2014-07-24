﻿module GraphTestsFST

open YC.FST.TableFST
open NUnit.Framework
open Microsoft.FSharp.Collections
open YC.FST.GraphBasedFST
open YC.FST.TestData

let basePath = "../../../../DOTfst/"
let fullPath f = System.IO.Path.Combine(basePath, f)

[<TestFixture>]
type ``Graph FST tests`` () =    
    let checkGraph (fst:FST<_,_>) initV finalV countE countV filePath =
        Assert.AreEqual(fst.InitState.Count, initV, "Count of init state not equal expected number.")
        Assert.AreEqual(fst.FinalState.Count, finalV, "Count of final state not equal expected number.")
        Assert.AreEqual(fst.EdgeCount, countE, "Count of edges not equal expected number. ")
        Assert.AreEqual(fst.VertexCount, countV, "Count of vertices not equal expected number. ")
        fst.PrintToDOT <| fullPath filePath
        

    [<Test>]
    member this.``Graph FST. Simple test.`` () =
        checkGraph fst1 1 1 4 4 "simple_test_graph.dot"

    [<Test>]
    member this.``Graph FST. Concat test.`` () =
        let resFST = fst1.Concat fst2
        checkGraph resFST 1 1 9 8 "concat_test_graph.dot"


    [<Test>]
    member this.``Graph FST. Concat test with multi finit and init.`` () =
        let resFST = multiInitfst.Concat multiFinishfst
        checkGraph resFST 2 3 12 10 "concat_test_graph_multi.dot"

    [<Test>]
    member this.``Graph FST. Concat test with multi finit and init 1.`` () =
        let resFST = multiFinishfst.Concat multiInitfst
        checkGraph resFST 1 1 15 10 "concat_test_graph_multi_1.dot"
        
    [<Test>]
    member this.``Graph FST. Union test.`` () =
        let resFST = fst1.Union fst2
        checkGraph resFST 1 1 11 9 "union_test_graph.dot"

    [<Test>]
    member this.``Graph FST. Union test with multi finit and init.`` () =
        let resFST = multiFinishfst.Union multiInitfst
        checkGraph resFST 1 1 17 11 "union_test_graph_multi.dot"

    [<Test>]
    member this.``Graph FST. Test composition FSTs.`` () =
        let resFST = FST.Compos(fstCompos1, fstCompos2)
        checkGraph resFST 1 1 11 8 "compos_test.dot"

    [<Test>]
    member this.``Graph FST. Test composition FSTs 1.`` () =
        let resFST = FST.Compos(fstCompos12, fstCompos22)
        checkGraph resFST 1 1 4 4 "compos_test_1.dot"

//[<EntryPoint>]
//let f x =
//      let t = new ``Graph FST tests`` () 
//      t.``Graph FST. Test composition FSTs.``()
//      1