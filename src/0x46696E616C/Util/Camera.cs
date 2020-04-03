using _0x46696E616C.InputHandler;
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
using MonoVector2 = Microsoft.Xna.Framework.Vector2;
namespace Util
{
    public class Camera : DrawableGameComponent
    {
        float timer;
        WorldHandler world;
        float Zoom
        {
            get
            {
                return zoom;
            }
        }
        Microsoft.Xna.Framework.Vector2 Position
        {
            get
            {
                return position;
            }
        }
        Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }
        Rectangle ViewPort;
        Matrix Transform
        {
            get
            {
                return transform;
            }
        }
        Rectangle bounds;
        float MoveSpeed;
        Microsoft.Xna.Framework.Vector2 position;
        float zoom;
        Matrix transform;
        SpriteBatch sb;
        MouseKeyboard input;
        MonoVector2 Dir;
        public Camera(Game game) : base(game)
        {
            zoom = 20f;
            MoveSpeed = 20f;
            this.ViewPort = new Rectangle(position.ToPoint(),new Point(game.GraphicsDevice.Viewport.Width,game.GraphicsDevice.Viewport.Height));
            input = new MouseKeyboard(game);
            Dir = new MonoVector2(0, 0);
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            world = new WorldHandler(Game, "HelloWorld");
            sb = new SpriteBatch(Game.GraphicsDevice);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

            //zoom += timer / 100000;
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            input.Update(gameTime);
            Dir.X = 0;
            Dir.Y = 0;
            if (input.CheckKeyDown(Keys.Q))
                zoom *= 2f;
            if (input.CheckKeyDown(Keys.E))
                zoom /= 2f;
            if (input.CheckKeyDown(Keys.W))
                Dir.Y = 1;
            if (input.CheckKeyDown(Keys.S))
                Dir.Y = -1;
            if (input.CheckKeyDown(Keys.A))
                Dir.X = 1;
            if (input.CheckKeyDown(Keys.D))
                Dir.X = -1;
            position += Dir * MoveSpeed * timer / 100;
            timer = gameTime.ElapsedGameTime.Milliseconds;
            sb.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateTranslation(position.X, position.Y, 0) * Matrix.CreateScale(zoom) * timer/1000);
            for (int y = ViewPort.Top; y < ViewPort.Bottom; y++)
                for (int x = ViewPort.Left; x < ViewPort.Right; x++)
                {
                    Tile tile = world.GetTile(new NationBuilder.TileHandlerLibrary.Vector2(x, y));
                    //if (tile.block.texture == TextureValue.Water || tile.block.texture == TextureValue.Tree)
                    //{
                        sb.Draw(ContentHandler.DrawnTexture(tile.block.texture), tile.position.ToMonoGameVector2()*16, Color.White);
                    //}
                }
            sb.End();
            base.Draw(gameTime);
        }
    }
}
