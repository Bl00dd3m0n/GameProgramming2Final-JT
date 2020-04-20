using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.Util
{
    public class ArtificialEnum
    {
        static List<string> myEnum;
        public ArtificialEnum(List<string> enums)
        {
            myEnum = enums;
        }

        public static implicit operator ArtificialEnum(string v)
        {
            return myEnum.Find(l=>l == v);
        }
    }
}
