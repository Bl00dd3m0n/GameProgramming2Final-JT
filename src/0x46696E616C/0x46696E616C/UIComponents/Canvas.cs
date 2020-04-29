using _0x46696E616C.Util.Input;
using MainMenu.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NationBuilder.DataHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UIProject
{
    public class Canvas : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        public List<IComponent> Components { get { return components; } }
        protected List<IComponent> components;
        protected SpriteFont font;
        public Canvas(Game game) : base(game)
        {
            components = new List<IComponent>();
        }

        public void LoadCanvas(List<IComponent> components)
        {
            this.components = components;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            if (ContentHandler.Font == null)
            {
                font = Game.Content.Load<SpriteFont>("Ariel");
            } else
            {
                font = ContentHandler.Font;
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public virtual Button CheckClick(Point point, InputDefinitions input)
        {
            foreach (IComponent component in components)
            {
                if (component.bounds.Contains(point))
                {
                    if (component is PageButton)
                    {
                        ((PageButton)component).Click(Game, this);
                        return (Button)component;
                    }
                    else if (component is InputButton)
                    {
                        ((InputButton)component).Click(input, Game);
                        return (Button)component;
                    }
                    else if (component is Button)
                    {
                        ((Button)component).Click(Game);
                        return (Button)component;
                    }
                }
            }
            return null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (((Component)components[i]).drawComponent)
                {
                    if (components[i] is Button && ((Button)components[i]).Clicked)
                    {
                        spriteBatch.Draw(components[i].picture, components[i].Position, Color.LightGray);
                    }
                    else if (components[i].picture != null)
                    {
                        spriteBatch.Draw(components[i].picture, components[i].Position, null, components[i].Color, 0, new Vector2(0), components[i].Scale, SpriteEffects.None, 0);
                    }
                    if (components[i].Text != null && components[i].Text != string.Empty)
                    {
                        Vector2 position = components[i].Position + (components[i].Size.ToVector2() / 2) - (font.MeasureString(components[i].Text) / 2);
                        spriteBatch.DrawString(font, components[i].Text, position, Color.Black);
                    }
                }
            }
        }
        public virtual void AddComponent(IComponent component)
        {
            components.Add(component);
        }
        public virtual void RemoveComponent(int value)
        {
            components.RemoveAt(value);
        }

        public void RemoveAllComponents()
        {
            components.Clear();
        }

        public void RemoveAllComponents(Type type)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == type)
                {
                    components.Remove(components[i]);
                    i--;
                }
            }
        }
    }
}
