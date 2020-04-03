using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using NationBuilder.WorldHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WorldManager
{
    public class WorldHandler
    {
        List<Region> regions;
        WorldGeneration wg;
        Game game;
        Save save;
        long Seed;
        public WorldHandler(Game game, string WorldName)
        {
            Seed = 14153456352343;
            regions = new List<Region>();
            wg = new WorldGeneration(game, WorldName);
            save = Save.save;
            this.game = game;
            if(regions.Count <= 0)
            {
                regions.Add(wg.GenerateRegion(new NationBuilder.TileHandlerLibrary.Vector2(0,0), new NationBuilder.TileHandlerLibrary.Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), 14153456352343));
            }
        }
        public void LoadArea()
        {
            
        }

        public Tile GetTile(NationBuilder.TileHandlerLibrary.Vector2 position)
        {
            int index = regions.FindIndex(l => l.bounds.Left <= position.x && l.bounds.Right >= position.x && l.bounds.Top <= position.y && l.bounds.Bottom > position.y);
            if (index < 0) index = 0;
            Tile returnTile = regions[index].tiles[(int)(position.x + (regions[index].bounds.Width * position.y))];
            if(returnTile != null)
                return returnTile;
            else
            {
                return new Tile(TextureValue.Grass, position);
            }
            //wg.Get
        }
    }
}
