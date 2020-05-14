using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern.GameCommands
{
    class ExitGameCommand : Command
    {
        public ExitGameCommand()
        {
            this.CommandName = "End Game";
        }

        public override void Execute(CommandComponent uc)
        {
            uc.EndGame();
            this.Log(uc);
        }
    }
}
