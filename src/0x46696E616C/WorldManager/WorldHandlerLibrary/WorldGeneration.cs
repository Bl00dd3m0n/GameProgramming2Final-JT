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
using WorldManager.TileHandlerLibrary;
//using MyVector2 = NationBuilder.TileHandlerLibrary.Vector2;

namespace NationBuilder.WorldHandlerLibrary
{
    public class WorldGeneration
    {
        Random ran;
        List<Region> regions;
        long Seed;
        string worldName;
        Game game;
        OpenSimplexNoise ElevationNoise;
        OpenSimplexNoise MoistureNoise;
        public WorldGeneration(Game game, string WorldName, long seed)
        {
            this.game = game;
            regions = new List<Region>();
            ran = new Random();
            //Seed = ran.Next() * ran.Next() * ran.Next();
            this.worldName = WorldName;
            ElevationNoise = new OpenSimplexNoise(Seed);
            MoistureNoise = new OpenSimplexNoise(Seed - 10);
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

        internal Region GenerateRegion(Vector2 position, Vector2 RegionSize)
        {
            return GenerateRegion(position, RegionSize, Seed);
        }

        internal Region GenerateRegion(Vector2 position, Vector2 RegionSize, long seed)
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

        public BackGroundTile Biome(Vector2 position, long seed)
        {
            double elevation = noise(position.X, position.Y, ElevationNoise);
            double moisture = noise(position.X, position.Y, MoistureNoise);
            if (elevation < -2220.65)
            {
                return new BackGroundTile(game, TextureValue.Water, position);
            }
            else if (elevation < -0.21)
            {
                return new BackGroundTile(game, TextureValue.Sand, position);
            }
            else if (elevation < 0.8)
            {
                return new BackGroundTile(game, TextureValue.Grass, position);
            }
            else
            {
                if (moisture < 0.6) return new BackGroundTile(game, TextureValue.Stone, position);//Add Snow later
                else return new BackGroundTile(game, TextureValue.Stone, position);
            }
        }

        public BackGroundTile Biome(float elevation, float moisture, Vector2 position)
        {
            if (elevation < -2220.65)
            {
                return new BackGroundTile(game, TextureValue.Water, position);
            }
            else if (elevation < -0.21)
            {
                return new BackGroundTile(game, TextureValue.Sand, position);
            }
            else if (elevation < 0.8)
            {
                return new BackGroundTile(game, TextureValue.Grass, position);
            }
            else
            {
                if (moisture < 0.6) return new BackGroundTile(game, TextureValue.Stone, position);//Add Snow later
                else return new BackGroundTile(game, TextureValue.Stone, position);
            }
        }

        public Tile AddDecor(BlockData Biome, float x, float y, OpenSimplexNoise decorNoise)
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
                return new Tile(game, TextureValue.Tree, new Vector2(x, y));
            }
            return null;
        }

        public void TextureGraphics(Region region)
        {
            int width = (int)(region.bounds.Width);
            int height = (int)(region.bounds.Height);

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
                    region.backTiles.Add(Biome(elevation, moisture, new Vector2(x, y)));
                    Tile decorTile = AddDecor(region.backTiles[x + y * width].block, x, y, ElevationNoise);
                    if(decorTile != null)
                        region.decorTiles.Add(decorTile);
                }
            }
            // TODO: use this.Content to load your game content here
        }
    }
}
