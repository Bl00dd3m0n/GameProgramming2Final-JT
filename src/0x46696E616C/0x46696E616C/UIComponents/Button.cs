﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using _0x46696E616C;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UIProject
{
    public class Button : Component
    {
        public bool Clicked { get; set; }
        public float clickedTimer;
        public virtual void Click(Game game)
        {
            Clicked = true;
            clickedTimer = 0.5f;
        }
        #region constructors
        public Button(GraphicsDevice gd) : this() { graphics = gd; }
        public Button(GraphicsDevice gd, Vector2 position, Point size) : this(position, size) { graphics = gd; }
        public Button(GraphicsDevice gd, Vector2 position, Point size, Color color) : this(position, size, color) { graphics = gd; }
        public Button(GraphicsDevice gd, Vector2 position, Point size, Color color, string text) : this(position, size, color, text) { graphics = gd; }

        protected Button() : this(new Vector2(0, 0), new Point(0, 0)) { }

        protected Button(Vector2 position, Point Size) : this(position, Size, Color.White) { }

        protected Button(Vector2 position, Point size, Color color) : base(position, size, color)
        {
        }

        protected Button(Vector2 position, Point size, Color color, string Text) : this(position, size, color)
        {
            this.Text = Text;
        }
        #endregion
    }
}
