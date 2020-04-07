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
        public StyleSheet(string FilePath)
        {
            page = new List<Component>();
            this.FilePath = FilePath;
            if(!File.Exists(FilePath)) //TODO Implement Non-Windows based system
            {
                XmlSerializer xml = new XmlSerializer(page.GetType());
                using(StreamWriter wr = new StreamWriter(File.Create(FilePath)))
                {
                    xml.Serialize(wr, page);
                }
            }
        }

        public bool SaveStyleSheet(List<Component> components)
        {
            return SaveStyleSheet(components, FilePath);
        }

        public bool SaveStyleSheet(List<Component> components, string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load
            try
            {
                this.page = ((List<Component>)components);
                if (File.Exists(FilePath)) //TODO Implement Non-Windows based system
                {
                    XmlSerializer xml = new XmlSerializer(page.GetType());
                    using (StreamWriter wr = new StreamWriter(File.Create(FilePath)))
                    {
                        xml.Serialize(wr, page);
                    }
                }
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
