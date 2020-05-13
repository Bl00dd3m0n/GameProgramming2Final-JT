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
using Newtonsoft.Json;
using _0x46696E616C.CommandPattern.Commands;

namespace WorldManager.MapData
{
    public class Map : IMapObserver
    {
        public Vector2 mapSize { get; private set; }
        [JsonIgnore]
        protected Tile[,,] map;
        [JsonIgnore]
        public Texture2D mapTexture { get; private set; }
        long Seed { get; set; }
        WorldGeneration wg { get; set; }
        public List<IEntity> mobs { get; private set; }

        internal void Clear()
        {
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    if (map[x, y, 1] is Building)
                    {
                        ((Building)map[x, y, 1]).Die();
                    }
                    map[x, y, 0] = null;
                    map[x, y, 1] = null;
                }
            }
            mapSize = Vector2.Zero;
            map = null;
            mapTexture = null;
            Seed = 0;
            wg = null;
            mobs.Clear();
        }
        protected Map() { }
        public Map(Game game, Vector2 mapSize, long Seed)
        {
            map = new Tile[(int)mapSize.X, (int)mapSize.Y, 2];
            this.mapSize = mapSize;
            this.Seed = Seed;
            wg = new WorldGeneration(this.GetType().Name.ToString(), Seed, mapSize);
            mobs = new List<IEntity>();
        }
        /// <summary>
        /// using the opensimplex noise function generates a map using a seed to generate the background
        /// </summary>
        /// <param name="gd"></param>
        public virtual void GenerateMap(GraphicsDevice gd)
        {
            if (mobs != null)
                mobs.Clear();
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    map[x, y, 0] = GenerateTerrain(new Vector2(x, y)).PlacedTile(gd);
                    map[x, y, 1] = GenerateDecor(new Vector2(x, y));
                    if (map[x, y, 1] != null)
                    {
                        map[x, y, 1].PlacedTile(gd);
                        ((ModifiableTile)map[x, y, 1]).Subscribe(this); //The map subscribes to every tile and if they update the map is notified
                    }
                }
            }

        }
        protected void DrawMapUI(GraphicsDevice gd)
        {
            Color[] colors = new Color[(int)((mapSize.X * mapSize.Y))];
            //Generates a map texture based on the background
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    if (map[x, y, 0] != null)
                    {
                        if (map[x, y, 1] != null) colors[(int)((x) + (y) * (mapSize.X))] = map[x, y, 1].tileColor;
                        else colors[(int)((x) + (y) * (mapSize.X))] = map[x, y, 0].tileColor;
                    }
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
            for (int y = 0; y < building.Size.Y; y++)
            {
                for (int x = 0; x < building.Size.X; x++)
                {
                    try
                    {
                        if (y == 0 && x == 0)
                        {
                            building.Subscribe(this);
                            map[(int)position.X + x, (int)position.Y + y, 1] = building;

                        }
                        else
                        {
                            map[(int)position.X + x, (int)position.Y + y, 1] = new ReferenceTile((ModifiableTile)map[(int)position.X, (int)position.Y, 1]);
                            ((ModifiableTile)map[(int)position.X + x, (int)position.Y + y, 1]).Subscribe(this);
                            ((ModifiableTile)map[(int)position.X, (int)position.Y, 1]).Subscribe((ReferenceTile)map[(int)position.X + x, (int)position.Y + y, 1]);
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
                    if (map[x, y, 1] != null)
                    {
                        if (((ModifiableTile)map[x, y, 1]).HasTag(v))
                        {
                            if (Vector2.Distance(map[x, y, 1].Position, Position) < distance || distance == 0)
                            {
                                tile = (IEntity)map[x, y, 1];
                                distance = Vector2.Distance(map[x, y, 1].Position, Position);
                            }
                        }
                    }
                }
            }
            return tile;
        }

        internal List<IUnit> GetUnits(int v)
        {
            return mobs.Where(l => ((BasicUnit)l).TeamAssociation == v).Cast<IUnit>().ToList();
        }

        internal IEntity[] GetTile(int team)
        {
            List<IEntity> taggedTiles = new List<IEntity>();
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    if (map[x, y, 1] != null)
                    {
                        if (((ModifiableTile)map[x, y, 1]).TeamAssociation.Equals(team))
                        {
                            taggedTiles.Add((IEntity)map[x, y, 1]);
                        }
                    }
                }
            }
            return taggedTiles.ToArray();
        }

        internal IEntity[] GetTile(string v)
        {
            List<IEntity> taggedTiles = new List<IEntity>();
            for (int y = 0; y < mapSize.Y; y++)
            {
                for (int x = 0; x < mapSize.X; x++)
                {
                    if (map[x, y, 1] != null)
                    {
                        if (((ModifiableTile)map[x, y, 1]).HasTag(v))
                        {
                            taggedTiles.Add((IEntity)map[x, y, 1]);
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
                    if (map[x, y, 1] != null) modTile = (ModifiableTile)map[x, y, 1];
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
                return (ModifiableTile)map[(int)position.X, (int)position.Y, 1];
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
                return map[(int)position.X, (int)position.Y, 0];
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
            return wg.AddDecor(map[(int)position.X, (int)position.Y, 0].block, position.X, position.Y);
        }
        /// <summary>
        /// if a modifiable tile dies it updates the map and removes it from either the list of mobs or the map array
        /// </summary>
        /// <param name="observer"></param>
        public void Update(ModifiableTile observer)
        {
            if (observer.Position.X >= 0 && observer.Position.X < mapSize.X && observer.Position.Y >= 0 && observer.Position.Y < mapSize.Y)
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
                    else map[(int)observer.Position.X, (int)observer.Position.Y, 1] = null;
                }
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
