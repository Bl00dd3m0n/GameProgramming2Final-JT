using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationBuilder.TileHandlerLibrary
{
    /*[Serializable]
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
        public static Vector2 Add(Vector2 vector1, Vector2 vector2)
        {
            return vector1 + vector2;
        }
        public static implicit operator Vector2(Microsoft.Xna.Framework.Vector2 v)
        {
            return new Vector2(v.X, v.Y);
        }
    }*/
    [Serializable]
    public class Tile : GameComponent
    {
        public BlockData block;
        public Vector2 position;
        public Tile(Game game, TextureValue texture, Vector2 position) : base(game)
        {
            block.texture = texture;
            this.position = position;
        }
    }
}
