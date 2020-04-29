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
    public class StartButton : Button
    {
        ActualGame game;
        #region Constructors
    
        public StartButton(GraphicsDevice gd, Vector2 position, Point size, Color color, string text, Game game) : base(gd, position, size, color, text)
        {
        }

        protected StartButton() : this(new Vector2(0),new Point(0), Color.Red, "Start", null)
        {

        }

        protected StartButton(Vector2 position, Point size, Color color, string Text, Game game) : base(position, size, color, Text)
        {
            
        }
        #endregion
        public override void Click(Game thisGame)
        {
            game = new ActualGame(thisGame);
            base.Click(thisGame);
        }
        public ActualGame LoadedGame()
        {
            return game;
        }
    }
}
