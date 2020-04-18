using _0x46696E616C;
using _0x46696E616C.ConcreteImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldManager.Buildings
{
    public interface ICollectionCenter<T>
    {
        void Collect(Wallet wallet);
    }
}
