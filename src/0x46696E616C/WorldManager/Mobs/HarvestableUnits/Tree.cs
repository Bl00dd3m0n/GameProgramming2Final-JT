using _0x46696E616C;
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
    class Tree : HarvestableUnit, IHarvestable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="totalHealth">Number of resources</param>
        /// <param name="currentHealth">Current amount of resource held</param>
        /// <param name="position"></param>
        /// <remarks>The health values should be randomized most likely to create a more realistic number of resources</remarks>
        public Tree(Game game, TextureValue texture, IResource type, string name, Microsoft.Xna.Framework.Vector2 size, float totalHealth, float currentHealth, Microsoft.Xna.Framework.Vector2 position, Color color) : base(game, texture, type, name, size, totalHealth, currentHealth, position, color)
        {
        }
    }
}
