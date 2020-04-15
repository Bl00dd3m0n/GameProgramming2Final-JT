using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler.Units;
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
            map = new Map(game, new Vector2(game.GraphicsDevice.Viewport.Width/2, game.GraphicsDevice.Viewport.Height/2), Seed);
            map.GenerateMap(game.GraphicsDevice);
            save = Save.save;
            this.game = game;
        }
        /// <summary>
        /// If the world has a tile at this position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Contains(Vector2 position)
        {
            if(GetBackgroundTile(position) != null)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// places a building of a position on the map
        /// </summary>
        /// <param name="building"></param>
        /// <param name="position"></param>
        /// <returns></returns>
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
        /// <summary>
        /// pulls the map size from map
        /// </summary>
        /// <returns></returns>
        public Vector2 GetSize()
        {
            return map.mapSize;
        }
        /// <summary>
        /// Gets the map as a texture
        /// </summary>
        /// <returns></returns>
        public Texture2D getMap()
        {
            return map.mapTexture;
        }
        /// <summary>
        /// checks if the building can be placed
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public bool CheckPlacement(Building building)
        {
            return CheckPlacement(building.Position, building.Size);
        }
        /// <summary>
        /// goes through each tile and checks if there is a tile if there is a tile it returns false
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets "Decoration" tiles such as buildings/resource tiles
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public ModifiableTile GetTile(Vector2 position)
        {
            return map.GetTile(position);
        }
        /// <summary>
        /// Gets units
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public IUnit GetUnit(Vector2 position)
        {
            return map.GetUnits(position);
        }
        /// <summary>
        /// Gets the world tiles.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Tile GetBackgroundTile(Vector2 position)
        {
            return map.GetBackTile(position);
        }
        /// <summary>
        /// Find the nearest tile with by tag
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Position"></param>
        /// <returns></returns>
        public IEntity FindNearest(string tag, Vector2 Position)
        {
            return map.GetTile(tag, Position);
        }
        /// <summary>
        /// Adds a mob to the list of mobs
        /// </summary>
        /// <param name="unit"></param>
        public void AddMob(IEntity unit)
        {
            map.AddMob(unit);
        }
    }
}
