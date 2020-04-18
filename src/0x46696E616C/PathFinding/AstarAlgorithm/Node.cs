using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.AstarAlgorithm
{
    class Node
    {
        public float fCost { get; private set; }
        public Vector2 Position { get; private set; }
        public Node Parent;
        public Node[] Child;

        public Node(Vector2 startPosition, float fCost)
        {
            Child = new Node[9];
            this.Position = startPosition;
            this.fCost = fCost;
        }
    }
}
