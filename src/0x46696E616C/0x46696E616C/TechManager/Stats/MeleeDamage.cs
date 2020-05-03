using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.TechManager.Stats
{
    public class MeleeDamage : Stat
    {
        public MeleeDamage(string name, float value) : base(name, value)
        {
            Texture = TextureValue.Damage;
        }
    }
}
