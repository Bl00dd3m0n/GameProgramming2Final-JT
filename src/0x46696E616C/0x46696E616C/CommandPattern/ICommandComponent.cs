using _0x46696E616C.MobHandler.Units;
using Microsoft.Xna.Framework;

namespace _0x46696E616C.CommandPattern
{
    internal interface ICommandComponent
    {
        void Move(Vector2 Position);
        void Attack(IEntity target);
        void Garrison(IEntity Target);
    }
}