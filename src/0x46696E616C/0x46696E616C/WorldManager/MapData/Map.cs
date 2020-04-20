using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
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
    public class Map : IMapObserver
    {
        public Vector2 mapSize { get; private set; }
        Tile[,,] tiles;
        public Texture2D mapTexture { get; private set; }
        long Seed;
        WorldGeneration wg;
        List<IEntity> mobs;

        public Map(Game game, Vector2 mapSize, long Seed)
        {
            tiles = new Tile[(int)mapSize.X, (int)mapSize.Y, 2];
            this.mapSize = mapSize;
            this.Seed = Seed;
            wg = new WorldGeneration(game, this.GetType().Name.ToString(), Seed, mapSize);
            mobs = new List<IEntity>();
        }
        /// <summary>
        /// using the opensimplex noise function generates a map using a seed to generate the background
        /// </summary>
        /// <param name="gd"></param>
        public void GenerateMap(GraphicsDevice gd)
        {
            Color[] colors = new Color[(int)((mapSize.X * mapSize.Y))];
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    tiles[x, y, 0] = GenerateTerrain(new Vector2(x, y)).PlacedTile();
                    tiles[x, y, 1] = GenerateDecor(new Vector2(x, y));
                    if (tiles[x, y, 1] != null)
                    {
                        tiles[x, y, 1].PlacedTile();
                        ((ModifiableTile)tiles[x, y, 1]).Subscribe(this); //The map subscribes to every tile and if they update the map is notified
                    }
                }
            }
            //Generates a map texture based on the background
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    if (tiles[x, y, 1] != null) colors[(int)((x) + (y) * (mapSize.X))] = tiles[x, y, 1].tileColor;
                    else colors[(int)((x) + (y) * (mapSize.X))] = tiles[x, y, 0].tileColor;
                }
            }
            mapTexture = new Texture2D(gd, (int)mapSize.X, (int)mapSize.Y);
            mapTexture.SetData(colors, 0, (int)((mapSize.X * mapSize.Y)));

        }

        /// <summary>
        /// places a building at a certain point
        /// </summary>
        /// <param name="building"></param>
        /// <param name="position"></param>
        internal void PlaceBlock(ModifiableTile building, Vector2 position)
        {
            for (int y = 0; y <= building.Size.Y; y++)
            {
                for (int x = 0; x <= building.Size.X; x++)
                {
                    try
                    {
                        if (y == 0 && x == 0)
                        {
                            building.Subscribe(this);
                            tiles[(int)position.X + x, (int)position.Y + y, 1] = building;
                            
                        }
                        else
                        {
                            tiles[(int)position.X + x, (int)position.Y + y, 1] = new ReferenceTile((ModifiableTile)tiles[(int)position.X, (int)position.Y, 1]);
                            ((ModifiableTile)tiles[(int)position.X + x, (int)position.Y + y, 1]).Subscribe(this);
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        //If the player tries to place a building off the map
                        string error = $"{ex}";
                    }
                }
            }
        }
        /// <summary>
        /// Checks the mobs list to see if it contains something at that position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        internal IUnit GetUnits(Vector2 position)
        {
            return (IUnit)mobs.Find(l => l.Position.ToPoint() == position.ToPoint());
        }

        /// <summary>
        /// Gets a tile based on tag a position - Used in finding buildings for resource collection
        /// </summary>
        /// <param name="v"></param>
        /// <param name="Position"></param>
        /// <returns>the building with the shortest distance from the unit with the specified tag</returns>
        internal IEntity GetTile(string v, Vector2 Position)
        {
            IEntity tile = null;
            float distance = 0;
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    if (tiles[x, y, 1] != null)
                    {
                        if (((ModifiableTile)tiles[x, y, 1]).HasTag(v))
                        {
                            if (Vector2.Distance(tiles[x, y, 1].Position, Position) < distance || distance == 0)
                            {
                                tile = (IEntity)tiles[x, y, 1];
                                distance = Vector2.Distance(tiles[x, y, 1].Position, Position);
                            }
                        }
                    }
                }
            }
            return tile;
        }
        internal IEntity[] GetTile(string v)
        {
            List<IEntity> taggedTiles = new List<IEntity>();
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    if (tiles[x, y, 1] != null)
                    {
                        if (((ModifiableTile)tiles[x, y, 1]).HasTag(v))
                        {
                            taggedTiles.Add((IEntity)tiles[x, y, 1]);
                        }
                    }
                }
            }
            return taggedTiles.ToArray();
        }

        internal IEntity GetTile(int v, Vector2 Position)
        {
            IEntity tile = null;
            float distance = 0;
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    ModifiableTile modTile = null;
                    if (tiles[x, y, 1] != null) modTile = (ModifiableTile)tiles[x, y, 1];
                    else if (GetUnits(new Vector2(x, y)) != null)
                        modTile = (ModifiableTile)GetUnits(new Vector2(x, y));
                    if (modTile != null)
                    {
                        if (modTile.TeamAssociation.Equals(v))
                        {
                            if (Vector2.Distance(modTile.Position, Position) < distance || distance == 0)
                            {
                                tile = modTile;
                                distance = Vector2.Distance(modTile.Position, Position);
                            }
                        }
                    }
                }
            }
            return tile;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns>the tile at the position</returns>
        internal ModifiableTile GetTile(Vector2 position)
        {
            try
            {
                return (ModifiableTile)tiles[(int)position.X, (int)position.Y, 1];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Background tiles at the positon</returns>
        internal Tile GetBackTile(Vector2 position)
        {
            try
            {
                return tiles[(int)position.X, (int)position.Y, 0];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
        /// <summary>
        /// Accesses world generation to generate a tile at the position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        internal BackGroundTile GenerateTerrain(Vector2 position)
        {
            return wg.Biome(position, Seed);
        }
        /// <summary>
        /// Generates "Decor" tiles through the world generation class
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        internal ModifiableTile GenerateDecor(Vector2 position)
        {
            return wg.AddDecor(tiles[(int)position.X, (int)position.Y, 0].block, position.X, position.Y);
        }
        /// <summary>
        /// if a modifiable tile dies it updates the map and removes it from either the list of mobs or the map array
        /// </summary>
        /// <param name="observer"></param>
        public void Update(ModifiableTile observer)
        {
            if (observer.State == tileState.dead)
            {

                if (observer is IUnit)
                {
                    int index = mobs.FindIndex(l => l.Position.ToPoint() == observer.Position.ToPoint());
                    if (index >= 0)
                    {
                        mobs.RemoveAt(index);
                    }
                }
                else tiles[(int)observer.Position.X, (int)observer.Position.Y, 1] = null;
            }
        }
        /// <summary>
        /// Adds a unit to the position
        /// </summary>
        /// <param name="unit"></param>
        internal void AddMob(IEntity unit)
        {
            mobs.Add(unit);
            ((ModifiableTile)unit).Subscribe(this);
        }
    }
}
