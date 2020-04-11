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
        private Vector2 position;
        bool placed;
        public Color tileColor { get; protected set; }
        public virtual Vector2 Position
        {
            get { return position; }
            protected set
            {
                position = value;
            }
        }
        public Tile(Game game, TextureValue texture, Vector2 position, Color color) : base(game)
        {
            block.texture = texture;
            this.Position = position;
            this.tileColor = color;
        }
        public Tile PlacedTile()
        {
            placed = true;
            return this;
        }
        public virtual void UpdatePosition(Vector2 position)
        {
            if (!placed)
            {
                this.position = position;//Can only modify position before you place it(allows for building placement)
            }
        }
    }
}
