using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _0x46696E616C.InputHandler
{
    public class InputHandler : GameComponent
    {
        public InputHandler(Game game) : base(game)
        {

        }

        public Vector2 inputPos { get; private set; }
        protected void Move(Vector2 position)
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
        public MouseKeyboard(Game game) : base(game)
        {

            keyBoard = Keyboard.GetState();
            prevKeyState = Keyboard.GetState();
            mouse = Mouse.GetState();
            prevMouseState = mouse;
            PressedKeys = new List<Keys>();
        }

        public override void Update(GameTime gameTime)
        {
            CheckMouseDown();
            UpdateKeyBoardState();

            base.Update(gameTime);
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


        public void CheckMouseDown()
        {

            if (mouse != prevMouseState)
            {
                if (mouse.RightButton == ButtonState.Pressed)
                {
                    Move(mouse.Position.ToVector2());
                }
                else if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Select = true;
                }
                else
                {
                    Select = false;
                }
            }
            else if (Select)
            {

            }
        }
    }
}