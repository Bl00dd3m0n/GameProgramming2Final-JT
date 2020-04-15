using _0x46696E616C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldManager.MapData
{
    public interface IBuildingObserver
    {
        void Deposit(Wallet wallet);
    }
}
