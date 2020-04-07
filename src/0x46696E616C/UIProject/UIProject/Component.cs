using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UIProject
{
    [XmlRoot("Component")]
    public abstract class Component : IComponent
    {
        public Vector2 Position { get; set; }
        public Point Size { get; set; }
        public Color color { get; set; }
        public string Text { get; set; }
        public Vector2 Textposition;

        [XmlIgnore]
        public Texture2D picture { get; private set; }
        [XmlIgnore]
        public GraphicsDevice graphics { get; set; }

        #region Constructors
        /// <summary>
        /// Need to define graphics outside of the constructor to allow style sheets to work properly
        /// </summary>
        protected Component() : this(new Vector2(0, 0), new Point(0, 0)) { }

        protected Component(Vector2 position, Point Size) : this(position, Size, Color.White) { }

        protected Component(Vector2 position, Point size, Color color)
        {
            this.Size = size;
            this.color = color;
            this.Position = position;
        }

        protected Component(Vector2 position, Point size, Color color, string Text) : this(position, size, color)
        {
            this.Text = Text;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public virtual void Draw(GraphicsDevice gd)
        {
            if (graphics == null) graphics = gd;
            Color[] background = new Color[Size.X * Size.Y];
            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    background[x + (y * Size.X)] = color;
                }
            }
            picture = new Texture2D(this.graphics, Size.X,Size.Y);

            picture.SetData(background,0,Size.X * Size.Y);
        }

        public virtual void Resize(Point size)
        {
            this.Size = size;
        }
    }
}
