using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Maize
{
    class Program
    {
        static void Main(string[] args)
        {
            //MazeGraph testGraph = new MazeGraph(3, 3, 3);
            //testGraph.BreadthFirstSearch(testGraph[0, 0, 0]);

            Player test = Player.loadPlayer("C:\\Users\\Ben_2\\Desktop\\player.xml");

            test.stepsTaken++;

            test.savePlayer("C:\\Users\\Ben_2\\Desktop\\player.xml");

            Console.ReadLine();
        }
    }
}
