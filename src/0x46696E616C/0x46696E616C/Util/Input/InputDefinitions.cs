using _0x46696E616C.Input;
using MainMenu.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using SaveManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.Util.Input
{
    public enum Controls { Select, Interact, ZoomIn, ZoomOut, Deselect, Up, Down, Left, Right }
    public class InputDefinitions
    {
        [JsonIgnore]
        Dictionary<Controls, MouseKeyboardBindings> inputs;

        public Dictionary<Controls, MouseKeyboardBindings> Input { get; set; }
        [JsonIgnore]
        MouseKeyboard input;
        [JsonIgnore]
        public Vector2 InputPos { get { return input.InputPos; } }
        [JsonIgnore]
        public Vector2 StartPosition { get { return input.selectionStart; } }
        [JsonIgnore]
        public Vector2 EndPosition { get { return input.selectionEnd; } }
        //String path for saving settings
        [JsonIgnore]
        private const string SaveFile = "Settings.json";
        [JsonIgnore]
        SaveJson<InputDefinitions> JsonControls;
        static bool StartedLoad;
        public InputDefinitions(Game game) : this()
        {
            input = new MouseKeyboard(game);
            input.Initialize();
            StartedLoad = false;
        }

        public InputDefinitions()
        {
            if (!StartedLoad)
            {
                JsonControls = new SaveJson<InputDefinitions>();
                if (File.Exists(SaveFile))//TODO Tie this to a file singleton so it isn't windows based
                {
                    LoadSettings();
                }
                else
                {
                    GenerateSettings();
                }
            }
            //Todo implement for multiple inputs

        }

        private void GenerateSettings()
        {
            inputs = new Dictionary<Controls, MouseKeyboardBindings>();

            MouseKeyboardBindings keys = new MouseKeyboardBindings(MouseInput.LeftClick);
            inputs.Add(Controls.Select, keys);

            keys = new MouseKeyboardBindings(MouseInput.RightClick);
            inputs.Add(Controls.Interact, keys);

            keys = new MouseKeyboardBindings(MouseInput.ScrollUp);
            inputs.Add(Controls.ZoomIn, keys);

            keys = new MouseKeyboardBindings(MouseInput.ScrollDown);
            inputs.Add(Controls.ZoomOut, keys);

            keys = new MouseKeyboardBindings(Keys.Escape);
            inputs.Add(Controls.Deselect, keys);

            keys = new MouseKeyboardBindings(Keys.W);
            inputs.Add(Controls.Up, keys);

            keys = new MouseKeyboardBindings(Keys.A);
            inputs.Add(Controls.Left, keys);

            keys = new MouseKeyboardBindings(Keys.S);
            inputs.Add(Controls.Down, keys);

            keys = new MouseKeyboardBindings(Keys.D);
            inputs.Add(Controls.Right, keys);
            SaveSettings();
        }

        internal void ListenForInput(InputButton inputButton)
        {
            input.Subscribe(inputButton);
        }

        public string GetControls(Controls ctrl)
        {
            string Control = "";
            MouseKeyboardBindings binding = inputs[ctrl];
            if (binding.isMouseType)
                Control += $"{binding.mouse.ToString()} ";
            else
                Control += $"{binding.keys.ToString()} ";
            return Control;
        }

        public void SetBinds(Controls control, MouseKeyboardBindings input)
        {
            inputs[control] = input;
        }

        public bool CheckInput(Controls control)
        {
            if (input.Updated)
            {
                MouseKeyboardBindings binding = inputs[control];
                if (binding.isMouseType)
                {
                    switch (binding.mouse)
                    {
                        case MouseInput.LeftClick:
                            return input.LeftClick();
                        case MouseInput.RightClick:
                            return input.RightClick();
                        case MouseInput.ScrollUp:
                            if (input.scrollVal > input.prevScrollVal)
                            {
                                return input.Scrolling();
                            }
                            return false;
                        case MouseInput.ScrollDown:
                            if (input.scrollVal < input.prevScrollVal)
                            {
                                return input.Scrolling();
                            }
                            return false;
                    }
                }
                else
                {
                    return input.GetKeyDown(binding.keys);
                }
            }
            return false;
        }
        public bool CheckRelease(Controls control)
        {
            if (input.Updated)
            {
                MouseKeyboardBindings binding = inputs[control];
                if (binding.isMouseType)
                {
                    switch (binding.mouse)
                    {
                        case MouseInput.LeftClick:
                            return input.LeftRelease();
                    }
                }
                else
                {
                    //Nothing implemented here
                }
            }
            return false;
        }
        public void Update(GameTime gameTime)
        {
            input.Update(gameTime);
        }
        public bool Updated()
        {
            return input.Updated;
        }
        public void LoadSettings()
        {
            StartedLoad = true;
            inputs = JsonControls.LoadFromJson(SaveFile).Input;
        }
        public void SaveSettings()
        {
            JsonControls.SaveToJson(this, SaveFile);
        }
    }
}
