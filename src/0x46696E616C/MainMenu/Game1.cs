﻿using _0x46696E616C;
using _0x46696E616C.Input;
using _0x46696E616C.UIComponents;
using _0x46696E616C.Util.Input;
using MainMenu.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
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
        Texture2D mainMenuBackground;
        Texture2D settingMenuBackground;
        Texture2D Cursor;
        Canvas canv;
        MouseKeyboard mK;
        bool startGame;
        StyleSheet ss;
        InputDefinitions inputDef;
        string currentPage;
        ActualGame PlayedGame;
        /// <summary>
        /// 0 - Main Menu
        /// 1 - Settings Menu
        /// </summary>
        internal StyleSheet[] Pages
        {
            get;
            set;
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Pages = new StyleSheet[] { new StyleSheet(), new StyleSheet() };
        }

        private void ResetEverything()
        {
            startGame = false;
            //LoadCanvas("MainMenu.ss", 0);
            currentPage = "Main Menu";

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

            mainMenuBackground = this.Content.Load<Texture2D>("MainMenu");
            settingMenuBackground = this.Content.Load<Texture2D>("SettingsPage");
            Cursor = this.Content.Load<Texture2D>("Cursor");
            inputDef = InputDefinitions.CreateInput(this);

            MouseKeyboard keyboard = new MouseKeyboard(this);
            mK = new MouseKeyboard(this);
            this.Components.Add(mK);

            canv = new Canvas(this);
            ss = new StyleSheet();
            canv.Initialize();

            if (!File.Exists("MainMenu.ss"))
            {
                GenerateStyleSheet("MainMenu");
            }
            if (!File.Exists("SettingsPage.ss"))
            {
                GenerateStyleSheet("SettingsPage");
            }
            LoadCanvas("MainMenu.ss", 0);
            currentPage = "Main Menu";

            PlayedGame = new ActualGame(this, "World");
            PlayedGame.Initialize();

            base.LoadContent();
        }
        /// <summary>
        /// Generates a specified style sheet by call
        /// </summary>
        #region Templates for Style Sheets
        private void MainMenu()
        {
            var startButton = new StartButton(GraphicsDevice, new Vector2(200, 140), new Point(400, 50), Color.LightGreen, "Start", this);
            var settings = new PageButton(GraphicsDevice, new Vector2(200, 200), new Point(400, 50), Color.LightGreen, "Settings", this, "SettingsPage.ss", 1);
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
            PageButton MainMenu = new PageButton(GraphicsDevice, new Vector2(270, y + 37), new Point(200, 32), Color.LightGreen, "Main Menu", this, "MainMenu.ss", 0);
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
            if (!startGame)
            {
                mK.Update(gameTime);
                if (inputDef.CheckInput(Controls.Deselect))
                {
                    if (currentPage == "MainMenu")
                    {
                        Exit();
                    }
                    else
                    {
                        LoadCanvas("MainMenu.ss", 0);
                    }
                }
                canv.Update(gameTime);
                // TODO: Add your update logic here
                if (inputDef.CheckInput(Controls.Select))
                {
                    Button button = canv.CheckClick(mK.InputPos.ToPoint(), inputDef, Pages);
                    if (button != null)
                        button.Clicked = false;
                    if (button is StartButton)
                    {
                        //this.Components.Remove(mK);
                        //canv.RemoveAllComponents();
                        //PlayedGame = ((StartButton)button).LoadedGame();
                        //PlayedGame.Initialize();
                        PlayedGame.StartGame();
                        startGame = true;
                    }
                    else if (button is PageButton)
                    {
                        LoadCanvas(((PageButton)button).path, ((PageButton)button).PageOrder);
                    }
                }
                inputDef.Update(gameTime);
            }
            else if (PlayedGame != null)
            {
                PlayedGame.Update(gameTime);
            }
            base.Update(gameTime);
        }
        private void LoadCanvas(string Path, int Page)
        {
            int test = Page;
            if (Pages.Length > Page)
                canv.LoadCanvas(Pages[Page].GetStyleSheet(GraphicsDevice, Path, StyleSheet.ComponentTypes));
            else
                canv.LoadCanvas(ss.GetStyleSheet(GraphicsDevice, Path, StyleSheet.ComponentTypes));
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!startGame)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin();
#if DEBUG
                if (Keyboard.GetState().IsKeyDown(Keys.U))
                {
                    if (currentPage == "Main Menu")
                    {
                        LoadCanvas("MainMenu.ss",0);
                    }
                    else
                    {
                        LoadCanvas("SettingsPage.ss",1);
                    }
                }



#endif
                if (Keyboard.GetState().IsKeyDown(Keys.I))
                {
                    Canvas test = new Canvas(this);
                    test.LoadCanvas(Pages[0].GetStyleSheet(GraphicsDevice, "Path", StyleSheet.ComponentTypes));
                    test = null;
                }
                if (currentPage == "Main Menu")
                {
                    spriteBatch.Draw(mainMenuBackground, new Vector2(0), Color.White);
                }
                else
                {
                    spriteBatch.Draw(settingMenuBackground, new Vector2(0), Color.White);
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
            else if (PlayedGame != null && startGame)
            {
                PlayedGame.Draw(gameTime);
            }
            else if (PlayedGame == null && startGame)
            {
                this.Components.Clear();
                ResetEverything();
            }
            base.Draw(gameTime);

        }
    }
}
