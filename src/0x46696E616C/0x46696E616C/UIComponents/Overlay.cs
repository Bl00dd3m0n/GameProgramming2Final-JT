using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.GameCommands;
using _0x46696E616C.Input;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.Util.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using TechHandler;
using UIProject;
using Util;
using WorldManager;

namespace _0x46696E616C.UIComponents
{
    class Overlay : Canvas
    {
        WorldHandler world;
        InputDefinitions input;
        CommandProccesor cp;
        Texture2D OverlayTexture;
        Texture2D CameraView;
        DescriptionBox description;
        Vector2 ZeroVector;
        public Overlay(Game game, InputDefinitions input, WorldHandler world, CommandProccesor command) : base(game)
        {
            cp = command;
            this.world = world;
            this.input = input;
            cp.overlay = this;
            ZeroVector = Vector2.Zero;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            ComponentOverlay();
            spriteBatch.Draw(OverlayTexture, Vector2.Zero, Color.White);
            DrawMap();
            DrawText();
            foreach (CommandButton button in components.Where(l => l is CommandButton))//For all queueable objects if you can afford it, it shows up normally if not it shows up red
            {
                if (button.command is BuildSelectCommand)
                {
                    if (!cp.cc.CheckCost(((BuildSelectCommand)button.command).build))
                    {
                        button.Color = Color.Red;
                    }
                    else
                    {
                        button.Color = Color.White;
                    }
                }
            }
            base.Draw(spriteBatch);
            if (description.drawComponent)
            {
                spriteBatch.Draw(description.picture, description.Position, description.Color);
                spriteBatch.DrawString(ContentHandler.Font, description.Text, description.Position, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            spriteBatch.End();
        }
        /// <summary>
        /// Drawing for selected buildings and the spawn marker(Visual placement markers - Buildings/spawn markers)
        /// </summary>
        private void ComponentOverlay()
        {
            if (cp.cc.SelectedBuild != null)
            {
                Building build = cp.cc.SelectedBuild;
                if (world.CheckPlacement(cp.CurrentPos, build.Size))
                {
                    spriteBatch.Draw(ContentHandler.DrawnTexture(build.block.texture), (cp.CurrentPos * Tile.Zoom * 16) - (cp.camera.Position * Tile.Zoom * 16), null, Color.Green, 0, ZeroVector, Tile.Zoom, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(ContentHandler.DrawnTexture(build.block.texture), (cp.CurrentPos * Tile.Zoom * 16) - (cp.camera.Position * Tile.Zoom * 16), null, Color.Red, 0, ZeroVector, Tile.Zoom, SpriteEffects.None, 0);
                }
            }
            else if (cp.cc.SpawnMarker != null)
            {
                spriteBatch.Draw(cp.cc.SpawnMarker, (cp.CurrentPos * Tile.Zoom * 16) - (cp.camera.Position * Tile.Zoom * 16), null, Color.Red, 0, ZeroVector, Tile.Zoom, SpriteEffects.None, 0);
            }
        }
        /// <summary>
        /// The command processor calls the overlay to see if a button was clicked with a command if a command button was clicked return the command
        /// </summary>
        /// <returns></returns>
        internal Command ClickCheck()
        {
            IComponent component = components.Find(x => x.bounds.Contains(input.InputPos));
            if (component != null)
            {
                if (component.Description() != null)
                {
                    if (component is CommandButton)
                    {
                        if (((CommandButton)component).command is BuildSelectCommand || ((CommandButton)component).command is TrainCommand || ((CommandButton)component).command is SetSpawnPointCommand)
                        {
                            description.Text = component.Description();
                            description.drawComponent = true;
                            description.Size = ContentHandler.Font.MeasureString(description.Text).ToPoint();
                            description.Draw(GraphicsDevice);
                            description.Position = input.InputPos - description.Size.ToVector2();
                        }
                    }
                }

                if (input.CheckInput(Controls.Select))
                {
                    if (component is CommandButton)//If the user can afford to train/build things it returns the selected unit, if not it returns null
                    {
                        if (((CommandButton)component).command is BuildSelectCommand)
                        {
                            BuildSelectCommand command = (BuildSelectCommand)((CommandButton)component).command;
                            if (cp.cc.CheckCost(command.build))
                            {
                                return (Command)((CommandButton)component).command;
                            }
                        }
                        else
                        {
                            return (Command)((CommandButton)component).command;
                        }
                    }
                }
            }
            else
            {
                description.drawComponent = false;
            }
            return null;
        }

        private void DrawSelectionDetails()
        {

        }
        /// <summary>
        /// For now it just draws the resource text
        /// </summary>
        private void DrawText()
        {
            List<string> resources = cp.cc.Resources();
            foreach (string resource in resources)
            {
                spriteBatch.DrawString(ContentHandler.Font, resource, new Vector2((resources.FindIndex(l => l == resource) * 115) + 13, 5), Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            OverlayTexture = Game.Content.Load<Texture2D>("Overlay");
            DrawViewPortRepresentation();
            description = new DescriptionBox(new Point(550, 200));
            description.Draw(GraphicsDevice);
            base.LoadContent();
        }
        /// <summary>
        ///Get the viewport and draw it visually on the map
        /// </summary>
        private void DrawViewPortRepresentation()
        {

            Color[] bounds = new Color[cp.camera.Size.X * cp.camera.Size.Y];
            for (int y = 0; y < cp.camera.Size.Y; y++)
            {
                for (int x = 0; x < cp.camera.Size.X; x++)
                {
                    if (x == 0 || y == 0 || x == cp.camera.Size.X - 1 || y == cp.camera.Size.Y - 1)
                    {
                        bounds[x + y * cp.camera.Size.X] = Color.White;
                    }
                }
            }
            CameraView = new Texture2D(GraphicsDevice, cp.camera.Size.X, cp.camera.Size.Y);
            CameraView.SetData(bounds, 0, cp.camera.Size.X * cp.camera.Size.Y);
        }

        private void DrawMap()
        {
            //Draw the worlds map scaled down
            float scale = 0.5f;
            Vector2 Position = new Vector2(0, GraphicsDevice.Viewport.Height) - new Vector2(-8, world.GetSize().Y * scale);
            spriteBatch.Draw(world.getMap(), Position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(CameraView, (cp.camera.Position * scale) + Position, null, Color.White, 0, ZeroVector, (scale / Tile.Zoom), SpriteEffects.None, 0);
        }
    }
}
