using _0x46696E616C.Buildings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.TileHandlerLibrary;
using NationBuilder.WorldHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WorldManager.MapData;
using WorldManager.TileHandlerLibrary;

namespace WorldManager
{
    public class WorldHandler
    {
        Map map;
        Game game;
        Save save;
        long Seed;
        public WorldHandler(Game game, string WorldName)
        {
            Seed = 14153456352343;
            map = new Map(game, new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), 14153456352343);
            map.GenerateMap(game.GraphicsDevice);
            save = Save.save;
            this.game = game;
        }

        public bool Place(Building building, Vector2 position)
        {
            if(CheckPlacement(position, building.Size))
            {
                map.PlaceBlock(building, position);
                return true;
            } else
            {
                return false;
            }
        }

        public Vector2 GetSize()
        {
            return map.mapSize;
        }

        public Texture2D getMap()
        {
            return map.mapTexture;
        }

        public bool CheckPlacement(Vector2 position, Vector2 size)
        {
            
            int StartX = 0;
            int StartY = 0;
            if (position.X + StartX >= 0 && position.Y + StartY >= 0)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    for (int x = 0; x < size.X; x++)
                    {
                        if (GetTile(new Vector2(position.X + x, position.Y + y)) != null)
                            return false;
                    }
                }
                return true;
            } else
            {
                return false;
            }

            
        }

        public void LoadArea()
        {

        }

        public ModifiableTile GetTile(Vector2 position)
        {
            return map.GetTile(position);
        }
        public Tile GetBackgroundTile(Vector2 position)
        {
            return map.GetBackTile(position);
        }
    }
}
