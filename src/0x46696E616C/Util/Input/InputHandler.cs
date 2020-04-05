using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _0x46696E616C.Input
{
    public class InputHandler : GameComponent
    {
        public Vector2 inputPos { get; protected set; }
        protected SpriteBatch spriteBatch;
        public InputHandler(Game game, SpriteBatch sb) : base(game)
        {
            this.spriteBatch = sb;
        }

        protected void UpdateCursorPosition(Vector2 position)
        {
            this.inputPos = position;
        }



    }
    public class MouseKeyboard : InputHandler
    {
        bool Pressed;
        Dictionary<string, Keys> keyBinds;
        KeyboardState keyBoard;
        KeyboardState prevKeyState;
        MouseState mouse;
        MouseState prevMouseState;
        bool Select;
        List<Keys> PressedKeys;
        public float scrollVal { get { return mouse.ScrollWheelValue; } }
        public float prevScrollVal { get; private set;  }
        public MouseKeyboard(Game game, SpriteBatch sb) : base(game, sb)
        {

            keyBoard = Keyboard.GetState();
            prevKeyState = Keyboard.GetState();
            mouse = Mouse.GetState();
            prevMouseState = mouse;
            PressedKeys = new List<Keys>();
        }

        public override void Update(GameTime gameTime)
        {
            if (Game.IsActive)//Stops updating inputs if the player is outside the game
            {
                CheckMouseDown();
                UpdateKeyBoardState();
                UpdateMouseState();
                UpdateCursorPosition(mouse.Position.ToVector2());
            }
            Pressed = false;
            base.Update(gameTime);
        }

        public bool Scrolling()
        {
            if (scrollVal != prevScrollVal)
            {
                prevScrollVal = scrollVal;
                return true;
            }
            return false;
        }
        private void UpdateMouseState()
        {
            mouse = Mouse.GetState();
        }

        private void UpdateKeyBoardState()
        {
            keyBoard = Keyboard.GetState();
            PressedKeys.Clear();
            PressedKeys.AddRange(keyBoard.GetPressedKeys());
            if (keyBoard != prevKeyState)
            {
                prevKeyState = keyBoard;
                Pressed = true;
            }
        }

        public bool CheckKeyDown(Keys key)
        {
            if (PressedKeys.Contains(key))
            {
                PressedKeys.Clear();
                return true;
            }
            //Key commands will be put here
            return false;
        }

        public bool RightClick()
        {
            if (mouse.RightButton == ButtonState.Pressed && mouse != prevMouseState)
            {
                mouse = Mouse.GetState();
                return true;
            }
            //Key commands will be put here
            return false;
        }

        public bool LeftClick()
        {

            if (mouse.LeftButton == ButtonState.Pressed && mouse != prevMouseState)
            {
                return true;
            }
            //Key commands will be put here
            return false;
        }

        public void CheckMouseDown()
        {

            if (mouse != prevMouseState)
            {
                
            }
        }
    }
}