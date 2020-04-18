using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SaveManager
{
    public class SaveXml<T>
    {
        public void SaveToXml(T value, string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write)))
            {
                ser.Serialize(writer, value);
            }
        }
        public T LoadFromXml(string path, Type[] types)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T), types);
            using (StreamReader writer = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read)))
            {
                return (T)ser.Deserialize(writer);
            }
        }
    }
}
