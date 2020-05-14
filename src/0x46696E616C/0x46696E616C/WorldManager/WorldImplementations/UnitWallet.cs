using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.ConcreteImplementations;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;

namespace _0x46696E616C
{
    public class UnitWallet : Wallet
    {
        int ResourceCap
        {
            get;
            set;
        }
        IUnit unit;
        public UnitWallet(IUnit unit) : base()
        {
            if (unit is BasicUnit && ((BasicUnit)unit).teamStats != null)
            {
                if (unit.stats[typeof(InventorySpace)] != null) this.ResourceCap = (int)unit.stats[typeof(InventorySpace)].Value + (int)((BasicUnit)unit).teamStats[typeof(InventorySpace)].Value;
                else this.ResourceCap = 10;
            }
            else
            {
                if (unit.stats[typeof(InventorySpace)] != null) this.ResourceCap = (int)unit.stats[typeof(InventorySpace)].Value;
                else this.ResourceCap = 10;
            }
            this.unit = unit;
        }

        public override bool Deposit(IResource resource, float amount)
        {
            // Dynamic resizing for the units inventory space
            if (unit is BasicUnit) { if (unit.stats[typeof(InventorySpace)] != null) this.ResourceCap = (int)unit.stats[typeof(InventorySpace)].Value + (int)((BasicUnit)unit).teamStats[typeof(InventorySpace)].Value; } 
            else { if (unit.stats[typeof(InventorySpace)] != null) this.ResourceCap = (int)unit.stats[typeof(InventorySpace)].Value; }
            if (amount + Count(resource) <= this.ResourceCap)
            {
                base.Deposit(resource, amount);
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool Deposit(Wallet wallet)
        {
            // Dynamic resizing for the units inventory space
            if (unit is BasicUnit) {
                if (((BasicUnit)unit).teamStats[typeof(InventorySpace)] != null)
                    this.ResourceCap = (int)unit.stats[typeof(InventorySpace)].Value + (int)((BasicUnit)unit).teamStats[typeof(InventorySpace)].Value;
            }
            else {
                if (unit.stats[typeof(InventorySpace)] != null)
                    this.ResourceCap = (int)unit.stats[typeof(InventorySpace)].Value;
            }
            if (CheckWalletSize(wallet))
            {
                base.Deposit(wallet);
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Checks each resource count in the wallet
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns>
        /// false - if above the cap
        /// true - if under the cap
        /// </returns>
        private bool CheckWalletSize(Wallet wallet)
        {
            foreach (IResource resource in wallet.Resources)
            {
                if (wallet.Count(resource) + Count(resource) > ResourceCap)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
