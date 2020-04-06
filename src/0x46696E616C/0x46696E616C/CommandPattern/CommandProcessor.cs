using _0x46696E616C.InputManager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.CommandPattern
{
    class CommandProcessor : GameComponent
    {
        Rectangle Selection;
        List<IUnit> SelectedUnits;
        public CommandProcessor(Game game, PlayerComponent player) : base(game)
        {
            
        }
    }
}
