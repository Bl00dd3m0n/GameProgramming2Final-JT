using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.Input;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.UIComponents;
using _0x46696E616C.Util.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MobHandler.HostileMobManager;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using NationBuilder.WorldHandlerLibrary;
using System.Collections.Generic;
using TechHandler;
using UIProject;
using Util;
using WorldManager;

namespace _0x46696E616C
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ActualGame : DrawableGameComponent
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CommandComponent cc;
        Camera cam;
        CommandProccesor process;
        InputDefinitions input;
        Overlay overlay;
        WaveManager wave;
        public ActualGame(Game game) : base(game)
        {
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public override void Initialize()
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
            ContentHandler.LoadContent(Game);
            //Create a new world
            WorldHandler world = new WorldHandler(Game, "TempWorld");
            //Initialize the new wallet to start with....this can probably be moved to a file
            Wallet startingResources = new Wallet();
            startingResources.Deposit(new Wood(), 500);
            startingResources.Deposit(new Steel(), 500);
            startingResources.Deposit(new Money(), 500);
            startingResources.Deposit(new Likes(), 500);
            startingResources.Deposit(new Iron(), 500);
            startingResources.Deposit(new Energy(), 500);
            //creates a new input handler instance
            input = new InputDefinitions(Game);
            //318,98 - Temp spawn point until I randomize it
            Vector2 startPoint = new Vector2(318, 98);
            cam = new Camera(Game, input, world, startPoint);
            //Adds a unit to start
            List<IUnit> units = new List<IUnit>();
            units.Add(new Civilian("Base unit", new Vector2(1, 1), 100, 100, startPoint + new Vector2(4, 5), BaseUnitState.Idle, TextureValue.Civilian, world, TextureValue.Civilian));
            world.AddMob(units[0]);
            ((BasicUnit)units[0]).SetTeam(1);

            //Game components
            cc = new CommandComponent(Game, startingResources, units, world);
            process = new CommandProccesor(Game, new List<IUnit>(), world, input, cc, cam);
            overlay = new Overlay(Game, input, world, process);

            //Center
            Center center = new Center(TextureValue.Center, startPoint, TextureValue.CenterIcon);
            center.SetTeam(cc.Team);
            center.SetSpawn(startPoint + center.Size + new Vector2(0, 1));
            center.AddQueueable(((Civilian)units[units.Count - 1]).NewInstace(100, startPoint));
            center.PlacedTile();
            world.Place(center, startPoint);
            center.Subscribe(cc);
            //Wave handler
            wave = new WaveManager(Game, world);

            //Initializer
            wave.Initialize();
            cam.Initialize();
            overlay.Initialize();
            process.Initialize();
            world.Save("WorldCheck.wrld");
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
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.X))
            {
                Game.Exit();
            }
            if (Game.IsActive && !cc.IsGameOver)
            {
                wave.Update(gameTime);
                cam.Update(gameTime);
                process.Update(gameTime);
                input.Update(gameTime);
                cc.Update(gameTime);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        bool debug = false;
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            if (Game.IsActive && !cc.IsGameOver)
            {
                cam.Draw(gameTime);
                overlay.Draw(gameTime);
                spriteBatch.Begin();
                spriteBatch.DrawString(ContentHandler.Font, cc.Time(), new Vector2(700, 0), Color.White);
                if (debug)
                {
                    spriteBatch.DrawString(ContentHandler.Font, $"{cam.Position.ToPoint() + (input.InputPos / (Tile.Zoom * 16)).ToPoint()}", new Vector2(0, 20), Color.White);
                    spriteBatch.DrawString(ContentHandler.Font, $"{cam.Position.ToPoint()} {Tile.Zoom}", new Vector2(0, 40), Color.White);
                    spriteBatch.DrawString(ContentHandler.Font, $"{cam.Position.ToPoint()} {Tile.Zoom}", new Vector2(0, 40), Color.White);
                    spriteBatch.DrawString(ContentHandler.Font, $"{2f / Tile.Zoom}", new Vector2(0, 60), Color.White);
                }
                spriteBatch.Draw(ContentHandler.DrawnTexture(TextureValue.Cursor), Mouse.GetState().Position.ToVector2(), null, Color.Red, 0, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0);

                //canvas.Draw(gameTime);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
