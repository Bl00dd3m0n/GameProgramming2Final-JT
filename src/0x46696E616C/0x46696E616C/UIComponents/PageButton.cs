using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UIProject;

namespace MainMenu.Component
{
    public class PageButton : Button
    {
        public string path { get; set; }
        Canvas canv;
        public PageButton(GraphicsDevice gd, Vector2 position, Point size, Color color, string text, Game game, string Path) : this(position, size, color, text, game)
        {
            this.path = Path;
        }

        protected PageButton() : this(new Vector2(0), new Point(0), Color.Green, "Exit", null)
        {

        }

        protected PageButton(Vector2 position, Point size, Color color, string Text, Game game) : base(position, size, color, Text)
        {
        }
        /// <summary>
        /// For this to work properly Click(Game, Canvas) needs to be called instead
        /// </summary>
        /// <param name="game"></param>
        public override void Click(Game game)
        {
            if (canv != null)
            {
                StyleSheet ss = new StyleSheet();
                canv.RemoveAllComponents();
                canv.LoadCanvas(ss.GetStyleSheet(game.GraphicsDevice, path, StyleSheet.ComponentTypes));
            }
            base.Click(game);
        }
        public void Click(Game game, Canvas canv)
        {
            this.canv = canv;
            Click(game);
        }
    }
}
