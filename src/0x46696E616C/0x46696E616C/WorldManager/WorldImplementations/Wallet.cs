using _0x46696E616C.Buildings;
using _0x46696E616C.ConcreteImplementations.Resources;
using _0x46696E616C.WorldManager.ConcreteImplementations.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.ConcreteImplementations
{
    public class Wallet
    {
        private List<IResource> resources;
        internal List<IResource> Resources { get { return resources.ToList(); } }

        public Wallet()
        {
            resources = new List<IResource>();
            resources.Add(new Wood());
            resources.Add(new Steel());
            resources.Add(new Money());
            resources.Add(new Likes());
            resources.Add(new Iron());
            resources.Add(new Energy());

        }

        public Wallet(Wallet wallet)
        {
            this.resources = wallet.resources;
        }
        public Wallet(Dictionary<IResource, float> resources)
        {
            this.resources = new List<IResource>();
            foreach(KeyValuePair<IResource, float> resourceItem in resources)
            {
                this.resources.Add(resourceItem.Key);
                Deposit(resourceItem.Key, resourceItem.Value);
            }
        }
        /// <summary>
        /// Gets the amount of a resource type held by the player
        /// </summary>
        /// <param name="resource">ResourceType</param>
        /// <returns></returns>
        public int Count(IResource resourceType)
        {
            return (int)resources[IndexOf(resourceType)].Count;
        }

        public virtual bool Deposit(IResource resource, float amount)
        {
            resources[IndexOf(resource)].Count += amount;
            return false;
        }

        public virtual bool Deposit(Wallet wallet)
        {
            foreach (IResource resource in wallet.resources)
            {
                Deposit(resource, wallet.Count(resource));
            }
            return false;
        }

        public void Clear()
        {
            resources.Clear();
        }

        public bool Contains(IResource item)
        {
            foreach (IResource resource in resources)
            {
                if (resource.GetType() == item.GetType())
                {
                    return true;
                }
            }
            return false;
        }

        private int IndexOf(IResource item)
        {
            foreach (IResource resource in resources)
            {
                if (resource.GetType() == item.GetType())
                {
                    return resources.IndexOf(resource);
                }
            }
            return -1;
        }

        public Wallet Withdraw()
        {
            Wallet WithdrawWallet = new Wallet();
            foreach (IResource resource in resources)
            {
                if (this.Count(resource) > 0)
                {
                    if (this.Count(resource) > Count(resource))
                    {
                        return null;
                    }
                    WithdrawWallet.Deposit(resource, this.resources[this.IndexOf(resource)].Count);
                    this.Withdraw(resource, this.resources[this.IndexOf(resource)].Count);
                }
            }
            return WithdrawWallet;
        }

        public Wallet Withdraw(Wallet cost)
        {
            Wallet WithdrawWallet = new Wallet();
            foreach (IResource resource in cost.resources)
            {
                if (cost.Count(resource) > 0)
                {
                    if (cost.Count(resource) > Count(resource))
                    {
                        return null;
                    }
                    WithdrawWallet.Deposit(resource, cost.resources[cost.IndexOf(resource)].Count);
                    this.Withdraw(resource, cost.resources[cost.IndexOf(resource)].Count);
                }
            }
            return WithdrawWallet;
        }

        public Wallet Withdraw(IResource resource)
        {
            Wallet WithdrawWallet = new Wallet();
            int amount = Count(resource);
            resources[IndexOf(resource)].Count -= amount;
            WithdrawWallet.Deposit(resource, amount);
            return WithdrawWallet;
        }

        public Wallet Withdraw(IResource resource, float amount)
        {
            Wallet WithdrawWallet = new Wallet();
            if (Count(resource) >= amount)
            {
                resources[IndexOf(resource)].Count -= amount;
                WithdrawWallet.Deposit(resource, amount);
                return WithdrawWallet;
            }
            else
            {
                return null;
            }
        }

        public bool CheckCost(Building build)
        {
            foreach (IResource resource in build.Cost.resources)
            {
                if (build.Cost.Count(resource) > this.Count(resource))
                {
                    return false;
                }
            }
            return true;
        }

        //Returns the teams resources
        internal List<string> ResourceString()
        {
            List<string> resourceStringList = new List<string>();
            resourceStringList.Clear();
            resourceStringList.Add($"Wood:{Count(new Wood())}");
            resourceStringList.Add($"Energy:{Count(new Energy())}");
            resourceStringList.Add($"Iron:{Count(new Iron())}");
            resourceStringList.Add($"Likes:{Count(new Likes())}");
            resourceStringList.Add($"Money:{Count(new Money())}");
            resourceStringList.Add($"Steel:{Count(new Steel())}");
            return resourceStringList;
        }
    }
}
