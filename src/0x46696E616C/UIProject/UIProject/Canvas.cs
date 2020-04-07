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
        public List<Component> Components { get { return components.ToList(); } }
        List<Component> components;
        Texture2D componentTexture;
        SpriteFont font;
        public Canvas(Game game) : base(game)
        {
            components = new List<Component>();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Arial");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (((Button)components[0]).Clicked)
            {
                ((Button)components[0]).Clicked = false;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (IComponent component in components)
            {
                component.Draw(ref componentTexture, GraphicsDevice);
                if (componentTexture != null)
                {
                    if (((Button)component).Clicked)
                    {
                        spriteBatch.Draw(componentTexture, component.Position, Color.LightGray);
                    }
                    else
                    {
                        spriteBatch.Draw(componentTexture, component.Position, Color.White);
                    }

                    if (component.Text != string.Empty)
                    {
                        Vector2 position = component.Position + (component.Size.ToVector2() / 2) - (font.MeasureString(component.Text) / 2);
                        spriteBatch.DrawString(font, component.Text, position, Color.Black);
                    }
                    spriteBatch.DrawString(font, Button.value.ToString(), new Vector2(0), Color.Black);
                }

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void AddComponent(Component component)
        {
            components.Add(component);
        }
        public void RemoveComponent(int value)
        {
            components.RemoveAt(value);
        }

        public void RemoveAllComponents()
        {
            components.Clear();
        }

    }
}
