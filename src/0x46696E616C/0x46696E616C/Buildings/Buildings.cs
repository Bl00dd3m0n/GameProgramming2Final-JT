using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _0x46696E616C.Buildings
{
    class Buildings : IUnit
    {
        public string name { get; private set; }

        public float health { get; private set; }

        public Vector2 Position { get; private set; }

        public Vector2 Size { get; private set; }

        public void Damage(int amount)
        {
            health -= amount;
        }

        public void Destroy()
        {
            
        }
    }
}
