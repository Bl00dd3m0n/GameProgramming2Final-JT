using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder.WorldHandlerLibrary
{
    class WorldGeneration : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Random ran;
        List<Region> regions;
        long Seed;
        public WorldGeneration(Game game) : base(game)
        {
            regions = new List<Region>();
            ran = new Random();
            Seed = ran.Next() * ran.Next() * ran.Next();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            ContentHandler.LoadContent(Game);
            base.LoadContent();
            GenerateRegion();
        }

        public void SaveWorldData()
        {
            Save.WorldSave(regions);
            regions.Clear();
        }
        public void LoadWorldData()
        {
            regions = Save.WorldLoad();
        }

        private void GenerateRegion()
        {
            Random ran = new Random();
            for (int i = 0; i < 1; i++)
            {
                Region currentRegion = new Region();
                regions.Add(currentRegion);

                TextureGraphics(currentRegion);
            }
        }

        public double noise(float x, float y, OpenSimplexNoise noiseType)
        {
            return noiseType.eval(x, y);
        }

        public Tile Biome(float elevation, float moisture, TileHandlerLibrary.Vector2 position)
        {
            if (elevation < -0.15) return new Tile(TextureValue.Water,position);
            else if (elevation < 0.1) return new Tile(TextureValue.Sand, position);
            else if (elevation < 0.8)
            {
                return new Tile(TextureValue.Grass, position);
            }
            else
            {
                if (moisture < 0.6) return new Tile(TextureValue.Stone, position);
                else return new Tile(TextureValue.Stone, position);
            }
        }

        public void TextureGraphics(Region region)
        {
            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;
            //Texture2D perlintex = new Texture2D(GraphicsDevice, width, height);
            OpenSimplexNoise ElevationNoise = new OpenSimplexNoise(Seed);
            OpenSimplexNoise MoistureNoise = new OpenSimplexNoise(Seed-10);
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
                    tiles[x + y * width] = Biome(elevation, moisture, new TileHandlerLibrary.Vector2(x,y));
                }
            }
            region.tiles.AddRange(tiles);
            // TODO: use this.Content to load your game content here
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            int x = 0;
            int y = 0;
            foreach(Region region in regions)
            {
                x = 0;
                y = 0;
                for(int i = 0; i < region.tiles.Count;i++)
                {
                    spriteBatch.Draw(ContentHandler.DrawnTexture(region.tiles[i].block.texture), region.tiles[i].position.ToMonoGameVector2()*16, Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
