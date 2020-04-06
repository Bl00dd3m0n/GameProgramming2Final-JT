using _0x46696E616C.CommandPattern;
using _0x46696E616C.Input;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using NationBuilder.WorldHandlerLibrary;
using System.Collections.Generic;
using Util;
using WorldManager;

namespace _0x46696E616C
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CommandComponent cc;
        Camera cam;
        CommandProccesor process;
        MouseKeyboard input;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //this.graphics.IsFullScreen = true;
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
            WorldHandler world = new WorldHandler(this, "TempWorld");

            Wallet startingResources = new Wallet();
            startingResources.Deposit(new Steel(), 500);
            startingResources.Deposit(new Money(), 500);
            startingResources.Deposit(new Likes(), 500);
            startingResources.Deposit(new Iron(), 500);
            startingResources.Deposit(new Energy(), 500);
            input = new MouseKeyboard(this, spriteBatch);
            cam = new Camera(this, input, world);
            cc = new CommandComponent(this, startingResources);
            process = new CommandProccesor(this, new List<MobHandler.Units.IUnit>(), world, input, cc, cam);

            cam.Initialize();
            process.Initialize();
            input.Initialize();
            ContentHandler.LoadContent(this);
            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            cam.Update(gameTime);
            process.Update(gameTime);
            input.Update(gameTime);
            cc.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            cam.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(ContentHandler.font, cc.Resources(), new Vector2(100, 0), Color.Black);
            spriteBatch.DrawString(ContentHandler.font, cc.Time(), new Vector2(700,0), Color.Black);
            spriteBatch.DrawString(ContentHandler.font, $"{cam.Position.ToPoint()+(input.inputPos / 16).ToPoint()}", new Vector2(0, 0), Color.Black);
            spriteBatch.Draw(ContentHandler.DrawnTexture(TextureValue.Cursor), Mouse.GetState().Position.ToVector2(), null, Color.Red, 0, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
