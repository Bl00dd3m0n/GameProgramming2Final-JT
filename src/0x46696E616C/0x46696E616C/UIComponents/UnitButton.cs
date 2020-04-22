using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject
{
    class UnitButton<t> : Button
    {
        t unit;

        public UnitButton(t unit)
        {
            this.unit = unit;
        }
        public override void Click(Game game)
        {
            base.Click(game);
        }
    }
}
