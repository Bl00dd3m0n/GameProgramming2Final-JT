using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject;

namespace _0x46696E616C.UIComponents
{
    public class Label : Component
    {
        protected Label() : this(new Vector2(0), "", Color.Black)
        {

        }
        public Label(Vector2 Position, string text, Color textColor)
        {
            this.Text = text;
            this.Position = Position;
            this.TextColor = textColor;
        } 
    }
}
