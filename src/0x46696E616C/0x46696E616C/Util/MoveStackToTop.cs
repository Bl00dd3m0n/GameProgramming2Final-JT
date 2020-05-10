using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Buildings;

namespace Util
{
    public class MoveStackToTop<T>
    {
        public static Stack<T> Move(Stack<T> toBuild, T toTop)
        {
            Stack<T> newStack = new Stack<T>();
            List<T> building = new List<T>(toBuild);
            int val = building.FindIndex(l => l.Equals(toTop));
            building.RemoveAt(val);
            foreach(T b in building)
            {
                newStack.Push(b);
            }
            newStack.Push(toTop);
            return newStack;
        }
    }
}
