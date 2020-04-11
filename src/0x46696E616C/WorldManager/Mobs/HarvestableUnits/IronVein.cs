using _0x46696E616C.MobHandler;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldManager.Mobs.HarvestableUnits
{
    class IronVein : HarvestableUnit, IHarvestable
    {
        public IronVein(Game game, TextureValue texture, IResource type, string name, Microsoft.Xna.Framework.Vector2 size, float totalHealth, float currentHealth, Microsoft.Xna.Framework.Vector2 position, Color color) : base(game, texture, type, name, size, totalHealth, currentHealth, position, color)
        {
        }
    }
}
