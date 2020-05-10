using _0x46696E616C.Util.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SaveManager;
using System;
using System.Collections.Generic;
using UIProject;

namespace MainMenu.Component
{
    public class InputButton : Button
    {
        public Controls control { get; set; }
        InputDefinitions input;
        
        public InputButton(GraphicsDevice gd, Vector2 position, Point size, Color color, string text, Game game, Controls control, InputDefinitions input) : this(position, size, color, text, game)
        {
            this.control = control;
        }

        protected InputButton() : this(new Vector2(0), new Point(0), Color.Green, "Input", null)
        {
            
        }

        protected InputButton(Vector2 position, Point size, Color color, string Text, Game game) : base(position, size, color, Text)
        {
        }

        public override void Click(Game game)
        {
            if (input != null)
            {
                input.ListenForInput(this);
            }
            base.Click(game);
        }

        public void Click(InputDefinitions input, Game game)
        {
            this.input = input;
            this.Click(game);
        }

        internal void UpdateControl(MouseKeyboardBindings controls)
        {
            input.SetBinds(control, controls);
            Text = input.GetControls(control);
            input.SaveSettings();
            Clicked = false;
        }
    }
}