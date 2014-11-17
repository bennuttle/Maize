using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maize
{
    public class MazeGraph
    {
        private MazeNode[, ,] graph { get; set; }
        public MazeNode start { get; set; }
        public MazeNode goal { get; set; }
        private ArrayList edges;

        public int sizeX { get; set; }
        public int sizeY { get; set; }
        public int sizeZ { get; set; }

        public MazeGraph(int dimX, int dimY, int dimZ)
        {
            // If bad people give us bad input, fix it.
            // Code does not respond happily to zero / negative
            // values for a dimension. What would such a maze look like?
            dimX = dimX < 1 ? 1 : dimX;
            dimY = dimY < 1 ? 1 : dimY;
            dimZ = dimZ < 1 ? 1 : dimZ;

            sizeX = dimX;
            sizeY = dimY;
            sizeZ = dimZ;
            //Allocate space for graph
            this.graph = new MazeNode[dimX, dimY, dimZ];

            //Set of edges for Kruskal's algorithm
            edges = new ArrayList();

            //Initializa all the nodes, giving XYZ coordinates
            for (int x = 0; x < dimX; x++)
            {
                for (int y = 0; y < dimY; y++)
                {
                    for (int z = 0; z < dimZ; z++)
                    {
                        //Create the nodes
                        graph[x, y, z] = new MazeNode(x, y, z);
                    }
                }
            }

            // Create our set of edges to be considered.
            // Taking all of these edges would make a complete graph
            generateEdges(dimX, dimY, dimZ);

            // Run Kruskal's MST algorithm
            // Graph connections are stored at the node level
            generateMaze(dimX, dimY, dimZ);

            //Arbitrarily define the start of our maze at 0, 0, 0
            start = graph[0, 0, 0];

            //Search the graph, maintaining an order of nodes we reached
            List<MazeNode> orderedVertices = BreadthFirstSearch(start);
            //Our goal node is the last one reached by BFS
            goal = orderedVertices[orderedVertices.Count - 1];

        }

        public MazeNode this[int x, int y, int z]
        {
            get { return graph[x, y, z]; }
        }

        public MazeNode[, ,] getAllNodes()
        {
            return graph;
        }

        //Connect nodes to form a MST over the graph
        //A MST is our maze, with guaranteed access to every node and no loops.
        private void generateMaze(int dimX, int dimY, int dimZ)
        {
            Random rand = new Random();
            int edgeIndex;
            MazeEdge currentEdge;
            int connections = 0;
            while (connections < dimX * dimY * dimZ - 1)
            {
                //Select an edge
                edgeIndex = rand.Next(edges.Count);
                currentEdge = (MazeEdge)edges[edgeIndex];

                if (!(currentEdge[0].isConnected(currentEdge[1])))
                {
                    //join the two nodes
                    currentEdge[0].connect(currentEdge[1]);
                    connections++;
                    edges.Remove(currentEdge);
                }
            }
        }

        private void generateEdges(int dimX, int dimY, int dimZ)
        {

            for (int x = 0; x < dimX; x++)
            {

                for (int y = 0; y < dimY; y++)
                {
                    for (int z = 0; z < dimZ; z++)
                    {
                        //Add edges to a set for consideration. Nodes are not yet connected.
                        if (x > 0 && dimX > 1)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x - 1, y, z]));
                        }

                        if (y > 0 && dimY > 1)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x, y - 1, z]));
                        }

                        if (z > 0 && dimZ > 1)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x, y, z - 1]));
                        }
                    }

                }

            }

        }

        public List<MazeNode> BreadthFirstSearch(MazeNode root)
        {
            List<MazeNode> orderedVertices = new List<MazeNode>();
            Queue nodes = new Queue();
            ArrayList children;

            orderedVertices.Add(root);
            nodes.Enqueue(root);

            MazeNode current;

            while (nodes.Count > 0)
            {
                current = (MazeNode)nodes.Dequeue();
                children = current.getAdjacentEdges();

                foreach (MazeNode child in children)
                {
                    if (!orderedVertices.Contains(child))
                    {
                        orderedVertices.Add(child);
                        nodes.Enqueue(child);
                    }
                }
            }
            return orderedVertices;
        }


        // Returns a stack of nodes representing the path from start to goal.
        // Popping elements off the stack yields the correct sequence of moves
        // in order.
        public Stack<MazeNode> AStarSearch(MazeNode start, MazeNode goal)
        {
            HashSet<MazeNode> closed = new HashSet<MazeNode>();
            HashSet<MazeNode> open = new HashSet<MazeNode>();
            MazeNode current;
            ArrayList children;
            int GScoreEstimate;
            start.g = 0;
            start.h = start.manhattanDistance(goal);
            start.f = start.g + start.h;
            start.a_star_parent = start;
            open.Add(start);

            //While there are adjacent, unexplored nodes to search
            while (open.Count > 0)
            {
                //Consider the 'best' node, as weighted by f-score
                current = open.Min();
                //If we've reached our goal
                if (current == goal)
                {
                    //Retrieve the path we took via a stack
                    Stack<MazeNode> path_to_goal = new Stack<MazeNode>();
                    while (current.a_star_parent != current)
                    {
                        path_to_goal.Push(current);
                        current = current.a_star_parent;
                    }
                    path_to_goal.Push(current);
                    return path_to_goal;
                }

                //Else continue our search
                open.Remove(current);
                closed.Add(current);
                children = current.getAdjacentEdges();
                foreach (MazeNode child in children)
                {
                    // If we've visited this node already, skip it.
                    if (closed.Contains(child))
                    {
                        continue;
                    }

                    // g is computed as the cost it took us to get here, plus the distance between the
                    // current node and the child we're considering.
                    GScoreEstimate = (int)(current.g) + current.manhattanDistance(child);

                    // If we haven't considered this node already, or our current estimate is more optimistic
                    // than our prior estimation
                    if (!open.Contains(child) || GScoreEstimate < child.g)
                    {
                        child.g = GScoreEstimate;
                        child.f = child.g + child.manhattanDistance(goal);

                        if (!open.Contains(child))
                        {
                            open.Add(child);
                        }

                        child.a_star_parent = current;
                    }
                }
                closed.Add(current);
            }
            //Search failed, no more nodes to find on open list
            return null;
        }

        //The length of the optimal solution is the number of nodes returned from A*
        //Use this method for considering the shortest solution for this MazeGraph
        public int getOptimalSolutionLength()
        {
            return AStarSearch(start, goal).Count;
        }

        //The length of the optimal solution is the number of nodes returned from A*
        //Use this method for considering the shortest solution for this MazeGraph
        //from the provided node (for example, where the player currently is)
        public int getOptimalSolutionLength(MazeNode current)
        {
            return AStarSearch(current, goal).Count;
        }
    }
}