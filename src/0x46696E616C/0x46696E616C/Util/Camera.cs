﻿using _0x46696E616C.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _0x46696E616C.MobHandler;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;
using WorldManager.TileHandlerLibrary;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.Util.Input;

namespace Util
{
    public class Camera : DrawableGameComponent
    {
        float timer;
        WorldHandler world;
        Rectangle ViewPort;
        Rectangle bounds;
        float MoveSpeed;
        public Point Size { get { return ViewPort.Size; } }
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }
        Vector2 position;
        SpriteBatch sb;
        InputDefinitions input;
        Vector2 Dir;

        public Camera(Game game, InputDefinitions input, WorldHandler worldHandler) : this(game, input, worldHandler, new Vector2(0, 0))
        {

        }

        public Camera(Game game, InputDefinitions input, WorldHandler worldHandler, Vector2 startPoint) : base(game)
        {
            Tile.Zoom = 3;
            MoveSpeed = 6.25f;
            this.position = startPoint;
            this.ViewPort = new Rectangle(position.ToPoint(), new Point(30, 16));
            this.input = input;
            Dir = new Vector2(0, 0);
            this.world = worldHandler;
            bounds = new Rectangle(new Vector2(0, 0).ToPoint(), (world.GetSize()).ToPoint());
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            sb = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            timer = gameTime.ElapsedGameTime.Milliseconds;
            if (input.Updated())
            {
                AdjustCamera();
                CameraPosition();
            }
            base.Update(gameTime);
        }

        private void AdjustCamera()
        {
            if (input.CheckInput(Controls.ZoomIn) && Tile.Zoom < 3f)
            {
                Tile.Zoom += 0.3f;
            }
            else if (input.CheckInput(Controls.ZoomOut) && Tile.Zoom > 1f)
            {
                Tile.Zoom -= 0.3f;
            }
        }
        /// <summary>
        /// Movement setup for the camera
        /// </summary>
        private void CameraPosition()
        {
            Dir.X = 0;
            Dir.Y = 0;
            if (input.CheckInput(Controls.Up) && bounds.Top < ViewPort.Top + (2 * (4 - Tile.Zoom)))
                Dir.Y = -1;
            if (input.CheckInput(Controls.Down) && bounds.Bottom > ViewPort.Bottom - (2 / ((4 - Tile.Zoom) / 4)))
                Dir.Y = 1;
            if (input.CheckInput(Controls.Left) && bounds.Left < ViewPort.Left + (2 * (4 - (Tile.Zoom))))
                Dir.X = -1;
            if (input.CheckInput(Controls.Right) && bounds.Right > ViewPort.Right - (2/((4-Tile.Zoom)/8)))
                Dir.X = 1;
            position += Dir * MoveSpeed * timer / 100;
            position = position.ToPoint().ToVector2();//Truncates the position to interger values
            ViewPort.Location = position.ToPoint();
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            //Layer tiles by type
            for (int i = 0; i < 3; i++)
            {
                int OverX = 0;
                int OverY = 0;
                float scale = 3;
                if (ViewPort.Top < 0) OverY = -ViewPort.Top;
                if (ViewPort.Left < 0) OverX = -ViewPort.Left;
                //draw the viewport of the map using the scale
                for (int y = ViewPort.Top; y < (ViewPort.Bottom * (4 - Tile.Zoom)) + (OverY * (4 - Tile.Zoom)); y++)
                {
                    for (int x = ViewPort.Left; x < (ViewPort.Right * (4 - Tile.Zoom)) + (OverX * (4 - Tile.Zoom)); x++)
                    {
                        DrawScreen(x, y, i);
                    }
                }
            }
            sb.End();
            sb.Begin();
            //DrawMap();//Overlay shouldn't be affected by the camera
            sb.End();
            base.Draw(gameTime);
        }

        private void DrawScreen(int x, int y, int i)
        {
            if (x >= 0 && x < bounds.Width && y >= 0 && y < bounds.Height)
            {
                //Background tiles are drawn first
                if (i == 0)
                {
                    Tile backtile = world.GetBackgroundTile(new Vector2(x, y));
                    sb.Draw(ContentHandler.DrawnTexture(backtile.block.texture), (backtile.Position * Tile.Zoom * 16) - (position * Tile.Zoom * 16), null, Color.White, 0, Vector2.Zero, Tile.Zoom, SpriteEffects.None, 0);
                }
                //Units are drawn second
                else if (i == 1)
                {
                    ModifiableTile tile = (ModifiableTile)world.GetUnit(new Vector2(x, y));
                    if (tile != null && tile.block.texture != TextureValue.None)
                    {

                        Texture2D texture = ContentHandler.DrawnTexture(tile.block.texture);
                        ((BasicUnit)tile).UpdatePosition(Game.GraphicsDevice, tile.Position);
                        DrawHealth(tile);
                        sb.Draw(ContentHandler.DrawnTexture(tile.block.texture), (tile.Position * Tile.Zoom * 16) - (position * Tile.Zoom * 16), null, Color.White, 0, Vector2.Zero, Tile.Zoom, SpriteEffects.None, 0);
                    }
                }
                //Draw buildings third
                else
                {
                    ModifiableTile decorTile = world.GetTile(new Vector2(x, y));
                    if (decorTile != null && decorTile.block.texture != TextureValue.None)
                    {
                        Texture2D texture = ContentHandler.DrawnTexture(decorTile.block.texture);
                        decorTile.UpdatePosition(Game.GraphicsDevice, decorTile.Position);
                        sb.Draw(ContentHandler.DrawnTexture(decorTile.block.texture), (decorTile.Position * Tile.Zoom * 16) - (position * Tile.Zoom * 16), null, Color.White, 0, Vector2.Zero, Tile.Zoom, SpriteEffects.None, 0);
                        DrawHealth(decorTile);
                    }
                }
            }
        }
        private void DrawHealth(ModifiableTile tileWithHealth)
        {
            //If a tile has a health bar draw it.
            if (tileWithHealth.healthBar != null)
            {
                if (tileWithHealth.healthBar.Health != null)
                {
                    sb.Draw(tileWithHealth.healthBar.Health, (tileWithHealth.healthBar.Position * Tile.Zoom * 16) - (position * Tile.Zoom * 16), null, Color.White, 0, Vector2.Zero, Tile.Zoom, SpriteEffects.None, 0);
                }
            }
        }
    }
}
