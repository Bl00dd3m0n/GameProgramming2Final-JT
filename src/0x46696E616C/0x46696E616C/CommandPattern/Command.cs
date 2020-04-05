using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern
{
    class Command : ICommand
    {
        public string CommandName;
        public virtual void Execute(CommandComponent uc)
        {
            this.Log(uc);
        }

        protected virtual string Log(CommandComponent uc)
        {
            string LogString = string.Format($"{CommandName} executed.");
            return LogString;
        }
    }
}
