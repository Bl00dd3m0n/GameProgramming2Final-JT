using _0x46696E616C.Buildings;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Technologies;
using TechHandler;

namespace _0x46696E616C.CommandPattern
{
    internal class LearnCommand : Command
    {
        private ITech tech;
        private Building building;

        public LearnCommand(ITech tech, Building building)
        {
            this.CommandName = "Learn Command";
            this.tech = tech;
            this.building = building;
        }
        public override void Execute(CommandComponent uc)
        {
            uc.Learn(building, tech);
            this.Log(uc);
        }
        public override string Description()
        {
            string description = string.Empty;
            description += $"{tech.GetType()}\n";
            description += "Cost:\n";
            foreach (string resource in ((Technology)tech).Cost.ResourceString())
            {
                description += $"{resource}\n";
            }
            //description += tech.Description;
            return description;
        }
    }
}