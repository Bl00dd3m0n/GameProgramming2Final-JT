using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Buildings;
using NationBuilder.TileHandlerLibrary;
using WorldManager.MapData;

namespace WorldManager.TileHandlerLibrary
{
    /// <summary>
    /// Tile to be a placeholder if the tile is bigger than a 16x16 tile
    /// </summary>
    public class ReferenceTile : ModifiableTile, IMapObserver 
    {
        public ModifiableTile tile { get; private set; }

        public ReferenceTile(ModifiableTile tile) : base(TextureValue.None, tile.Position, tile.TeamStats, tile.tileColor)
        {
            this.tile = tile;
        }

        public void Update(ModifiableTile observer)
        {
            if(observer.State == tileState.dead)
            {
                tile = null;
                Die();
            }
        }
    }
}
