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
        public List<Vector2> FindPath(Vector2 StartPosition, Vector2 EndPosition, WorldHandler world)
        {
            StartPosition = StartPosition.ToPoint().ToVector2();
            List<Vector2> waypoints = new List<Vector2>();
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            Node Starter = new Node(StartPosition, FCost(GCost(StartPosition, StartPosition), HCost(EndPosition, StartPosition)));
            Node CurrentNode = Starter;
            open.Add(Starter);
            Node EndNode = null;
            bool OnMap = world.Contains(EndPosition);
            int i = 0;
            while (open.Count > 0 && i < world.GetSize().X + world.GetSize().Y && OnMap)
            {
                i++;
                CurrentNode = open[0];
                foreach (Node node in open)
                {
                    if (node.fCost < CurrentNode.fCost && world.CheckPlacement(node.Position, new Vector2(1)))
                    {
                        CurrentNode = node;
                    }
                }
                open.Remove(CurrentNode);

                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        Vector2 CurrentPos = CurrentNode.Position + new Vector2(x, y);
                        if (world.CheckPlacement(CurrentPos, new Vector2(1)) && CurrentPos != CurrentNode.Position)
                        {
                            Node tempNode = new Node(CurrentPos, FCost(GCost(StartPosition, CurrentPos), HCost(EndPosition, CurrentPos)));
                            tempNode.Parent = CurrentNode;
                            CurrentNode.Child[(x + 1) + ((y + 1) * 3)] = tempNode;
                        }
                    }
                }
                //    d) for each successor
                foreach (Node node in CurrentNode.Child)
                {
                    if (node != CurrentNode && node != null)
                    {
                        if (node.Position == EndPosition)
                        {
                            open.Clear();
                            EndNode = node;
                            break;
                        }
                        bool AddNode = true;
                        foreach (Node openNode in open)
                        {
                            if (openNode.Position == node.Position)
                            {
                                if (node.fCost >= openNode.fCost)
                                {
                                    AddNode = false;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        Node heldNode = null;
                        foreach (Node closedNode in closed)
                        {
                            if (closedNode.Position == node.Position)
                            {
                                if (node.fCost >= closedNode.fCost)
                                {
                                    AddNode = false;
                                    break;
                                }
                                else
                                {
                                    AddNode = false;
                                    open.Add(closedNode);
                                    heldNode = closedNode;
                                    break;
                                }
                            }
                        }
                        if (heldNode != null) closed.Remove(heldNode);
                        if (AddNode)
                        {
                            open.Add(node);
                        }
                    }
                }
                closed.Add(CurrentNode);
            }
            if (EndNode != null)
            {
                while (EndNode != null)
                {
                    waypoints.Add(EndNode.Position);
                    EndNode = EndNode.Parent;
                }
            }
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
