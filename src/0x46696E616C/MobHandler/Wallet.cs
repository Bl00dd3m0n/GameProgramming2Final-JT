using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C
{
    public class Wallet<t>
    {
        private List<t> resources;
        internal List<t> Resources { get { return resources.ToList(); } }

        public Wallet() : this(new List<t>())
        {
            
        }

        public Wallet(List<t> wallet)
        {
            this.resources = wallet;
        }

        /// <summary>
        /// Gets the amount of a resource type held by the player
        /// </summary>
        /// <param name="resource">ResourceType</param>
        /// <returns></returns>
        public int Count(t resourceType)
        {
            return resources.Where(l => l.GetType() == resourceType.GetType()).Count();
        }

        public void Deposit(t resource, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Add(resource);
            }
        }

        private void Add(t item)
        {
            resources.Add(item);
        }

        public void Deposit(Wallet<t> wallet)
        {
            this.resources.AddRange(wallet.resources);
        }

        public void Clear()
        {
            resources.Clear();
        }

        public bool Contains(t item)
        {
            foreach(t resource in resources)
            {
                if (resource.GetType() == item.GetType())
                {
                    return true;
                }
            }
            return false;
        }

        private int IndexOf(t item)
        {
            foreach (t resource in resources)
            {
                if (resource.GetType() == item.GetType())
                {
                    return resources.IndexOf(resource);
                }
            }
            return -1;
        }

        public Wallet<t> Withdraw(Wallet<t> cost)
        {
            Wallet<t> WithdrawWallet = new Wallet<t>();
            foreach (t resource in cost.resources)
            {
                if(cost.Count(resource) > Count(resource))
                {
                    return null;
                }
                WithdrawWallet.Add(resource);
            }
            return WithdrawWallet;
        }
        public Wallet<t> Withdraw(t resource, int amount)
        {
            Wallet<t> WithdrawWallet = new Wallet<t>();
            if (Count(resource) >= amount) {
                for (int i = 0; i < amount; i++)
                {
                    WithdrawWallet.resources.Add(Remove(resource));
                }
                return WithdrawWallet;
            } else
            {
                return null;
            }
        }

        private t Remove(t item)
        {
            resources.RemoveAt(IndexOf(item));
            return item;
        }

        public static implicit operator Wallet<t>(List<t> v)
        {
            return new Wallet<t>(v);
        }
    }
}
