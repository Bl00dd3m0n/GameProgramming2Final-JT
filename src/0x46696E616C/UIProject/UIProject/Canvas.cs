using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UIProject
{
    public class Canvas : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        public IComponent[] Components { get { return components; } }
        IComponent[] components;
        SpriteFont font;
        public Canvas(Game game) : base(game)
        {
            components = new IComponent[0];
        }

        public void LoadCanvas(IComponent[] components)
        {
            this.components = components;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Ariel");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            for (int i = 0; i < components.Length; i++)
            {
                if (((Button)components[i]).Clicked)
                {
                    spriteBatch.Draw(components[i].picture, components[i].Position, Color.LightGray);
                }
                else
                {
                    spriteBatch.Draw(components[i].picture, components[i].Position, components[i].color);
                }
                if (components[i].Text != string.Empty)
                {
                    Vector2 position = components[i].Position + (components[i].Size.ToVector2() / 2) - (font.MeasureString(components[i].Text) / 2);
                    spriteBatch.DrawString(font, components[i].Text, position, Color.Black);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void AddComponent(Component component)
        {
            Array.Resize(ref components, components.Length + 1);
            components[components.Length] = component;
        }
        public void RemoveComponent(int value)
        {
            Array.Resize(ref components, components.Length - 1);
        }

        public void RemoveAllComponents()
        {
            Array.Clear(components, 0, components.Length);
        }

    }
}
