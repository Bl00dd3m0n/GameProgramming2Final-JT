using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.CommandPattern.GameCommands;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.Input;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.UIComponents;
using _0x46696E616C.Units.Attacks;
using _0x46696E616C.Util.Collision;
using _0x46696E616C.Util.Input;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using _0x46696E616C.WorldManager.WorldImplementations.Buildings.HostileBuidlings;
using MainMenu.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MobHandler.HostileMobManager;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using NationBuilder.WorldHandlerLibrary;
using System;
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
        CommandComponent cc;
        Camera cam;
        CommandProccesor process;
        InputDefinitions input;
        Overlay overlay;
        WaveManager wave;
        ProjectileManager projectileManager;
        CollisionHandler collision;
        WorldHandler world;
        public bool InProgress { get; private set; }
        private bool newGame;
        public ActualGame(Game game, string World) : base(game)
        {
            newGame = true;
        }

        public void StartGame()
        {
            ContentHandler.LoadContent(Game);
            SetUpGame();
            InProgress = true;
        }

        private void SetUpGame()
        {
            if (world != null)
            {
                world.ResetWorld(Game);
            }
            //Create a new world
            else
            {
                world = new WorldHandler(Game, "TempWorld");
            }
            //318,98 - Temp spawn point until I randomize it
            Vector2 startPoint = new Vector2(318, 98);
            collision = new CollisionHandler(Game, world);
            world.AddCollision(collision);
            //Probably could be moved to a save file to setup templates for start
            #region Resource Setup  
            //Initialize the new wallet to start with....this can probably be moved to a file
            Wallet startingResources = new Wallet(new Dictionary<IResource, float>() { { new Wood(), 500 }, { new Steel(), 500 }, { new Money(), 500 }, { new Likes(), 500 }, { new Iron(), 500 }, { new Energy(), 500 } });
            #endregion
            #region util setup
            if (newGame)
            {
                //creates a new input handler instance
                input = InputDefinitions.CreateInput(Game);
                cam = new Camera(Game, input, world, startPoint);
            }
            #endregion
            projectileManager = new ProjectileManager(Game, cam, collision);
            #region componenets
            //Game components
            cc = new CommandComponent(Game, startingResources, world);

            process = new CommandProccesor(Game, new List<IUnit>(), world, input, cc, cam);
            overlay = new Overlay(Game, input, world, process);

            #endregion
            //Wave handler
            wave = new WaveManager(Game, world, projectileManager);
            //Adds a unit to start
            #region startUnits
            List<IUnit> units = new List<IUnit>();
            units.Add(new Civilian("Base unit", new Vector2(1, 1), 100, 100, startPoint + new Vector2(4, 5), BaseUnitState.Idle, TextureValue.Civilian, TextureValue.Civilian, 1, projectileManager, cc.TeamStats).AddQueueables());
            world.AddMob(units[0]);
            ((BasicUnit)units[0]).SetTeam(1);
            #endregion

            #region buildingPlacement
            #region Allies
            //Center
            Center center = new Center(TextureValue.Center, startPoint, TextureValue.CenterIcon, projectileManager, cc.TeamStats);
            center.AddQueueables();
            center.SetTeam(cc.Team);
            center.SetSpawn(startPoint + center.Size + new Vector2(0, 1));
            center.PlacedTile(GraphicsDevice);
            world.Place(center, startPoint);
            center.Subscribe((IBuildingObserver)cc);
            center.Damage(-5000);
            #endregion
            #region Enemies
            //Portal
            startPoint = new Vector2(82, 190);
            for (int i = 0; i < 3; i++)
            {
                Portal portal = new Portal(TextureValue.Portal, startPoint, TextureValue.Portal, projectileManager, wave.TeamStats, wave);
                startPoint += new Vector2(i * 8, 0);
                portal.SetTeam(cc.Team + 1);
                portal.SetSpawn(startPoint + portal.Size + new Vector2(0, 1));
                portal.PlacedTile(GraphicsDevice);
                world.Place(portal, startPoint);
                portal.Subscribe((IBuildingObserver)cc);
            }
            #endregion
            #endregion
            CommandButton exit = new CommandButton(GraphicsDevice, new ExitGameCommand(), new Vector2(GraphicsDevice.Viewport.Width - 50, 0), TextureValue.None, new Point(50, 30));
            exit.Scale = 1;
            exit.color = Color.SlateGray;
            exit.Text = "Exit";
            exit.Draw(GraphicsDevice);
            overlay.AddComponent(exit);


            //Initializer
            overlay.Initialize();
            newGame = false;
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
            if (InProgress)
            {
                cam.Update(gameTime);
                input.Update(gameTime);
                wave.Update(gameTime);
                projectileManager.Update(gameTime);
                process.Update(gameTime);
                cc.Update(gameTime);
                collision.Update(gameTime);
                overlay.Update(gameTime);
            }
            if (InProgress && (cc.IsGameOver /*|| wave.Won*/))
            {
                InProgress = false;
                CommandComponent.ID = 0;
                world.Clear();
            }
            /*if(InProgress && (cc.SelectedBuild == null && cc.SelectedUnits.Count > 0) && input.CheckInput(Controls.Deselect))
            {
                Clean();
                InProgress = false;
            }*/
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        bool debug = false;
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            if (InProgress && !cc.IsGameOver)
            {
                cam.Draw(spriteBatch);
                projectileManager.Draw(spriteBatch);
                overlay.Draw(spriteBatch);
                spriteBatch.Begin();
                spriteBatch.DrawString(ContentHandler.Font, cc.Time(), new Vector2(700, 0), Color.White);
                if (debug)
                {
                    spriteBatch.DrawString(ContentHandler.Font, $"{cam.Position.ToPoint() + (input.InputPos / (Tile.Zoom * 16)).ToPoint()}", new Vector2(0, 20), Color.White);
                    spriteBatch.DrawString(ContentHandler.Font, $"{cam.Position.ToPoint()} {Tile.Zoom}", new Vector2(0, 40), Color.White);
                    spriteBatch.DrawString(ContentHandler.Font, $"{cam.Position.ToPoint()} {Tile.Zoom}", new Vector2(0, 40), Color.White);
                    spriteBatch.DrawString(ContentHandler.Font, $"{2f / Tile.Zoom}", new Vector2(0, 60), Color.White);
                }
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
