using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager.TileHandlerLibrary;

namespace NationBuilder.WorldHandlerLibrary
{
    [Serializable]
    public class Region
    {
        public List<BackGroundTile> backTiles;
        public List<Tile> decorTiles;
        public Rectangle bounds;
        public Region(Vector2 startPos, Vector2 size)
        {
            bounds = new Rectangle(startPos.ToPoint(), size.ToPoint());
            backTiles = new List<BackGroundTile>();
            decorTiles = new List<Tile>();
        }
        public Tile[] GetTilesAtPosition(Vector2 position)
        {
            Tile[] decorAndBack = new Tile[2];
            bool decorCheck = false;
            try
            {
                //decorAndBack[0] = decorTiles.First(l => l.position == position);
                decorCheck = true;
                decorAndBack[1] = backTiles.First(l => l.Position == position);
            } catch(InvalidOperationException e)
            {
                if (decorCheck)
                {
                    try
                    {
                        decorAndBack[0] = null;
                        decorAndBack[1] = backTiles.First(l => l.Position == position);
                    }
                    catch (InvalidOperationException ex)
                    {
                        decorAndBack[1] = null;
                    }
                }
            }
            return decorAndBack;
        }
    }
}
