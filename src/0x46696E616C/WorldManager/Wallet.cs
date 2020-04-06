﻿using _0x46696E616C.WorldManager.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C
{
    public class Wallet
    {
        private List<IResource> resources;
        internal List<IResource> Resources { get { return resources.ToList(); } }

        public Wallet() : this(new List<IResource>())
        {
            resources.Add(new Wood());
            resources.Add(new Steel());
            resources.Add(new Money());
            resources.Add(new Likes());
            resources.Add(new Iron());
            resources.Add(new Energy());

        }

        public Wallet(List<IResource> wallet)
        {
            this.resources = wallet;
        }

        public Wallet(Wallet wallet)
        {
            this.resources = wallet.resources;
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

        public void Deposit(IResource resource, float amount)
        {
            resources[IndexOf(resource)].Count += amount;
        }

        public void Deposit(Wallet wallet)
        {
            foreach (IResource resource in wallet.resources)
            {
                Deposit(resource, wallet.Count(resource));
            }
        }

        public void Clear()
        {
            resources.Clear();
        }

        public bool Contains(IResource item)
        {
            foreach(IResource resource in resources)
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

        public Wallet Withdraw(Wallet cost)
        {
            Wallet WithdrawWallet = new Wallet();
            foreach (IResource resource in cost.resources)
            {
                if (cost.Count(resource) > 0) {
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
        public Wallet Withdraw(IResource resource, float amount)
        {
            Wallet WithdrawWallet = new Wallet();
            if (Count(resource) >= amount) {
                resources[IndexOf(resource)].Count -= amount;
                WithdrawWallet.Deposit(resource, amount);
                return WithdrawWallet;
            } else
            {
                return null;
            }
        }

        public static implicit operator Wallet(List<IResource> v)
        {
            return new Wallet(v);
        }
    }
}