using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.Input;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.UIComponents;
using _0x46696E616C.Units.Attacks;
using _0x46696E616C.Util.Collision;
using _0x46696E616C.Util.Input;
using _0x46696E616C.WorldManager.WorldImplementations.Buildings.HostileBuidlings;
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
using WorldManager.MapData;

namespace _0x46696E616C
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ActualGame : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        CommandComponent cc;
        Camera cam;
        CommandProccesor process;
        InputDefinitions input;
        Overlay overlay;
        WaveManager wave;
        ProjectileManager projectileManager;
        CollisionHandler collision;
        public bool InProgress { get; private set; }
        public ActualGame(Game game) : base(game)
        {
            InProgress = true;
        }
        private void Clean()
        {
            Game.Components.Clear();
            spriteBatch = null;
            cc = null;
            cam = null;
            process = null;
            input = null;
            overlay = null;
            wave = null;
            projectileManager = null;
            collision = null;
            InProgress = false;
            CommandComponent.ID = 0;
            ContentHandler.Clear();
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
            SetUpGame();


        }

        private void SetUpGame()
        {
            //Create a new world
            WorldHandler world = new WorldHandler(Game, "TempWorld");
            //318,98 - Temp spawn point until I randomize it
            Vector2 startPoint = new Vector2(318, 98);
            collision = new CollisionHandler(Game, world);
            world.AddCollision(collision);
            //Probably could be moved to a save file to setup templates for start
            #region Resource Setup  
            //Initialize the new wallet to start with....this can probably be moved to a file
            Wallet startingResources = new Wallet();
            startingResources.Deposit(new Wood(), 500);
            startingResources.Deposit(new Steel(), 500);
            startingResources.Deposit(new Money(), 500);
            startingResources.Deposit(new Likes(), 500);
            startingResources.Deposit(new Iron(), 500);
            startingResources.Deposit(new Energy(), 500);
            #endregion
            #region util setup
            //creates a new input handler instance
            input = new InputDefinitions(Game);

            cam = new Camera(Game, input, world, startPoint);
            #endregion

            //Adds a unit to start
            #region startUnits
            List<IUnit> units = new List<IUnit>();
            units.Add(new Civilian("Base unit", new Vector2(1, 1), 100, 100, startPoint + new Vector2(4, 5), BaseUnitState.Idle, TextureValue.Civilian, world, TextureValue.Civilian,1));
            world.AddMob(units[0]);
            ((BasicUnit)units[0]).SetTeam(1);
            #endregion
            #region componenets
            //Game components
            cc = new CommandComponent(Game, startingResources, units, world);
            process = new CommandProccesor(Game, new List<IUnit>(), world, input, cc, cam);
            overlay = new Overlay(Game, input, world, process);
            #endregion
            #region buildingPlacement
            #region Allies
            //Center
            Center center = new Center(TextureValue.Center, startPoint, TextureValue.CenterIcon);
            center.SetTeam(cc.Team);
            center.SetSpawn(startPoint + center.Size + new Vector2(0, 1));
            center.AddQueueable(((Civilian)units[units.Count - 1]).NewInstace(100, startPoint));
            center.PlacedTile();
            world.Place(center, startPoint);
            center.Subscribe((IBuildingObserver)cc);
            #endregion
            #region Enemies
            //Portal
            Portal portal = new Portal(TextureValue.Portal, startPoint + new Vector2(0, 20), TextureValue.Portal);
            portal.SetTeam(cc.Team + 1);
            portal.SetSpawn(startPoint + portal.Size + new Vector2(0, 1));
            portal.PlacedTile();
            world.Place(portal, startPoint + new Vector2(0, 20));
            portal.Subscribe((IBuildingObserver)cc);
            #endregion
            #endregion
            projectileManager = new ProjectileManager(Game, world, cam, collision);
            //Wave handler
            wave = new WaveManager(Game, world, projectileManager);

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
        bool EndRun;
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.X))
            {
                EndRun = true;
            }
            if (/*Game.IsActive &&*/ (!EndRun && !cc.IsGameOver))
            {
                //wave.Update(gameTime);
                cam.Update(gameTime);
                projectileManager.Update(gameTime);
                process.Update(gameTime);
                input.Update(gameTime);
                cc.Update(gameTime);
                collision.Update(gameTime);
            }
            else if (/*cc.IsGameOver || */EndRun)
            {
                Clean();
                InProgress = false;
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
            if (/*Game.IsActive && */InProgress && (!cc.IsGameOver || !EndRun))
            {
                cam.Draw(gameTime);
                projectileManager.Draw(spriteBatch);
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
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
