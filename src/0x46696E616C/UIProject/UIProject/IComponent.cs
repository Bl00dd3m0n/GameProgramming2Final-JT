using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject
{
    public interface IComponent
    {
        Vector2 Position { get; }
        Point Size { get; }
        Color color { get; }
        string Text { get; }
        void Draw(ref Texture2D tex, GraphicsDevice device);
    }
}
