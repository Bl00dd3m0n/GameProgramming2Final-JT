using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Resources;
using NationBuilder.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using MyVector2 = NationBuilder.TileHandlerLibrary.Vector2;
using _0x46696E616C.WorldManager.Resources;

namespace _0x46696E616C.Buildings
{
    class Center : Building
    {
        public Center(TextureValue texture, MyVector2 position) : base(texture, position)
        {
            Cost = new Wallet<IResource>();
            name = "Center";
            Position = position.ToMonoGameVector2();
            Size = new Vector2(0, 0);
            TotalHealth = 0;
            CurrentHealth = 500;
        }
    }
}
