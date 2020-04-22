using _0x46696E616C.ConcreteImplementations.Resources;
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
using WorldManager.Mobs.HarvestableUnits;
using WorldManager.TileHandlerLibrary;

namespace NationBuilder.WorldHandlerLibrary
{
    public class WorldGeneration
    {
        Random ran;
        string worldName;
        Game game;
        OpenSimplexNoise ElevationNoise;
        OpenSimplexNoise MoistureNoise;
        Vector2 MapSize;

        public WorldGeneration(Game game, string WorldName, long Seed, Vector2 MapSize)
        {
            this.game = game;
            this.worldName = WorldName;
            ElevationNoise = new OpenSimplexNoise(Seed);
            MoistureNoise = new OpenSimplexNoise(Seed - 10);
            this.MapSize = MapSize;
        }



        public double noise(float x, float y, OpenSimplexNoise noiseType)
        {
            return noiseType.eval(x, y);
        }
        /// <summary>
        /// Generates a tile based on the seed using OpenSimplex noise
        /// </summary>
        /// <param name="position"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public BackGroundTile Biome(Vector2 position, long seed)
        {
            float nx = position.X / MapSize.X;
            float ny = position.Y / MapSize.Y;
            double elevation = Math.Pow(Math.Sin(noise(nx, ny, ElevationNoise)), 3) + Math.Pow(Math.Sin(noise(nx, ny, ElevationNoise)), 2) + Math.Pow(nx, 2);
            double moisture = (6 * Math.Pow(noise(nx, ny, ElevationNoise), 5) - 15 * Math.Pow(noise(nx, ny, ElevationNoise), 4) + 10 * Math.Pow(noise(nx, ny, ElevationNoise), 3));
            if (elevation < 0.6)
            {
                return new BackGroundTile(TextureValue.Grass, position, Color.LimeGreen);
            }
            else if (elevation < 0.7)
            {
                return new BackGroundTile(TextureValue.Sand, position, Color.Yellow);
            }
            /*lse if (elevation < 0.9)
            {
                return new BackGroundTile(game, TextureValue.Water, position);
            }*/
            else
            {
                if (moisture < 0.6) return new BackGroundTile(TextureValue.Stone, position, Color.Gray);//Add Snow later
                else return new BackGroundTile(TextureValue.Stone, position, Color.Gray);
            }
        }
        /// <summary>
        /// Generates a decor tile based on the tile type using open simplex(I.E. trees are only spawnable in grass biomes, iron is only spawnable in stone tiles)
        /// </summary>
        /// <param name="Biome"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ModifiableTile AddDecor(BlockData Biome, float x, float y)
        {
            float treeFrequency = 0;
            float ironFrequency = 0;
            if (Biome.texture == TextureValue.Grass)
            {
                treeFrequency = 3f;
            }
            else if (Biome.texture == TextureValue.Stone)
            {
                ironFrequency = 1.2f;
            }
            bool placetree = (treeFrequency * noise(x, y, ElevationNoise)) > 0.75;
            bool placeIron = (ironFrequency * noise(x, y, ElevationNoise)) > 0.75;
            if (placetree)
            {
                return new Tree(TextureValue.Tree, new Wood(), this.GetType().Name, new Vector2(1), 100, 100, new Vector2(x, y), Color.DarkGreen);
            }
            if (placeIron)
            {
                return new IronVein(TextureValue.IronVein, new Iron(), this.GetType().Name, new Vector2(1), 100, 100, new Vector2(x, y), Color.OrangeRed);
            }
            return null;
        }
    }
}
