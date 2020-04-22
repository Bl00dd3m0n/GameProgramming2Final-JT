using _0x46696E616C.Util.Input;
using MainMenu.Component;
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
        public Vector2 InputPos { get; protected set; }
        public Vector2 selectionStart { get; protected set; }
        public Vector2 selectionEnd { get; protected set; }
        protected float clickTimer;
        public InputHandler(Game game) : base(game)
        {
        }

        protected void UpdateCursorPosition(Vector2 position)
        {
            this.InputPos = position;
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
        public float prevScrollVal { get; private set; }
        public bool Updated { get; private set; }
        bool Subscribed;
        bool leftButtonPressed, leftButtonReleased, rightButtonPressed;
        List<InputButton> listenForPress;
        public MouseKeyboard(Game game) : base(game)
        {

            keyBoard = Keyboard.GetState();
            prevKeyState = Keyboard.GetState();
            mouse = Mouse.GetState();
            prevMouseState = mouse;
            PressedKeys = new List<Keys>();
            leftButtonPressed = rightButtonPressed = false;
            listenForPress = new List<InputButton>();
        }

        public override void Update(GameTime gameTime)
        {
            clickTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (Game.IsActive)//Stops updating inputs if the player is outside the game
            {
                if (listenForPress.Count > 0 && !Subscribed) CheckForInput();
                UpdateKeyBoardState();
                UpdateMouseState();
                UpdateCursorPosition(mouse.Position.ToVector2());
            }
            Pressed = false;
            base.Update(gameTime);
            Subscribed = false;
        }

        private void CheckForInput()
        {
            List<MouseKeyboardBindings> controls = new List<MouseKeyboardBindings>();
            if (LeftClick())
            {
                controls.Add(new MouseKeyboardBindings(MouseInput.LeftClick));
            }
            else if (RightClick())
            {
                controls.Add(new MouseKeyboardBindings(MouseInput.RightClick));
            }
            else if (scrollVal > prevScrollVal)
            {
                controls.Add(new MouseKeyboardBindings(MouseInput.ScrollUp));
            }
            else if (scrollVal < prevScrollVal)
            {
                controls.Add(new MouseKeyboardBindings(MouseInput.ScrollDown));
            }
            else if (keyBoard.GetPressedKeys().Length > 0)
            {
                foreach (Keys key in keyBoard.GetPressedKeys())
                    controls.Add(new MouseKeyboardBindings(key));
            }
            if (controls.Count > 0)
            {
                foreach (InputButton button in listenForPress)
                {
                    button.UpdateControl(controls[0]);
                }
                listenForPress.Clear();
            }
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

        internal void Subscribe(InputButton inputButton)
        {
            listenForPress.Clear();
            listenForPress.Add(inputButton);
            Subscribed = true;
        }

        private void UpdateMouseState()
        {
            mouse = Mouse.GetState();
            leftButtonPressed = leftButtonReleased = rightButtonPressed = false;

            if (mouse.LeftButton != prevMouseState.LeftButton || mouse.RightButton != prevMouseState.RightButton)//Position changes mean a different state - this if statement needs to only change if the mouse is clicked
            {
                Updated = true;
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    clickTimer = 0;
                    selectionStart = InputPos;
                    leftButtonPressed = true;
                }
                if (mouse.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed && clickTimer / 1000 >= 0.2)
                {
                    leftButtonReleased = true;
                    selectionEnd = InputPos;
                    clickTimer = 0;
                }
                if (mouse.RightButton == ButtonState.Pressed)
                    rightButtonPressed = true;
                prevMouseState = mouse;
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
            if (Updated && PressedKeys.Contains(key))
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
        public bool LeftRelease()
        {
            return leftButtonReleased;
        }
    }
}