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
        Texture2D Cursor;
        Canvas canv;
        MouseKeyboard mK;
        bool StartGame;
        StyleSheet ss;
        InputDefinitions inputDef;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            MainMenuBackground = this.Content.Load<Texture2D>("MainMenu");
            Cursor = this.Content.Load<Texture2D>("Cursor");
            canv = new Canvas(this);
            ss = new StyleSheet();


            inputDef = new InputDefinitions(this);
            //GenerateStyleSheet("MainMenu");
            //GenerateStyleSheet("SettingsPage");
            canv.LoadCanvas(ss.GetStyleSheet(GraphicsDevice, "MainMenu.ss", StyleSheet.ComponentTypes));
            //canv.LoadCanvas(ss.GetStyleSheet(GraphicsDevice, "SettingsPage.ss", StyleSheet.ComponentTypes));
            MouseKeyboard keyboard = new MouseKeyboard(this);
            canv.Initialize();
            mK = new MouseKeyboard(this);
            this.Components.Add(mK);

            //StyleSheet ss = new StyleSheet("MainMenu.ss");
            //canv.LoadCanvas(ss.GetStyleSheet(GraphicsDevice));
            // TODO: use this.Content to load your game content here
        }

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
            int y = 90;
            Label label = new Label(new Vector2(350, y), "Settings", Color.LimeGreen);
            canv.AddComponent(label);
            for (int i = 0; i < Enum.GetNames(typeof(Controls)).Length; i++)
            {
                y += 37;
                label = new Label(new Vector2(200, y), ((Controls)i).ToString(), Color.Black);
                InputButton input = new InputButton(GraphicsDevice, new Vector2(500, y), new Point(200, 32), Color.LightSlateGray, inputDef.GetControls((Controls)i), this, (Controls)i, inputDef);
                canv.AddComponent(label);
                canv.AddComponent(input);
  
            }
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
                    Exit();
                canv.Update(gameTime);
                // TODO: Add your update logic here
                if (inputDef.CheckInput(Controls.Select))
                {
                    Button button = canv.CheckClick(mK.InputPos.ToPoint(), inputDef);
                    if (button is StartButton)
                    {
                        this.Components.Remove(mK);
                        StartGame = true;
                    }
                }
                inputDef.Update(gameTime);
            }
            base.Update(gameTime);
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
                    canv.LoadCanvas(ss.GetStyleSheet(GraphicsDevice, "MainMenu.ss", new System.Type[] { typeof(Button), typeof(StartButton), typeof(ExitButton), typeof(Label) }));
#endif
                spriteBatch.Draw(MainMenuBackground, new Vector2(0), Color.White);
                canv.Draw(spriteBatch);
                spriteBatch.Draw(Cursor, Mouse.GetState().Position.ToVector2(), null, Color.Red, 0, Vector2.Zero, 0.25f, SpriteEffects.None, 0);
                spriteBatch.End();
                // TODO: Add your drawing code here
            }
            base.Draw(gameTime);

        }
    }
}
