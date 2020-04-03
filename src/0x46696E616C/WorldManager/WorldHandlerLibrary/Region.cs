using Microsoft.Xna.Framework;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder.WorldHandlerLibrary
{
    [Serializable]
    public class Region
    {
        public List<Tile> tiles;
        public List<Tile> decorTiles;
        public Rectangle bounds;
        public Region(TileHandlerLibrary.Vector2 startPos, TileHandlerLibrary.Vector2 size)
        {
            bounds = new Rectangle(startPos.ToMonoGameVector2().ToPoint(), size.ToMonoGameVector2().ToPoint());
            tiles = new List<Tile>();
        }
    }
}
