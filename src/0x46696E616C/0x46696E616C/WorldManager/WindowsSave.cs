using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using NationBuilder.WorldHandlerLibrary;

namespace WorldManager
{
    public class WindowsSave : Save
    {

        public override bool SaveWorld(List<Region> region, string worldName)
        {
            if (!locked)
            {
                locked = true;
                FileStream fs = null;
                //if (File.Exists($"{worldName}.wrld"))
                    //fs = new FileStream($"{worldName}.wrld", FileMode.Append);
                //else
                    fs = new FileStream($"{worldName}.wrld", FileMode.Create);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, region);
                }
                catch (SerializationException e)
                {
                    fs.Close();
                    locked = false;
                    return false;
                }
                finally
                {
                    fs.Close();
                    locked = false;
                }
                return true;
            } else {
                return false;
            }
        }
        /// <summary>
        /// Not using at the moment
        /// </summary>
        /// <param name="worldName"></param>
        /// <returns></returns>
        public override List<Region> LoadWorld(string worldName)
        {
            if (!locked) {
                List<Region> region = new List<Region>();

                FileStream fs = new FileStream($"{worldName}.wrld", FileMode.Open);
                locked = true;
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    fs.Position = 0;
                    region = (List<Region>)bf.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    fs.Close();
                    locked = false;
                    return null;
                }
                finally
                {
                    fs.Close();
                    locked = false;
                }
                return region;
            } else {
                return null;
            }
        }
    }
}
