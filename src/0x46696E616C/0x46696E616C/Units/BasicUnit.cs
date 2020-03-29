using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern.Commands
{
    class BasicUnit : IDamagable
    {
        string Name;
        Vector2 Position;

        public float health { get; private set; }

        public void Damage(int amount)
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}
