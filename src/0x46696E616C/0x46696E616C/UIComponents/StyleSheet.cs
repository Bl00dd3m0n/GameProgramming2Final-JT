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
        public StyleSheet(string FilePath)
        {
            save = new SaveXml<List<Component>>();
            this.FilePath = FilePath;
        }

        public List<IComponent> GetStyleSheet(GraphicsDevice gd)
        {
            Type[] types = new Type[] { typeof(Button) };
            List<Component> components = save.LoadFromXml(FilePath, types);
            foreach(Component component in components)
            {
                component.Draw(gd);
                component.Scale = 1;
            }
            return components.Cast<IComponent>().ToList();
        }

        public void SaveStyleSheet(List<IComponent> list)
        {
            List<Component> page = list.Cast<Component>().ToList();
            componentTypes = new Type[] { typeof(Button) };
            XmlSerializer xml = new XmlSerializer(page.GetType(), componentTypes);
            using (StreamWriter wr = new StreamWriter(File.Create(FilePath)))
            {
                xml.Serialize(wr, page);
            }
        }
    }
}
