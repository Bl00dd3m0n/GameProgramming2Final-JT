using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;

namespace _0x46696E616C.CommandPattern.Commands
{
    class AttackCommand : Command, ICommand
    {
        IEntity target;
        public AttackCommand(IEntity target)
        {
            this.target = target;
        }

        public override void Execute(CommandComponent uc)
        {
            uc.Attack(target);
            this.Log(uc);
        }
    }
}
