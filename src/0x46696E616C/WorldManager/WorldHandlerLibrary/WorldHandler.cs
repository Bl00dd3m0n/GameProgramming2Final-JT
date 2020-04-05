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
            map.GenerateMap();
            save = Save.save;
            this.game = game;
        }

        public bool Place(Building building, Vector2 position)
        {
            if(CheckPlacement(position, building.Size))
            {
                map.PlaceBlock(building, position);
                Tile tile = map.GetTile(position-new Vector2(0,1));
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

        public bool CheckPlacement(Vector2 position, Vector2 size)
        {
            
            int StartX = 0;
            int StartY = 0;
            if(size.X%2 == 0) StartX = -((int)(size.X/2)-1);
            else StartX = -(int)(size.X / 2);

            if (size.Y%2 == 0) StartY = -((int)(size.Y/2)-1);
            else StartY = -(int)(size.Y / 2);
            if (position.X + StartX > 0 && position.Y + StartY > 0)
            {
                for (int y = StartY; y < (int)(size.Y / 2); y++)
                {
                    for (int x = StartX; x < (int)(size.X / 2); x++)
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
