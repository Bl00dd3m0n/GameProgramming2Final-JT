using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.TileHandlerLibrary;
using WorldManager.MapData;

namespace _0x46696E616C.WorldManager.MapData
{
    class TutorialMap : Map
    {
        public TutorialMap(Game game, Vector2 mapSize, long Seed) : base(game, mapSize, Seed)
        {
        }

        public override void GenerateMap(GraphicsDevice gd)
        {
            for(int y = 0; y < mapSize.X; y++)
            {
                for (int x = 0; x < mapSize.Y; x++)
                {
                    map[x, y, 0] = new Tile(TextureValue.Grass, new Vector2(x, y), Color.White);
                }
            }
            DrawMapUI(gd);
        }
    }
}
