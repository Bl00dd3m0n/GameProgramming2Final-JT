using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using NationBuilder.WorldHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager.TileHandlerLibrary;

namespace WorldManager.Map
{
    public class Map
    {
        Vector2 mapSize;
        Tile[,,] tiles;
        long Seed;
        WorldGeneration wg;
        public Map(Vector2 mapSize, long Seed)
        {
            tiles = new Tile[(int)mapSize.X, (int)mapSize.Y, 2];
            this.Seed = Seed;
        }
        public void GenerateMap()
        {
            
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    tiles[x, y, 0] = GenerateTerrain(new Vector2(x,y));
                    tiles[x, y, 1] = GenerateDecor(new Vector2(x, y));
                }
            }
        }

        public BackGroundTile GenerateTerrain(Vector2 position)
        {
            return wg.Biome(position, Seed);
        }

        public Tile GenerateDecor(Vector2 position)
        {
            return wg.AddDecor(tiles[(int)position.X,(int)position.Y,0].block,position.X,position.Y,wg.noise    ());
        }
    }
}
