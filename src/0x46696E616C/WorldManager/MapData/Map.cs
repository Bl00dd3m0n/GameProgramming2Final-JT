using _0x46696E616C.Buildings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.DataHandlerLibrary;
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
        public Texture2D mapTexture { get; private set; }
        long Seed;
        WorldGeneration wg;
        public Map(Game game, Vector2 mapSize, long Seed)
        {
            tiles = new Tile[(int)mapSize.X, (int)mapSize.Y, 2];
            this.mapSize = mapSize;
            this.Seed = Seed;
            wg = new WorldGeneration(game, this.GetType().Name.ToString(), Seed, mapSize);
        }
        public void GenerateMap(GraphicsDevice gd)
        {
            Color[] colors = new Color[(int)((mapSize.X * mapSize.Y)/4)];
            Color[] tileImage = new Color[16*16]; 
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    tiles[x, y, 0] = GenerateTerrain(new Vector2(x, y));
                    tiles[x, y, 1] = GenerateDecor(new Vector2(x, y));
                }
            }
            /*for (int y = 0; y < mapSize.Y/2; y++)
            {
                for (int x = 0; x < mapSize.X/2; x++)
                {
                    if (tiles[x, y, 1] != null) ContentHandler.DrawnTexture(tiles[x * 2, y * 2, 0].block.texture).GetData(tileImage);
                    else ContentHandler.DrawnTexture(tiles[x * 2, y * 2, 0].block.texture).GetData(tileImage);
                    colors[(int)((x) + (y) * (mapSize.X / 2))] = tileImage[(int)(tileImage.Length / 2)];
                }
            }
            mapTexture = new Texture2D(gd, (int)mapSize.X/2, (int)mapSize.Y/2);
            mapTexture.SetData(colors, 0, (int)((mapSize.X * mapSize.Y)/4));*/
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
