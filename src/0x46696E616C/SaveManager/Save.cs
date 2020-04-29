using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveManager
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

        public virtual bool SaveToFile(string Path, string StringToSave)
        {
            try
            {
                return true;
            } catch(Exception)
            {
                return false;
            }
        }
        public virtual string LoadFromFile(string Path)
        {
            try
            {
                return "";
            } catch(Exception)
            {
                return null;
            }
        }

    }
}
