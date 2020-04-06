using NationBuilder.WorldHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldManager
{
    public class Save
    {
        protected static Save _save;
        public bool locked { get; protected set; }
        public static Save save
        {
            get
            {

                if (_save == null)
                {
                    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    {
                        _save = new WindowsSave();
                    }else
                    {
                        _save = new Save();
                    }
                }
                return _save;
            }
        }

        public Save()
        {
            
        }

        public virtual bool SaveWorld(List<Region> region, string worldName)
        {
            try
            {
                return true;
            } catch(Exception ex)
            {
                return false;
            }
        }
        public virtual List<Region> LoadWorld(string worldName)
        {
            try
            {
                return new List<Region>();
            } catch(Exception ex)
            {
                return null;
            }
        }

    }
}
