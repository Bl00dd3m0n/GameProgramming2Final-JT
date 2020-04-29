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
            Save.save.SaveToFile(path, json);
        }

        public T LoadFromJson(string path)
        {
            string json = "";
            json = Save.save.LoadFromFile(path);
            T thisObject = JsonConvert.DeserializeObject<T>(json);
            return thisObject;
        }
    }
}
