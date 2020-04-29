using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Graphics;

namespace NationBuilder.TileHandlerLibrary
{
    public class Tile
    {
        public static float Zoom;
        public BlockData block;
        private Vector2 position;
        protected bool placed;
        public Color tileColor { get; protected set; }
        internal float fCost;
        
        internal void SetCost(float fCost)
        {
            this.fCost = fCost; 
        }

        public virtual Vector2 Position
        {
            get { return position; }
            protected set
            {
                position = value;
            }
        }
        public Tile(TextureValue texture, Vector2 position, Color color)
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
        public virtual void UpdatePosition(GraphicsDevice gd, Vector2 position)
        {
            if (!placed)
            {
                this.position = position;//Can only modify position before you place it(allows for building placement)
            }
        }
    }
}
