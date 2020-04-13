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
        InputHandler input;
        CommandProccesor cp;
        Texture2D OverlayTexture;
        Texture2D CameraView;
        public Overlay(Game game, InputHandler input, WorldHandler world, CommandProccesor command) : base(game)
        {
            cp = command;
            this.world= world;
            this.input = input;
            cp.overlay = this;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (cp.cc.selectedBuild != null)
            {
                Building build = cp.cc.selectedBuild;
                if (world.CheckPlacement(cp.CurrentPos, build.Size))
                {
                    spriteBatch.Draw(ContentHandler.DrawnTexture(build.block.texture), (cp.CurrentPos * Tile.Zoom * 16) - (cp.camera.Position * Tile.Zoom * 16), null, Color.Green, 0, new Vector2(0), Tile.Zoom, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(ContentHandler.DrawnTexture(build.block.texture), (cp.CurrentPos * Tile.Zoom * 16) - (cp.camera.Position * Tile.Zoom * 16), null, Color.Red, 0, new Vector2(0), Tile.Zoom, SpriteEffects.None, 0);
                }
            }
            spriteBatch.Draw(OverlayTexture, new Vector2(), Color.White);
            DrawMap();
            DrawText();
            foreach (CommandButton button in components.Where(l => l is CommandButton))//For all queueable objects if you can afford it, it shows up normally if not it shows up red
            {
                if(button.command is BuildSelectCommand)
                {
                    if (!cp.cc.CheckCost(((BuildSelectCommand)button.command).build))
                    {
                        button.color = Color.Red;
                    } else
                    {
                        button.color = Color.White;
                    }
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        internal Command ClickCheck()
        {
            if(((MouseKeyboard)input).LeftClick())
            {
                if(components.Find(x=>x.bounds.Contains(input.inputPos)) != null)
                {
                    IComponent component = components.Find(x => x.bounds.Contains(input.inputPos));
                    if (component is CommandButton)//If the user can afford to train/build things it returns the selected unit, if not it returns null
                    {
                        if (((CommandButton)component).command is BuildSelectCommand)
                        {
                            BuildSelectCommand command = (BuildSelectCommand)((CommandButton)component).command;
                            if (cp.cc.CheckCost(command.build))
                            {
                                return (Command)((CommandButton)component).command;
                            }
                        } else
                        {
                            return (Command)((CommandButton)component).command;
                        }
                    }
                }
            }
            return null;
        }

        private void DrawSelectionDetails()
        {
            
        }

        private void DrawText()
        {
            List<string> resources = cp.cc.Resources();
            foreach (string resource in resources)
            {
                spriteBatch.DrawString(ContentHandler.Font, resource, new Vector2((resources.FindIndex(l => l == resource)*115)+13, 5), Color.White);
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
            base.LoadContent();
        }

        private void DrawViewPortRepresentation()
        {
            Color[] bounds = new Color[cp.camera.Size.X*cp.camera.Size.Y];
            for(int y = 0; y < cp.camera.Size.Y;y++)
            {
                for (int x = 0; x < cp.camera.Size.X; x++)
                {
                    if(x == 0 || y== 0 || x == cp.camera.Size.X-1 || y == cp.camera.Size.Y - 1)
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
            float scale = 0.5f;
            Vector2 Position = new Vector2(0,GraphicsDevice.Viewport.Height)-new Vector2(-8, world.GetSize().Y * scale);
            spriteBatch.Draw(world.getMap(), Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            spriteBatch.Draw(CameraView, (cp.camera.Position*scale) + Position, null, Color.White, 0, new Vector2(0, 0), (scale/Tile.Zoom), SpriteEffects.None, 0);
        }
    }
}
