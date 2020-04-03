using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;
using MyVector2 = NationBuilder.TileHandlerLibrary.Vector2;

namespace NationBuilder.WorldHandlerLibrary
{
    public class WorldGeneration
    {
        Random ran;
        List<Region> regions;
        long Seed;
        string worldName;
        Game game;
        public WorldGeneration(Game game, string WorldName)
        {
            this.game = game;
            regions = new List<Region>();
            ran = new Random();
            //Seed = ran.Next() * ran.Next() * ran.Next();
            this.worldName = WorldName;
        }


        public void SaveWorldData()
        {
            WindowsSave save = (WindowsSave)WindowsSave.save;
            save.SaveWorld(regions, worldName);
            //regions.Clear();
        }
        public void LoadWorldData()
        {
            //regions = Save.save.LoadWorld(worldName);
        }

        internal Region GenerateRegion(MyVector2 position, MyVector2 RegionSize)
        {
            return GenerateRegion(position, RegionSize, Seed);
        }

        internal Region GenerateRegion(MyVector2 position, MyVector2 RegionSize, long seed)
        {
            Random ran = new Random();
            Region currentRegion = new Region(position, RegionSize);
            TextureGraphics(currentRegion);
            SaveWorldData();
            seed = Seed;
            return currentRegion;
        }


        public double noise(float x, float y, OpenSimplexNoise noiseType)
        {
            return noiseType.eval(x, y);
        }

        public Tile Biome(float elevation, float moisture, MyVector2 position)
        {
            if (elevation < -2220.65)
            {
                return new Tile(TextureValue.Water, position);
            }
            else if (elevation < -0.21)
            {
                return new Tile(TextureValue.Sand, position);
            }
            else if (elevation < 0.8)
            {
                return new Tile(TextureValue.Grass, position);
            }
            else
            {
                if (moisture < 0.6) return new Tile(TextureValue.Stone, position);//Add Snow later
                else return new Tile(TextureValue.Stone, position);
            }
        }

        private Tile AddDecor(BlockData Biome, float x, float y, OpenSimplexNoise decorNoise)
        {
            float treeFrequency = 0;
            if (Biome.texture == TextureValue.Grass)
            {
                treeFrequency = 2;
            }
            else if (Biome.texture == TextureValue.Grass)
            {
                treeFrequency = 0.5f;
            }
            bool placetree = (treeFrequency * noise(1 * x, 1 * y, decorNoise)) > 0.75;
            if (placetree)
            {
                return new Tile(TextureValue.Tree, new MyVector2(x, y));
            }
            return null;
        }

        public void TextureGraphics(Region region)
        {
            float dividing_value = 2;
            int width = (int)(region.bounds.Width);
            int height = (int)(region.bounds.Height);
            //Texture2D perlintex = new Texture2D(GraphicsDevice, width, height);
            OpenSimplexNoise ElevationNoise = new OpenSimplexNoise(Seed);
            OpenSimplexNoise MoistureNoise = new OpenSimplexNoise(Seed - 10);
            Tile[] tiles = new Tile[(width) * (height)];
            float frequency = 10f;
            float nx = 0;
            float ny = 0;
            for (int y = 0; y < height; y += 1)
            {
                for (int x = 0; x < width; x += 1)
                {
                    nx = frequency * x / (width);
                    ny = frequency * y / (height);
                    float elevation = (float)(1 * noise(1 * nx, 1 * ny, ElevationNoise) + 0.5 * noise(2 * nx, 2 * ny, ElevationNoise) + 0.25 * noise(4 * nx, 2 * ny, ElevationNoise));
                    float moisture = (float)(1 * noise(1 * nx, 1 * ny, MoistureNoise) + 0.5 * noise(2 * nx, 2 * ny, MoistureNoise) + 0.25 * noise(4 * nx, 2 * ny, MoistureNoise));
                    tiles[x + y * width] = Biome(elevation, moisture, new MyVector2(x, y));
                    Tile decorTile = AddDecor(tiles[x + y * width].block, x, y, ElevationNoise);
                    tiles[x + y * width] = decorTile == null ? tiles[x + y * width] : decorTile;
                }
            }
            region.tiles.AddRange(tiles);
            // TODO: use this.Content to load your game content here
        }
    }
}
