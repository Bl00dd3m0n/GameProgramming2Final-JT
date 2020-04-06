using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder.WorldHandlerLibrary
{
    public class Region
    {
        public List<Tile> tiles;

        public Region()
        {
            tiles = new List<Tile>();
        }
    }
}
