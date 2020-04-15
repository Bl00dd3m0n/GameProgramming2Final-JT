using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.AstarAlgorithm
{
    class A_Star
    {

        /// <summary>
        /// A* Algorithm implementation
        /// </summary>
        /// <param name="StartPosition"></param>
        /// <param name="EndPosition"></param>
        /// <param name="world"></param>
        /// <returns></returns>
        public List<Vector2> FindPath(Vector2 StartPosition, Vector2 EndPosition, WorldHandler world)
        {
            //Make the start and the end whole numbers - without this it leads to errors of not finding the end
            StartPosition = StartPosition.ToPoint().ToVector2();
            EndPosition = EndPosition.ToPoint().ToVector2();
            //List of waypoints to find the end
            List<Vector2> waypoints = new List<Vector2>();
            //Nodes open and closed
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            //Start node is the start position
            Node Starter = new Node(StartPosition, FCost(GCost(StartPosition, StartPosition), HCost(EndPosition, StartPosition)));
            Node CurrentNode = Starter;
            open.Add(Starter);
            //Set when arrived at the end
            Node EndNode = null;
            //Only finds the path if you click within the world
            bool OnMap = world.Contains(EndPosition);
            int i = 0;

            //while you have open nodes, the i value is within a certain limit to make this not crash my game when the unit tries to find the end. I also check if it's on the map and that you're not already where you clicked.
            while (open.Count > 0 && i < Math.Pow((EndPosition.X-StartPosition.X),2) * Math.Pow((EndPosition.Y - StartPosition.Y),2) && OnMap && StartPosition != EndPosition)
            {
                i++;
                CurrentNode = open[0];
                //Find the lowest values that isn't already occupied
                foreach (Node node in open)
                {
                    if (node.fCost < CurrentNode.fCost && world.CheckPlacement(node.Position, new Vector2(1)))
                    {
                        CurrentNode = node;
                    }
                }
                //Remove the lowest node from the open list
                open.Remove(CurrentNode);
                //Generate children around the current node that are empty tiles
                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        Vector2 CurrentPos = CurrentNode.Position + new Vector2(x, y);
                        if ((world.CheckPlacement(CurrentPos, new Vector2(1)) || CurrentNode.Position != StartPosition) && world.GetUnit(CurrentPos) == null && CurrentPos != CurrentNode.Position)//To avoid the entire game breaking if you spawn something on a block CurrentPos != StartPosition needs to be implemented
                        {
                            Node tempNode = new Node(CurrentPos, FCost(GCost(StartPosition, CurrentPos), HCost(EndPosition, CurrentPos)));
                            tempNode.Parent = CurrentNode;
                            CurrentNode.Child[(x + 1) + ((y + 1) * 3)] = tempNode;
                        }
                    }
                }
                
                foreach (Node node in CurrentNode.Child)
                {
                    //If the child node isn't equal to the parent node and it's not null
                    if (node != CurrentNode && node != null)
                    {
                        //If the child is at the end, set the end node clear the list of open nodes and break the loop.
                        if (node.Position == EndPosition)
                        {
                            open.Clear();
                            EndNode = node;
                            break;
                        }
                        //By default add the child node to the list of open nodes
                        bool AddNode = true;
                        //for each open node
                        foreach (Node openNode in open)
                        {
                            //If the node is equal to an existing open node
                            if (openNode.Position == node.Position)
                            {
                                //If the added node would be less than the openNode cost don't add it
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
                        //for each closed node
                        foreach (Node closedNode in closed)
                        {
                            //If the node is equal to an existing closed node
                            if (closedNode.Position == node.Position)
                            {
                                //Don't add this node
                                AddNode = false;
                                //if the closed node has a lower fCost than the current node add the selected closed node back to the open node
                                if (node.fCost < closedNode.fCost)
                                {
                                    open.Add(closedNode);
                                    heldNode = closedNode;
                                    break;
                                }
                            }
                        }
                        //Remove the selected closed node from the closed list
                        if (heldNode != null) closed.Remove(heldNode);
                        if (AddNode)
                        {
                            open.Add(node);
                        }
                    }
                }
                closed.Add(CurrentNode);
            }
            //If the end node isn't null loop until the end node is null 
            if (EndNode != null)
            {
                while (EndNode != null)
                {
                    //Add the end node position to the waypoint list
                    waypoints.Add(EndNode.Position);
                    //Set the end node to it's parent
                    EndNode = EndNode.Parent;
                }
            }
            //Since the waypoints start from the end return it reversed so the unit has the proper order of waypoints
            waypoints.Reverse();
            return waypoints;
        }

        /// <summary>
        /// Distance from the Start
        /// </summary>
        /// <returns>G cost</returns>
        private float GCost(Vector2 start, Vector2 currentPosition)
        {
            return (float)Math.Sqrt(Math.Pow(start.X - currentPosition.X, 2) + Math.Pow(start.Y - currentPosition.Y, 2)) * 10;
        }
        /// <summary>
        /// Distance from the end
        /// </summary>
        /// <returns>H Cost</returns>
        private float HCost(Vector2 end, Vector2 currentPosition)
        {
            return (float)Math.Sqrt(Math.Pow(end.X - currentPosition.X, 2) + Math.Pow(end.Y - currentPosition.Y, 2)) * 10;
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
