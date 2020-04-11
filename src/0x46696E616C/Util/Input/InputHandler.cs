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
        public bool Updated { get; private set; }

        bool leftButtonPressed, rightButtonPressed;
        public MouseKeyboard(Game game, SpriteBatch sb) : base(game, sb)
        {

            keyBoard = Keyboard.GetState();
            prevKeyState = Keyboard.GetState();
            mouse = Mouse.GetState();
            prevMouseState = mouse;
            PressedKeys = new List<Keys>();
            leftButtonPressed = rightButtonPressed = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (Game.IsActive)//Stops updating inputs if the player is outside the game
            {
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
            leftButtonPressed = rightButtonPressed = false;
            if (mouse != prevMouseState)
            {
                prevMouseState = mouse;
                Updated = true;
                if (mouse.LeftButton == ButtonState.Pressed)
                    leftButtonPressed = true;
                if (mouse.RightButton == ButtonState.Pressed)
                    rightButtonPressed = true;
            }

        }

        private void UpdateKeyBoardState()
        {
            keyBoard = Keyboard.GetState();
            PressedKeys.Clear();
            PressedKeys.AddRange(keyBoard.GetPressedKeys());
            if (keyBoard != prevKeyState)
            {
                prevKeyState = keyBoard;
                Updated = true;
                Pressed = true;
            }
        }
        /// <summary>
        /// If the key is down at all
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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
        /// <summary>
        /// If the key has been pressed once have to release and press again to activate
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetKeyDown(Keys key)
        {
            if(Updated && PressedKeys.Contains(key))
            {
                return true;
            }
            return false;
        }

        public bool RightClick()
        {
            return rightButtonPressed;
        }

        public bool LeftClick()
        {
            return leftButtonPressed;
        }
    }
}