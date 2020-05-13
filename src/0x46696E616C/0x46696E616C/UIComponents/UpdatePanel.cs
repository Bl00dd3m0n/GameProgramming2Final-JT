using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.UIComponents.Stats;
using Microsoft.Xna.Framework;
using UIProject;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.UIComponents
{
    class UpdatePanel : Panel
    {
        ModifiableTile tile;
        public UpdatePanel(ModifiableTile tile, Game game, Rectangle bounds, CommandProccesor cp) : base(game, bounds, cp)
        {
            this.tile = tile;
        }

        public override void Update(GameTime gameTime)
        {
            for(int i = 0; i < components.Count; i++)
            {
                if(components[i] is StatComponent)
                {
                    StatComponent component = (StatComponent)components[i];
                    if(tile.stats[component.stat.GetType()] + tile.TeamStats[component.stat.GetType()] != component.stat + tile.TeamStats[((StatComponent)components[i]).stat.GetType()])
                    {
                        string display = tile.stats[component.stat.GetType()].Value.ToString();
                        if (tile is BasicUnit)
                        {
                            Stat stat = null;
                            if (((BasicUnit)tile).teamStats != null)
                            {
                                stat = ((BasicUnit)tile).teamStats[tile.stats[i].GetType()];
                            }
                            if (stat != null)
                            {
                                display += $" ({stat.Value.ToString()})";
                            }
                        }
                        component.component.Text = display;
                    }
                } 
            }
            base.Update(gameTime);
        }
    }
}
