using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SaveManager
{
    public class WindowsSave : Save
    {

        public override bool SaveToFile(string Path, string StringToSave)
        {
            if (!locked)
            {
                locked = true;
                using (StreamWriter fs = new StreamWriter(new FileStream($"{Path}", FileMode.Create)))
                {
                    fs.Write(StringToSave);
                }
                locked = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Not using at the moment
        /// </summary>
        /// <param name="worldName"></param>
        /// <returns></returns>
        public override string LoadFromFile(string Path)
        {
            if (!locked)
            {
                string LoadedText = "";
                locked = true;
                using (StreamReader fs = new StreamReader(new FileStream($"{Path}", FileMode.Open)))
                {
                    LoadedText = fs.ReadToEnd();
                }
                locked = false;
                return LoadedText;
            }
            else
            {
                return "";
            }
        }
    }
}
