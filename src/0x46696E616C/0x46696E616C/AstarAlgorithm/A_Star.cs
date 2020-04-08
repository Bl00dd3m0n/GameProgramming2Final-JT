using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;

namespace _0x46696E616C.AstarAlgorithm
{
    class A_Star
    {
        public List<Vector2> FindPath(Vector2 StartPosition, Vector2 EndPosition, IUnit unit, WorldHandler world)
        {
            List<Vector2> waypoints = new List<Vector2>();
            List<Node> Nodes = new List<Node>();
            Node CurrentNode = new Node(StartPosition, true, 0);
            Node OriginNode;
            Vector2 CurrentPosition = new Vector2();
            float lowestFCost = 100000000;
            if (EndPosition.X >= 0 && EndPosition.X < world.GetSize().X && EndPosition.Y >= 0 && EndPosition.Y < world.GetSize().Y)
            {
                while (CurrentNode.Position != EndPosition)
                {
                    for (int yOff = -1; yOff < 1; yOff++)
                    {
                        for (int xOff = -1; xOff < 1; xOff++)
                        {
                            CurrentPosition = CurrentNode.Position + new Vector2(xOff, yOff);
                            Nodes.Add(new Node(CurrentPosition, false, FCost(GCost(StartPosition, CurrentPosition), HCost(EndPosition, CurrentPosition))));
                        }
                    }
                    foreach (Node node in Nodes)
                    {
                        if (node.fCost < lowestFCost && world.CheckPlacement(node.Position, unit.Size))
                        {
                            CurrentNode = node;
                            node.Closed = true;
                            lowestFCost = node.fCost;
                        }
                    }
                }
            }
            foreach()
            return waypoints;
        }

        /// <summary>
        /// Distance from the Start
        /// </summary>
        /// <returns>G cost</returns>
        private float GCost(Vector2 start, Vector2 currentPosition)
        {

            return (float)Math.Sqrt((Math.Abs(start.X) + Math.Abs(currentPosition.X)) + (Math.Abs(start.Y) + Math.Abs(currentPosition.Y))) * 10;
        }
        /// <summary>
        /// Distance from the end
        /// </summary>
        /// <returns>H Cost</returns>
        private float HCost(Vector2 end, Vector2 currentPosition)
        {
            return (float)Math.Sqrt((Math.Abs(end.X) + Math.Abs(currentPosition.X)) + (Math.Abs(end.Y) + Math.Abs(currentPosition.Y))) * 10;
        }

        /// <summary>
        /// Total "Cost" to get from point a to b
        /// </summary>
        /// <param name="G">G Cost</param>
        /// <param name="H">H Cost</param>
        /// <returns>F Cost</returns>
        public float FCost(float G, float H)
        {
            return G + H;
        }
    }
}
