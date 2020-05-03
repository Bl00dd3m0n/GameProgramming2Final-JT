using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Util.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UIProject;

namespace _0x46696E616C.UIComponents
{
    class Panel : Canvas, IComponent
    {


        public Vector2 Position { get; set; }

        public Point Size { get; set; }

        public Color Color => throw new NotSupportedException();

        public Rectangle bounds { get; }

        public string Text => throw new NotSupportedException();

        public Texture2D picture => throw new NotSupportedException();

        public float Scale => throw new NotSupportedException();

        public Panel(Game game, Rectangle bounds) : base(game)
        {
            this.bounds = bounds;
        }

        public override void AddComponent(IComponent component)
        {
            base.AddComponent(component);
        }

        public override Button CheckClick(Point point, InputDefinitions input)
        {
            return base.CheckClick(point, input);
        }

        public string Description()
        {
            throw new NotSupportedException();
        }

        public void Draw(GraphicsDevice gd)
        {
            throw new NotSupportedException();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
