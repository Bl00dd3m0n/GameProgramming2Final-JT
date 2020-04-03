using System;
using NationBuilder.TileHandlerLibrary;
using WorldManager.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using MyVector2 = NationBuilder.TileHandlerLibrary.Vector2;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.WorldManager.Resources;

namespace _0x46696E616C.Buildings
{
    public abstract class Building : ModifiableTile, IEntity
    {
        /// <summary>
        /// This will probably get turned into a wallet class at some point however the name Cost will stay the same
        /// </summary>
        public Wallet<IResource> Cost { get; protected set; }

        public Building(TextureValue texture, MyVector2 position) : base(texture, position)
        {
            Cost = new Wallet<IResource>();
            name = "Building";
            Position = new Vector2(0, 0);
            Size = new Vector2(0, 0);
            TotalHealth = 0;
            CurrentHealth = 0;
        }

        public override void Damage(int amount)
        {
            CurrentHealth -= amount;
        }

        public void Destroy()
        {
            
        }

        public override void Die()
        {
            throw new NotImplementedException();
        }
    }
}
