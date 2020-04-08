using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern.Commands
{
    class Movecommand : Command
    {
        private Vector2 position;
        public Movecommand(Vector2 position)
        {
            this.CommandName = "Move";
            this.position = position;
        }

        public override void Execute(CommandComponent uc)
        {
            uc.Move(position);
            base.Execute(uc);
        }
    }
}
