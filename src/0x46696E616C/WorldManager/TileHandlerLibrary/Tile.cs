using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder.TileHandlerLibrary
{
    [Serializable]
    public class Vector2 : IEquatable<Vector2>
    {
        internal float x;
        internal float y;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Vector2);
        }

        public bool Equals(Vector2 other)
        {
            return other != null &&
                   x == other.x &&
                   y == other.y;
        }

        public Microsoft.Xna.Framework.Vector2 ToMonoGameVector2()
        {
            return new Microsoft.Xna.Framework.Vector2(this.x, this.y);
        }
    }
    [Serializable]
    public class Tile
    {
        public BlockData block;
        public Vector2 position;
        public Tile(TextureValue texture, Vector2 position)
        {
            block.texture = texture;
            this.position = position;
        }
    }
}
