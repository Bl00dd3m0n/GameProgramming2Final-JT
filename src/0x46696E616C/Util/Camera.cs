using _0x46696E616C.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager;
using WorldManager.TileHandlerLibrary;

namespace Util
{
    public class Camera : DrawableGameComponent
    {
        float timer;
        WorldHandler world;
        Rectangle ViewPort;
        Rectangle bounds;
        float MoveSpeed;
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }
        Vector2 position;
        float zoom;
        SpriteBatch sb;
        MouseKeyboard input;
        Vector2 Dir;
        public Camera(Game game, InputHandler input, WorldHandler worldHandler) : base(game)
        {
            zoom = 1.75f;
            MoveSpeed = 19f;
            this.ViewPort = new Rectangle(position.ToPoint(), new Point(30, 16));
            this.input = (MouseKeyboard)input;
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
            if (input.Updated)
            {
                AdjustCamera();
                CameraPosition();
            }
            //zoom += timer / 100000;
            base.Update(gameTime);
        }

        private void AdjustCamera()
        {
            if (input.scrollVal > input.prevScrollVal && zoom < 3f)
            {
                zoom += 0.05f;
            }
            else if (input.scrollVal < input.prevScrollVal && zoom > 1.5f)
            {
                zoom -= 0.05f;
            }
            input.Scrolling();
        }

        private void CameraPosition()
        {
            Dir.X = 0;
            Dir.Y = 0;
            if (input.CheckKeyDown(Keys.W) && bounds.Top < ViewPort.Top)
                Dir.Y = -1;
            if (input.CheckKeyDown(Keys.S) && bounds.Bottom > ViewPort.Bottom+16)
                Dir.Y = 1;
            if (input.CheckKeyDown(Keys.A) && bounds.Left < ViewPort.Left)
                Dir.X = -1;
            if (input.CheckKeyDown(Keys.D) && bounds.Right > ViewPort.Right+16)
                Dir.X = 1;
            position += Dir * MoveSpeed * timer / 100;
            if(!bounds.Contains(position)) position -= Dir * MoveSpeed * timer / 100;
            position = position.ToPoint().ToVector2();//Truncates the position to interger values
            ViewPort.Location = position.ToPoint();
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix transform = Matrix.CreateScale(zoom);
            sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null, transform);
            for (int i = 0; i < 2; i++)
            {
                for (int y = ViewPort.Top; y < ViewPort.Bottom; y++)
                {
                    for (int x = ViewPort.Left; x < ViewPort.Right; x++)
                    {
                        if (i == 0)
                        {
                            Tile backtile = world.GetBackgroundTile(new Vector2(x, y));
                            sb.Draw(ContentHandler.DrawnTexture(backtile.block.texture), (backtile.position * 16) - (position * 16), Color.White);
                        }
                        else
                        {
                            ModifiableTile decorTile = world.GetTile(new Vector2(x, y));
                            if (decorTile != null && decorTile.block.texture != TextureValue.None)
                            {

                                Texture2D texture = ContentHandler.DrawnTexture(decorTile.block.texture);
                                decorTile.position = decorTile.position.ToPoint().ToVector2();
                                sb.Draw(texture, decorTile.position * 16 - (position.ToPoint().ToVector2() * 16), Color.White);
                            }
                        }
                    }
                }
            }
            sb.End();
            sb.Begin();
            //DrawMap();//Overlay shouldn't be affected by the camera
            sb.End();
            base.Draw(gameTime);
        }

        private void DrawMap()
        {
            for (int y = 0; y < bounds.Height; y++)
            {
                for (int x = 0; x < bounds.Width; x++)
                {
                    sb.Draw(world.getMap(), new Vector2(x, y) * .25f + new Vector2(0, bounds.Height * 14.5f * 0.05f), null, Color.White, 0, new Vector2(0, 0), 0.05f, SpriteEffects.None, 0);
                }
            }
        }
    }
}
