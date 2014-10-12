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
            //MazeGraph test = new MazeGraph(2);

            for (int size = 1; size < 10; size++)
            {
               MazeGraph  testGraph = new MazeGraph(3);
            }

            Console.ReadLine();
        }
    }
}
