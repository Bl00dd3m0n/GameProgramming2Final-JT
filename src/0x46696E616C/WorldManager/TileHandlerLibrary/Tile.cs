using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder.TileHandlerLibrary
{
    [Serializable]
    public class Tile : GameComponent
    {
        public static float Zoom;
        public BlockData block;
        public Vector2 position;
        public Tile(Game game, TextureValue texture, Vector2 position) : base(game)
        {
            block.texture = texture;
            this.position = position;
        }
    }
}
