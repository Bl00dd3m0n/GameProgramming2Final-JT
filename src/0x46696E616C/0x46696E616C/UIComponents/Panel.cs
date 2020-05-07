using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.GameCommands;
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

        private CommandProccesor cp;

        public Panel(Game game, Rectangle bounds, CommandProccesor cp) : base(game)
        {
            this.cp = cp;
            this.bounds = bounds;
        }

        public override void AddComponent(IComponent component)
        {
            base.AddComponent(component);
        }

        public override Button CheckClick(Point point, InputDefinitions input, StyleSheet[] sheets)
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

        public override void Draw(SpriteBatch sb)
        {
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
            base.Draw(sb);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
    }
}
