using _0x46696E616C;
using _0x46696E616C.Input;
using _0x46696E616C.UIComponents;
using _0x46696E616C.Util.Input;
using MainMenu.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using UIProject;

namespace MainMenu
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D MainMenuBackground;
        Texture2D SettingMenuBackground;
        Texture2D Cursor;
        Canvas canv;
        MouseKeyboard mK;
        bool StartGame;
        StyleSheet ss;
        InputDefinitions inputDef;
        string CurrentPage;
        ActualGame PlayedGame;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        private void ResetEverything()
        {
            graphics = null;
            spriteBatch = null;
            Cursor = null;
            canv = null;
            mK = null;
            StartGame = false;
            ss = null;
            inputDef = null;
            CurrentPage = null;
            PlayedGame = null;
            LoadContent();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            if (MainMenuBackground == null && SettingMenuBackground == null)
            {
                MainMenuBackground = this.Content.Load<Texture2D>("MainMenu");
                SettingMenuBackground = this.Content.Load<Texture2D>("SettingsPage");
            }
            Cursor = this.Content.Load<Texture2D>("Cursor");
            canv = new Canvas(this);
            ss = new StyleSheet();

            inputDef = new InputDefinitions(this);
            LoadCanvas("MainMenu.ss");
            CurrentPage = "Main Menu";
            MouseKeyboard keyboard = new MouseKeyboard(this);
            canv.Initialize();
            mK = new MouseKeyboard(this);
            this.Components.Add(mK);

        }
        /// <summary>
        /// Generates a specified style sheet by call
        /// </summary>
        #region Templates for Style Sheets
        private void MainMenu()
        {
            var startButton = new StartButton(GraphicsDevice, new Vector2(200, 140), new Point(400, 50), Color.LightGreen, "Start", this);
            var settings = new PageButton(GraphicsDevice, new Vector2(200, 200), new Point(400, 50), Color.LightGreen, "Settings", this, "SettingsPage.ss");
            var exitButton = new ExitButton(GraphicsDevice, new Vector2(200, 260), new Point(400, 50), Color.LightGreen, "Exit", this);
            startButton.Scale = 1;
            settings.Scale = 1;
            exitButton.Scale = 1;
            canv.AddComponent(startButton);
            canv.AddComponent(settings);
            canv.AddComponent(exitButton);
            ss.SaveStyleSheet(canv.Components, "MainMenu.ss");
            canv.RemoveAllComponents();
        }
        private void SettingsPage()
        {
            int y = 60;
            for (int i = 0; i < Enum.GetNames(typeof(Controls)).Length; i++)
            {
                y += 37;
                InputButton input = new InputButton(GraphicsDevice, new Vector2(400, y), new Point(200, 32), Color.LightGreen, inputDef.GetControls((Controls)i), this, (Controls)i, inputDef);
                Label label = new Label(new Vector2(250, y + (input.Size.Y / 2)), ((Controls)i).ToString(), Color.Black);
                canv.AddComponent(label);
                canv.AddComponent(input);

            }
            PageButton MainMenu = new PageButton(GraphicsDevice, new Vector2(270, y + 37), new Point(200, 32), Color.LightGreen, "Main Menu", this, "MainMenu.ss");
            canv.AddComponent(MainMenu);
            ss.SaveStyleSheet(canv.Components, "SettingsPage.ss");
            canv.RemoveAllComponents();
        }
        private void GenerateStyleSheet(string Path)
        {
            switch (Path)
            {
                case "MainMenu":
                    MainMenu();
                    break;
                case "SettingsPage":
                    SettingsPage();
                    break;
            }
        }
        #endregion
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!StartGame)
            {
                if (inputDef.CheckInput(Controls.Deselect))
                {
                    if (CurrentPage == "MainMenu")
                    {
                        Exit();
                    }
                    else
                    {
                        LoadCanvas("MainMenu.ss");
                    }
                }
                canv.Update(gameTime);
                // TODO: Add your update logic here
                if (inputDef.CheckInput(Controls.Select))
                {
                    Button button = canv.CheckClick(mK.InputPos.ToPoint(), inputDef);
                    if (button is StartButton)
                    {
                        //this.Components.Remove(mK);
                        canv.RemoveAllComponents();
                        PlayedGame = ((StartButton)button).LoadedGame();
                        PlayedGame.Initialize();
                        //this.Components.Add(PlayedGame);
                        StartGame = true;
                    }
                    else if (button is PageButton)
                    {
                        CurrentPage = button.Text;
                    }
                }
                inputDef.Update(gameTime);
            }
            else
            {
                PlayedGame.Update(gameTime);
            }
            base.Update(gameTime);
        }
        private void LoadCanvas(string Path)
        {
            canv.LoadCanvas(ss.GetStyleSheet(GraphicsDevice, Path, StyleSheet.ComponentTypes));
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!StartGame)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();
#if DEBUG
                if (Keyboard.GetState().IsKeyDown(Keys.U))
                {
                    if (CurrentPage == "Main Menu")
                    {
                        LoadCanvas("MainMenu.ss");
                    }
                    else
                    {
                        LoadCanvas("SettingsPage.ss");
                    }
                }

#endif
                if (CurrentPage == "Main Menu")
                {
                    spriteBatch.Draw(MainMenuBackground, new Vector2(0), Color.White);
                }
                else
                {
                    spriteBatch.Draw(SettingMenuBackground, new Vector2(0), Color.White);
                }
                canv.Draw(spriteBatch);
                spriteBatch.Draw(Cursor, Mouse.GetState().Position.ToVector2(), null, Color.Red, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 0);
                spriteBatch.End();
                // TODO: Add your drawing code here
            }
            if (PlayedGame != null && !PlayedGame.InProgress)
            {
                this.Components.Clear();
                ResetEverything();
            }
            else if (PlayedGame != null)
            {
                PlayedGame.Draw(gameTime);
            }
            base.Draw(gameTime);

        }
    }
}
