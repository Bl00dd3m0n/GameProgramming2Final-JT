using System;
using NationBuilder.TileHandlerLibrary;
using WorldManager.TileHandlerLibrary;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using MobHandler;
using _0x46696E616C.MobHandler;

namespace _0x46696E616C.Buildings
{
    public abstract class Building : ModifiableTile, IEntity
    {
        /// <summary>
        /// This will probably get turned into a wallet class at some point however the name Cost will stay the same
        /// </summary>
        public Wallet Cost { get; protected set; }

        public int energyCost { get; protected set; }

        public Building(Game game, TextureValue texture, Vector2 position) : base(game, texture, position)
        {
            Cost = new Wallet();
            name = "Building";
            Position = new Vector2(0, 0);
            Size = new Vector2(0, 0);
            TotalHealth = 0;
            CurrentHealth = 0;
            energyCost = 0;
            healthBar = new HealthBar(new Rectangle(this.position.ToPoint() - new Point(0, (int)(this.Size.Y * 16 + 1)), Size.ToPoint()));
        }

        public void Construct(float amount)
        {
            CurrentHealth += amount;
            if(CurrentHealth > TotalHealth) CurrentHealth = TotalHealth;
        }

        public override void Damage(float amount)
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
