using Microsoft.Xna.Framework.Graphics;
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
        List<Component> page;
        Type[] componentTypes;
        public StyleSheet(string FilePath)
        {
            this.FilePath = FilePath;
        }

        public List<IComponent> GetStyleSheet(GraphicsDevice gd)
        {
            List<Component> components = new List<Component>();
            componentTypes = new Type[] { typeof(Button) };
            XmlSerializer xml = new XmlSerializer(components.GetType(), componentTypes);
            using (StreamReader r = new StreamReader(File.Open(FilePath, FileMode.Open, FileAccess.Read)))
            {
                components = (List<Component>)xml.Deserialize(r);
            }
            foreach(Component component in components)
            {
                component.Draw(gd);
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
