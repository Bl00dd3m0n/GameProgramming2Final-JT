using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.Util.Collision
{
    public interface ICollider
    {
        Vector2 Position { get; }
        Vector2 Size { get; }
        void Collision(ICollider collider);
    }
}
