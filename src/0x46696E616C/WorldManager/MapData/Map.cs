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
                        ((ModifiableTile)tiles[x, y, 1]).Subscribe(this);
                    }
                }
            }
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
                            tiles[(int)position.X + x, (int)position.Y + y, 1] = building;
                        }
                        else
                        {
                            tiles[(int)position.X + x, (int)position.Y + y, 1] = new ReferenceTile((ModifiableTile)tiles[(int)position.X, (int)position.Y, 1]);
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

        internal IUnit GetUnits(Vector2 position)
        {
            return (IUnit)mobs.Find(l => l.Position.ToPoint() == position.ToPoint());
        }

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

        internal BackGroundTile GenerateTerrain(Vector2 position)
        {
            return wg.Biome(position, Seed);
        }

        internal ModifiableTile GenerateDecor(Vector2 position)
        {
            return wg.AddDecor(tiles[(int)position.X, (int)position.Y, 0].block, position.X, position.Y);
        }

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
        internal void AddMob(IEntity unit)
        {
            mobs.Add(unit);
            ((ModifiableTile)unit).Subscribe(this);
        }
    }
}
