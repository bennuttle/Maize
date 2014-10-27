using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Maize
{
    class MazeEdge
    {
        private MazeNode[] joinedNodes {get; set;}

        public MazeEdge(MazeNode first, MazeNode second)
        {
            joinedNodes = new MazeNode[2] {first, second};
        }

        public MazeNode this[int index]
        {
            get { return joinedNodes[index]; }
        }
    }
}
