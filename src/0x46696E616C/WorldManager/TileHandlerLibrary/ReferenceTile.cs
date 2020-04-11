using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationBuilder.TileHandlerLibrary;

namespace WorldManager.TileHandlerLibrary
{
    /// <summary>
    /// Tile to be a placeholder if the tile is bigger than a 16x16 tile
    /// </summary>
    class ReferenceTile : ModifiableTile
    {
        private ModifiableTile tile;

        public ReferenceTile(ModifiableTile tile) : base(tile.Game,TextureValue.None, tile.Position, tile.tileColor)
        {
            this.tile = tile;
        }
    }
}
