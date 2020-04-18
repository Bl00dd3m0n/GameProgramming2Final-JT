using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveManager
{
    class SaveJson<T>
    {
        public void SaveToJson(T value, string path)
        {
            string json = JsonConvert.SerializeObject(value);
            using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create, FileAccess.Write)))
            {
                
                writer.WriteLine(json);
            }
        }
        public T LoadFromJson(string path, Type[] types)
        {
            string json = "";
            using (StreamReader reader = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read)))
            {
                json += reader.ReadLine();
            }
            return (T)JsonConvert.DeserializeObject(json);
        }
    }
}
