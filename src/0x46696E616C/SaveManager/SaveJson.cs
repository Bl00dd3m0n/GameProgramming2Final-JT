using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveManager
{
    public class SaveJson<T>
    {
        public void SaveToJson(T value, string path)
        {
            
            string json = JsonConvert.SerializeObject(value);
            using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write)))//TODO Abstract saving to a singleton
            {
                writer.Write(json);
            }
        }

        public T LoadFromJson(string path)
        {
            string json = "";
            using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read)))// TODO File read to a singleton
            {
                json += reader.ReadLine();
            }
            T thisObject = JsonConvert.DeserializeObject<T>(json);
            return thisObject;
        }
    }
}
