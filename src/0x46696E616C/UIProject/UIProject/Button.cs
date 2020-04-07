using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UIProject
{
    public class Button : Component
    {
        public bool Clicked { get; set; }
        public static int value;
        public virtual void Click()
        {
            value++;
            Clicked = true;
        }
        public Button()
        {

        }
        public Button(Vector2 position) : base(position, new Point(0,0))
        {
            this.Position = position;
        }
        public Button(GraphicsDevice gd, Vector2 position, Point Size) : this(gd, position, Size, Color.White) { }

        public Button(GraphicsDevice gd, Vector2 position, Point size, Color color) : this(position)
        {
            this.Size = size;
            this.color = color;
        }

        public Button(GraphicsDevice gd, Vector2 position, Point size, Color color, string Text) : this(gd, position, size, color)
        {
            this.Text = Text;
        }
    }
}
