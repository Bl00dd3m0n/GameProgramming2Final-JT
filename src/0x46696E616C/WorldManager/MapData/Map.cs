using _0x46696E616C.Buildings;
using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using NationBuilder.WorldHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager.TileHandlerLibrary;

namespace WorldManager.MapData
{
    public class Map
    {
        public Vector2 mapSize { get; private set; }
        Tile[,,] tiles;
        long Seed;
        WorldGeneration wg;
        public Map(Game game, Vector2 mapSize, long Seed)
        {
            tiles = new Tile[(int)mapSize.X, (int)mapSize.Y, 2];
            this.mapSize = mapSize;
            this.Seed = Seed;
            wg = new WorldGeneration(game, this.GetType().Name.ToString(), Seed, mapSize);
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

        internal void PlaceBlock(ModifiableTile building, Vector2 position)
        {
           

            for (int y = 0; y <= building.Size.Y ; y++)
            {
                for (int x = 0; x <= building.Size.X; x++)
                {
                    try
                    {
                        if (y == 0 && x == 0)
                        {
                            tiles[(int)position.X + x, (int)position.Y + y, 1] = building;
                        }
                        else
                        {
                            tiles[(int)position.X + x, (int)position.Y + y, 1] = new ReferenceTile((ModifiableTile)tiles[(int)position.X, (int)position.Y, 1]);
                        }
                    } catch(IndexOutOfRangeException ex)
                    {
                        //If the player tries to place a building off the map
                        string error = $"{ex}";
                    }
                }
            }
        }

        public ModifiableTile GetTile(Vector2 position)
        {
            return (ModifiableTile)tiles[(int)position.X, (int)position.Y, 1];
        }
        public Tile GetBackTile(Vector2 position)
        {
            return tiles[(int)position.X, (int)position.Y, 0];
        }

        public BackGroundTile GenerateTerrain(Vector2 position)
        {
            return wg.Biome(position, Seed);
        }

        public Tile GenerateDecor(Vector2 position)
        {
            return wg.AddDecor(tiles[(int)position.X,(int)position.Y,0].block,position.X,position.Y);
        }
    }
}
