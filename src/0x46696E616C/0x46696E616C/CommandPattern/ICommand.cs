﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern
{
    interface ICommand
    {
        void Execute(CommandComponent uc);
        string Description();
    }
}
