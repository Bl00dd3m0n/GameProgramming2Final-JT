using _0x46696E616C.UIComponents;
using _0x46696E616C.UIComponents.Stats;
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
            if (ContentHandler.Font == null)
            {
                font = Game.Content.Load<SpriteFont>("Ariel");
            }
            else
            {
                font = ContentHandler.Font;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IComponent component in components)
            {
                if (component is UpdatePanel)
                {
                    ((UpdatePanel)component).Update(gameTime);
                }
                if (component is Button && ((Button)component).clickedTimer > 0)
                {
                    ((Button)component).clickedTimer = ((((Button)component).clickedTimer * 1000) - gameTime.ElapsedGameTime.Milliseconds) / 1000;
                    if (((Button)component).clickedTimer <= 0)
                    {
                        ((Button)component).Clicked = false;
                    }
                }
            }
            base.Update(gameTime);
        }

        public virtual Button CheckClick(Point point, InputDefinitions input, StyleSheet[] sheets = null)
        {
            foreach (IComponent component in components)
            {
                if (component.bounds.Contains(point))
                {
                    if (component is Button)
                    {

                    }
                    if (component is PageButton)
                    {
                        ((PageButton)component).Click(Game, sheets[((PageButton)component).PageOrder], this);
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is Component)
                {
                    Component component = (Component)components[i];
                    if (components[i] is StatComponent)
                    {
                        component = ((StatComponent)component).component;
                    }
                    if (component.drawComponent)
                    {
                        if (component is Button && ((Button)component).Clicked)
                        {
                            spriteBatch.Draw(component.picture, component.Position, Color.LightGray);
                        }
                        else if (component.picture != null)
                        {
                            spriteBatch.Draw(component.picture, component.Position, null, component.Color, 0, new Vector2(0), component.Scale, SpriteEffects.None, 0);
                        }
                        if (component.Text != null && component.Text != string.Empty)
                        {
                            Vector2 position = component.Position;
                            if (!(component is Label))
                            {
                                position = component.Position + (component.Size.ToVector2() / 2) - (font.MeasureString(component.Text) / 2);
                            }
                            spriteBatch.DrawString(font, component.Text, position, Color.Black);
                        }
                    }
                }
                else if (components[i] is Panel)
                {
                    ((Panel)components[i]).Draw(spriteBatch);
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

        public virtual void RemoveComponent(IComponent component)
        {
            components.Remove(component);
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
