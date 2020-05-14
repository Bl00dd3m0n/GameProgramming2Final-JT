using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.UIComponents.Stats;
using Microsoft.Xna.Framework;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using TechHandler;
using UIProject;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.UIComponents
{
    class UpdatePanel : Panel
    {
        ModifiableTile tile;
        float prevHealth;
        float prevCount;
        List<IComponent> QueueableUnits;
        public UpdatePanel(ModifiableTile tile, Game game, Rectangle bounds, CommandProccesor cp) : base(game, bounds, cp)
        {
            this.tile = tile;
            prevHealth = tile.CurrentHealth;
            QueueableUnits = new List<IComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            if (tile is Building)
            {
                if (((Building)tile).trainingQueue.Count != prevCount)//Handle Queued item display
                {
                    float x = 220;
                    foreach (IComponent comp in QueueableUnits)
                    {
                        if (components.Contains(comp))
                        {
                            components.Remove(comp);
                        }
                    }
                    QueueableUnits.Clear();
                    foreach (IQueueable<TextureValue> queueable in ((Building)tile).trainingQueue)
                    {
                        float width = ContentHandler.DrawnTexture(queueable.Icon).Bounds.Size.X;
                        x += 16 + 1;
                        QueueableUnits.Add(new ImageBox(queueable.Icon, new Vector2(x, 433), ContentHandler.DrawnTexture(queueable.Icon).Bounds.Size, Color.White));
                        float scale = width / 16;
                        ((Component)QueueableUnits[QueueableUnits.Count - 1]).Scale = 1f/scale;
                        components.Add(QueueableUnits[QueueableUnits.Count-1]);
                    }
                    prevCount = ((Building)tile).trainingQueue.Count;
                }
            }
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is StatComponent)
                {
                    StatComponent component = (StatComponent)components[i];
                    string display = component.component.Text;
                    if (tile.stats[component.statType] is Health)
                    {
                        if (prevHealth != tile.CurrentHealth)
                        {
                            display = $"{tile.CurrentHealth}/{tile.TotalHealth}";
                        }
                    }
                    else if (tile.stats[component.statType].Value + tile.TeamStats[component.statType].Value != component.value)
                    {
                        display = tile.stats[component.statType].Value.ToString();
                        if (tile.TeamStats[component.statType] != null)
                            display += $"({tile.TeamStats[component.statType].Value})";
                        component.value = tile.stats[component.statType].Value + tile.TeamStats[component.statType].Value;
                    }
                    component.component.Text = display;
                }
            }
            base.Update(gameTime);
        }
    }
}
