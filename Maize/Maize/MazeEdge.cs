using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maize
{
    class MazeEdge
    {
        public MazeNode[] joinedNodes {get; set;}

        public MazeEdge(MazeNode first, MazeNode second)
        {
            joinedNodes = new MazeNode[2] {first, second};
        }
    }
}
