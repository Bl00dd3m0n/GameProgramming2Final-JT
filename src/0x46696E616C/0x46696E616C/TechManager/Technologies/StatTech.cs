using _0x46696E616C.TechManager.Stats;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.TechManager.Technologies
{
    public class StatTech : Technology
    {
        public Stat Upgrade { get; protected set; }


        public StatTech(Stat upgrade, TextureValue icon, Vector2 position) : base(icon, position)
        {
            Upgrade = upgrade;
        }
    }
}
