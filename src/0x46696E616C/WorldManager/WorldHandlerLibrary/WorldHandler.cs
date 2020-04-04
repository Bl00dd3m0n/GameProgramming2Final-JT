using _0x46696E616C.Buildings;
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
        Map.Map map;
        WorldGeneration wg;
        Game game;
        Save save;
        long Seed;
        public WorldHandler(Game game, string WorldName)
        {
            Seed = 14153456352343;
            regions = new List<Region>();
            wg = new WorldGeneration(game, WorldName, Seed);
            save = Save.save;
            this.game = game;
            if (regions.Count <= 0)
            {
                regions.Add(wg.GenerateRegion(new Vector2(0, 0), new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), 14153456352343));
            }
        }

        public void Place(Building building)
        {

        }

        public bool CheckPlacement(Vector2 position, Microsoft.Xna.Framework.Vector2 size)
        {
            return false;
        }

        public void LoadArea()
        {

        }

        public Tile GetTile(Vector2 position)
        {
            int index = regions.FindIndex(l => l.bounds.Left <= position.X && l.bounds.Right >= position.X && l.bounds.Top <= position.Y && l.bounds.Bottom > position.Y);
            if (index < 0) index = 0;
            Tile returnTile = regions[index].GetTilesAtPosition(position)[0];
            if (returnTile != null)
                return returnTile;
            else
            {
                return null;
            }
            //wg.Get
        }
        public Tile GetBackgroundTile(Vector2 position)
        {
            int index = regions.FindIndex(l => l.bounds.Left <= position.X && l.bounds.Right >= position.X && l.bounds.Top <= position.Y && l.bounds.Bottom > position.Y);
            if (index < 0) index = 0;
            Tile returnTile = regions[index].GetTilesAtPosition(position)[1];
            if (returnTile != null)
                return returnTile;
            //else
            //{
            //    return new Tile(game, TextureValue.Grass, position);
            //}
            return null;
            //wg.Get
        }
    }
}
