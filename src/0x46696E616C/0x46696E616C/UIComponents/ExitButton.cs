using _0x46696E616C;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject;

namespace MainMenu.Component
{
    public class ExitButton : Button
    {
        public ExitButton(GraphicsDevice gd, Vector2 position, Point size, Color color, string text, Game game) : this(position, size, color, text, game)
        {
        }

        protected ExitButton() : this(new Vector2(0), new Point(0), Color.Green, "Exit", null)
        {

        }

        protected ExitButton(Vector2 position, Point size, Color color, string Text, Game game) : base(position, size, color, Text)
        {

        }
        public override void Click(Game game)
        {
            game.Exit();
            base.Click(game);
        }
    }
}
