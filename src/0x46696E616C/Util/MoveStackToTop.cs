using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Buildings;

namespace Util
{
    public class MoveStackToTop
    {
        public static Stack<Building> Move(Stack<Building> toBuild, Building toTop)
        {
            Stack<Building> newStack = new Stack<Building>();
            List<Building> building = new List<Building>(toBuild);
            int val = building.FindIndex(l => l == toTop);
            building.RemoveAt(val);
            foreach(Building b in building)
            {
                newStack.Push(b);
            }
            newStack.Push(toTop);
            return newStack;
        }
    }
}
