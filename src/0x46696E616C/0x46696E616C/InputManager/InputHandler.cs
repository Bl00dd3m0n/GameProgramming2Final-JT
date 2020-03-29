using _0x46696E616C.InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _0x46696E616C.InputManager
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
        Dictionary<string, Keys> keyBinds;
        KeyboardState keyBoard;
        KeyboardState prevKeyState;
        MouseState mouse;
        MouseState prevMouseState;
        bool Select;
        public MouseKeyboard(Game game) : base(game)
        {

            keyBoard = Keyboard.GetState();
            prevKeyState = keyBoard;
            mouse = Mouse.GetState();
            prevMouseState = mouse;
        }

        public override void Update(GameTime gameTime)
        {
            CheckMouseDown();
            CheckKeyDown();
            base.Update(gameTime);
        }

        private void CheckKeyDown()
        {

            if(keyBoard != prevKeyState)
            {
                //Key commands will be put here
            }
        }

        private Mouse CheckMouse()
        {
           
            if(mouse != prevMouseState)
            {
                if(mouse.RightButton == ButtonState.Pressed)
                {
                    Move(mouse.Position.ToVector2());
                } 
                else if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Select = true;
                } else
                {
                    Select = false;
                }
            } else if(Select)
            {

            }
        }
    }
}