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
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(ms, Encoding.UTF32))
                {
                    ser.Serialize(writer, value);
                    byte[] utf8EncodedXml = ms.ToArray();
                    Save.save.SaveToFile(path, System.Text.Encoding.UTF8.GetString(utf8EncodedXml));
                }
            }
        }

        public void SaveToXml(T value, string path, Type[] types)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T), types);
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(ms, Encoding.UTF32))
                {
                    ser.Serialize(writer, value);
                    byte[] utf8EncodedXml = ms.ToArray();
                    Save.save.SaveToFile(path, Encoding.UTF8.GetString(utf8EncodedXml));
                }
            }
        }

        public T LoadFromXml(string path, Type[] types)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T), types);
            string deserialize = Save.save.LoadFromFile(path);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(deserialize)))
            {
                T test = (T)ser.Deserialize(ms);
                return test;
            }
        }
    }
}
