using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Maize
{
   public class MazeGraph
    {
		public MazeNode[, ,] graph { get; set; }
        public MazeNode start { get; set; }
        public MazeNode goal { get; set; }
        ArrayList edges;

        public MazeGraph(int size)
        {
            //Allocate space for graph
            this.graph = new MazeNode[size, size, size];

            //Set of edges for Kruskal's algorithm
            edges = new ArrayList();

            //Initializa all the nodes, giving XYZ coordinates
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        //Create the nodes
                        graph[x, y, z] = new MazeNode(x, y, z);
                    }
                }
            }
            generateEdges(size);
            generateMaze(size);

            //Arbitrarily define the start of our maze at 0, 0, 0
            start = graph[0, 0, 0];

            //Search the graph, maintaining an order of nodes we reached
            List<MazeNode> orderedVertices = BreadthFirstSearch(start);
            //Our goal node is the last one reached by BFS
            goal = orderedVertices[orderedVertices.Count - 1];

        }

        //Connect nodes to form a MST over the graph
        //A MST is our maze, with guaranteed access to every node and no loops.
        private void generateMaze(int size)
        {


            //TODO debug - few redundant edges.
            Random rand = new Random();
            int edgeIndex;
            MazeEdge currentEdge;
            int connections = 0;
            while (connections < Math.Pow(size, 3) - 1)
            {
                //Select an edge
                edgeIndex = rand.Next(edges.Count);
                currentEdge = (MazeEdge)edges[edgeIndex];

                if (!(currentEdge.joinedNodes[0].isConnected(currentEdge.joinedNodes[1])))
                {
                    //join the two nodes
                    currentEdge.joinedNodes[0].connect(currentEdge.joinedNodes[1]);
                    connections++;
                    edges.Remove(currentEdge);
                }
            }
        }

        private bool allEdgesConnected()
        {
            for (int x = 0; x < graph.GetLength(0); x++)
            {
                for (int y = 0; y < graph.GetLength(1); y++)
                {
                    for (int z = 0; z < graph.GetLength(2); z++)
                    {
                        foreach (MazeNode current in graph)
                        {
                            if(!current.isConnected(graph[x, y, z]))
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void generateEdges(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        //Add edges to a set for consideration. Nodes are not yet connected.
                        if (x > 0)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x - 1, y, z]));
                        }

                        if (y > 0)
                        {
                            edges.Add(new MazeEdge(graph[x, y, z], graph[x, y - 1, z]));
                        }

                        if (z > 0)
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
            
            while (open.Count > 0)
            {
                current = open.Min();
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

                        if(!open.Contains(child))
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

        public int getOptimalSolutionLength()
        {

        }
    }
}