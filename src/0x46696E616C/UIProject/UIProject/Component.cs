using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject
{
    [Serializable]
    public abstract class Component : IComponent
    {
        public Vector2 Position { get; protected set; }

        public Point Size { get; protected set; }

        public Color color { get; set; }

        public string Text { get; protected set; }

        public Component() : this(new Vector2(0,0), new Point(0,0))
        {
        }
        public Component(Vector2 position, Point Size) : this(position, Size, Color.White) { }

        public Component(Vector2 position, Point size, Color color)
        {
            this.Size = size;
            this.color = color;
        }

        public Component(Vector2 position, Point size, Color color, string Text) : this(position, size, color)
        {
            this.Text = Text;
        }

        public virtual void Draw(ref Texture2D component, GraphicsDevice device)
        {
            Color[] background = new Color[Size.X * Size.Y];
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    background[x + (y * Size.X)] = color;
                }
            }
            if(component == null || component.Width > Size.X || component.Height > Size.Y) component = new Texture2D(device, Size.X,Size.Y);
            
            component.SetData(background,0,Size.X * Size.Y);
        }

        public virtual void Resize(Point size)
        {
            this.Size = size;
        }
    }
}
