using _0x46696E616C.UIComponents;
using MainMenu.Component;
using Microsoft.Xna.Framework.Graphics;
using SaveManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace UIProject
{
    public class StyleSheet
    {
        private string FilePath;
        Type[] componentTypes;
        SaveXml<List<Component>> save;
        List<IComponent> ssComponents;
        List<IComponent> SSComponents { get { return ssComponents.ToList(); } }
        public Type[] ComponentTypes = new Type[] { typeof(Button), typeof(StartButton), typeof(ExitButton), typeof(Label), typeof(PageButton), typeof(InputButton) };
        public StyleSheet()
        {
            save = new SaveXml<List<Component>>();
        }

        public List<IComponent> GetStyleSheet(GraphicsDevice gd, string path, Type[] types)
        {
            if (ssComponents == null || ssComponents.Count <= 0)
            {
                this.FilePath = path;
                List<Component> components = new List<Component>();
                if (File.Exists(path))
                {
                    components = save.LoadFromXml(FilePath, types);
                    foreach (Component component in components)
                    {
                        component.Draw(gd);
                        component.Scale = 1;
                    }
                }
                ssComponents = components.Cast<IComponent>().ToList();
                return components.Cast<IComponent>().ToList();
            }
            else
            {
                return ssComponents.ToList();//If I reset my components in the canvas this gets reset I think....should solve the issue
            }
        }

        public void SaveStyleSheet(List<IComponent> list, string Path)
        {
            this.FilePath = Path;
            List<Component> page = list.Cast<Component>().ToList();
            List<Type> types = new List<Type>();
            foreach (IComponent component in list)
            {
                types.Add(component.GetType());
            }
            componentTypes = types.Distinct().ToArray();
            save.SaveToXml(page, Path, componentTypes);
        }
    }
}
