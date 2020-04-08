using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using WorldManager;

namespace _0x46696E616C.CommandPattern
{
    class CommandComponent : GameComponent, ICommandComponent
    {
        Wallet resources;
        List<IUnit> SelectedUnits;
        internal List<IUnit> units;
        List<Building> Buildings;
        List<Building> toBuild;
        Energy energy;
        float timer;
        public CommandComponent(Game game, Wallet startingResources) : base(game)
        {
            energy = new Energy();
            Buildings = new List<Building>();
            toBuild = new List<Building>();
            resources = startingResources;
        }
        /// <summary>
        /// Test Constructor to easily get units
        /// </summary>
        /// <param name="game"></param>
        /// <param name="startingResources"></param>
        public CommandComponent(Game game, Wallet startingResources, List<IUnit> units) : base(game)
        {
            energy = new Energy();
            Buildings = new List<Building>();
            toBuild = new List<Building>();
            resources = startingResources;
            SelectedUnits = this.units = units;
        }

        public void Select(List<IUnit> units)
        {
            SelectedUnits = units;
        }
        public void Attack(IEntity target)
        {
            throw new NotImplementedException();
        }

        public void Move(Vector2 Position)
        {
            foreach (IUnit unit in units)
            {
                if (unit is UnitComponent)
                {
                    ((UnitComponent)unit).Move(Position);
                }
            }
        }


        internal void Build(Building building, WorldHandler wh, Vector2 Position)
        {
            Wallet wallet = resources.Withdraw(building.Cost);
            if (wallet != null)
            {
                if (wh.Place(building, Position))
                {
                    toBuild.Add(building);
                    if (SelectedUnits != null)
                    {
                        foreach (IUnit unit in SelectedUnits)
                        {
                            if (unit is UnitComponent)
                            {
                                ((UnitComponent)unit).Build(building);
                            }
                        }
                    }
                }
                else
                {
                    resources.Deposit(wallet);
                }
            }
        }

        public string Resources()
        {
            string returnMessage = $"Energy:{resources.Count(new Energy())} Iron:{resources.Count(new Iron())} Likes:{resources.Count(new Likes())} Money:{resources.Count(new Money())} Steel:{resources.Count(new Steel())}"; ;
            return returnMessage;
        }

        public string Time()
        {
            int hours = 0;
            int minutes = 0;
            int seconds = (int)(timer / 1000);

            seconds %= 60;
            minutes = seconds / 60;
            if (minutes > 60)
            {
                minutes %= 60;
                hours /= 60;
            }
            if (hours > 0)
                return $"{hours}:{minutes}:{seconds}";
            return $"0:{minutes}:{seconds}";
        }

        public override void Update(GameTime gameTime)
        {
            foreach(IUnit unit in units)
            {
                if(unit is UnitComponent)
                {
                    ((UnitComponent)unit).Update(gameTime);
                }
            }
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer / 1000 >= 1)
            {
                ProduceResources();
                ChargeEnergy();
                Construction();
            }
            base.Update(gameTime);
        }

        private void Construction()
        {
            for (int i = 0; i < toBuild.Count; i++)
            {
                toBuild[i].healthBar.UpdateHealth(toBuild[i], Game.GraphicsDevice);
                toBuild[i].Construct(20/60f); //TODO implement worker proficiency at repairing/building here(More workers/better tech should speed this process up)
                if (toBuild[i].CurrentHealth >= toBuild[i].TotalHealth)
                {
                    Buildings.Add(toBuild[i]);
                    toBuild.RemoveAt(i);
                }
            }

        }

        public void ChargeEnergy()
        {
            foreach (Building building in Buildings)
            {
                resources.Withdraw(energy, building.energyCost / 60f); // TODO implement an building shutoff if the player runs out of energy
            }
        }

        public void ProduceResources()
        {
            foreach (Building building in Buildings)
            {
                if (building is IProductionCenter)
                {
                    for (int i = 0; i < ((IProductionCenter)building).productionTypes.Count; i++)
                    {
                        resources.Deposit(((IProductionCenter)building).productionTypes[i], ((IProductionCenter)building).ProductionAMinute / 60f);
                    }
                }
            }
        }
    }
}
