using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maize;
using System.Collections.Generic;
using System.Collections;

namespace MaizeBackendTests
{
    [TestClass]
    public class MaizeBackendTests
    {
        #region MST

        //Test to see that we can reach the expected number of nodes in our graph
        [TestMethod]
        public void GraphIsSpanning()
        {
            MazeGraph testGraph;
            HashSet<MazeNode> nodes;
            for (int size = 1; size < 10; size++)
            {
                testGraph = new MazeGraph(size);
                //Search the graph, retrieving all accessible nodes
                nodes = testGraph.BreadthFirstSearch(testGraph.graph[0, 0, 0]);
                Assert.AreEqual(nodes.Count, Math.Pow(size, 3));
            }
        }


        //An MST has the following property:
        //|edges| = |nodes| - 1
        [TestMethod]
        public void GraphIsMinimum()
        {
            MazeGraph testGraph;
            ArrayList adjacencyLists = new ArrayList();
            int edgeCount = 0;
            int minimumEdges;
            for (int size = 1; size < 10; size++)
            {
                edgeCount = 0;
                testGraph = new MazeGraph(size);
                foreach (MazeNode current in testGraph.graph)
                {
                    edgeCount += current.getAdjacentEdges().Count;
                    adjacencyLists.Add(current.getAdjacentEdges());
                }

                edgeCount = edgeCount / 2;
                minimumEdges = (int)(Math.Pow(size, 3) - 1);
                Assert.AreEqual(edgeCount, minimumEdges);
            }
        }
        #endregion

        #region Astar

        
        //Test for the existence (non-null) of a path between each possible node-pair
        //WARNING: Test is **SLOW** for n < 10. This test generates n^6 pairwise paths
        //Running with n < 10 ran on my computer for nearly 20 minutes before passing :)
        [TestMethod]
        public void APathExists()
        {
            MazeGraph testGraph;
            for (int size = 1; size < 10; size++)
            {
                testGraph = new MazeGraph(size);

                for (int x = 0; x < testGraph.graph.GetLength(0); x++)
                {
                    for (int y = 0; y < testGraph.graph.GetLength(1); y++)
                    {
                        for (int z = 0; z < testGraph.graph.GetLength(2); z++)
                        {
                            foreach (MazeNode current in testGraph.graph)
                            {
                                Assert.IsNotNull(testGraph.AStarSearch(current, testGraph.graph[x, y, z]));
                            }
                        }
                    }
                }
            }

        }
        #endregion
    }
}
