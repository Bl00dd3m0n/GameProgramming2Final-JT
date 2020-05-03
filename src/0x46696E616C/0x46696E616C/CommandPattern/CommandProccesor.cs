using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.CommandPattern.GameCommands;
using _0x46696E616C.Input;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.TechManager.Stats;
using _0x46696E616C.TechManager.Technologies;
using _0x46696E616C.UIComponents;
using _0x46696E616C.Units;
using _0x46696E616C.Units.HostileMobManager;
using _0x46696E616C.Util.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;
using UIProject;
using Util;
using WorldManager;
using WorldManager.MapData;
using WorldManager.TileHandlerLibrary;

namespace _0x46696E616C.CommandPattern
{
    class CommandProccesor : GameComponent
    {
        WorldHandler wh;
        InputDefinitions input;
        internal CommandComponent cc { get; private set; }
        public Camera camera { get; private set; }
        List<ICommand> commands;
        List<CommandButton> buttons;
        public Vector2 CurrentPos { get; private set; }
        public Overlay overlay { get; set; }

        public CommandProccesor(Game game, List<IUnit> startingUnits, WorldHandler wh, InputDefinitions input, CommandComponent command, Camera camera) : base(game)
        {
            this.cc = command;
            this.wh = wh;
            this.input = input;
            this.camera = camera;
            commands = new List<ICommand>();
            buttons = new List<CommandButton>();
        }
        public override void Update(GameTime gameTime)
        {
            CurrentPos = camera.ConvertToWorldSpace(input.InputPos);
            Command command = null;
            if (Game.IsActive)
            {
                if (input.CheckInput(Controls.Deselect))
                {
                    cc.SelectedBuild = null;
                    cc.SpawnMarker = null;
                }
                if (input.InputPos.Y >= 33 && input.InputPos.Y <= 345) // Overlay positioning - this should probably be more dynamic
                {
                    command = OnMapAction(CurrentPos);
                }
                else
                {
                    command = overlay.ClickCheck();
                }
            }
            if (command != null)
            {
                commands.Add(command);
                command.Execute(cc);
            }
            base.Update(gameTime);
        }

        private Command OnMapAction(Vector2 currentPos)
        {
            if (input.CheckInput(Controls.Select))
            {

                if (cc.SelectedBuild != null)
                {
                    return new BuildCommand(cc.SelectedBuild, wh, (CurrentPos).ToPoint().ToVector2());
                }
                else if (cc.SpawnMarker != null)
                {
                    return new SetSpawnPointCommand(currentPos);
                }
                else
                {
                    ModifiableTile tile = null;

                    if (tile == null)
                    {
                        tile = wh.GetTile(CurrentPos);
                    }
                    if (tile is ReferenceTile)
                    {
                        tile = ((ReferenceTile)tile).tile;
                    }
                    if (tile == null)
                    {
                        try
                        {
                            tile = (ModifiableTile)wh.GetUnit(CurrentPos);
                        }
                        catch (ArgumentOutOfRangeException) { }//Prevents a crash if there aren't any units at this point
                    }
                    if (tile != null)
                    {
                        overlay.RemoveAllComponents();
                        if (tile is IEntity)
                        {
                            SelectedUnitDisplay(tile);
                        }
                        if (tile is BasicUnit && tile.TeamAssociation == cc.Team)
                        {
                            AddQueueables(tile);
                            return new SelectCommand((IUnit)tile);
                        }
                        else if (tile is Building)
                        {
                            AddQueueables(tile);
                            Component com = new CommandButton(Game.GraphicsDevice, new SetSpawnPointCommand(currentPos, (Building)tile), new Vector2(757, 447), TextureValue.SpawnPoint, new Point(32));
                            com.Scale = 2;
                            overlay.AddComponent(com);
                        }
                        SelectedUnitDisplay(tile);
                    }
                }
            }
            else if (input.CheckInput(Controls.Interact))
            {
                Tile tile = wh.GetTile(CurrentPos);
                if (tile is ReferenceTile)
                {
                    tile = ((ReferenceTile)tile).tile;
                }
                if (tile is IHarvestable)
                {
                    return new AttackCommand((IEntity)tile);
                }
                else if (tile is Building)
                {
                    if (((Building)tile).CurrentHealth >= ((Building)tile).TotalHealth)
                    {
                        return new GarrisonCommand((Building)tile);
                    }
                    else
                    {
                        return new RepairCommand((Building)tile); //Should be a repair command Garrison at the moment will cause the buildings to be repaired....needs to be updated later
                    }
                }
                else
                {
                    IUnit unit = wh.GetUnit(CurrentPos);
                    if (unit != null)
                    {
                        return new AttackCommand(unit);
                    }
                    else
                    {
                        return new Movecommand((CurrentPos).ToPoint().ToVector2());
                    }
                }
            }
            else if (input.CheckRelease(Controls.Select))
            {

                Vector2 startPoint = camera.ConvertToWorldSpace(input.StartPosition);
                Vector2 endPoint = camera.ConvertToWorldSpace(input.EndPosition);
                List<IUnit> unitSelection = new List<IUnit>();
                Vector2 tempStart = startPoint;
                Vector2 tempEnd = endPoint;
                bool UnitsTheSame = true;
                #region Fix Start/End points
                int startY = (int)startPoint.Y > endPoint.Y ? (int)endPoint.Y : (int)startPoint.Y; //If the endpoint is above the start point flip it
                int startX = (int)startPoint.X > endPoint.X ? (int)endPoint.X : (int)startPoint.X; //if the end point is left of the start point flip it
                int endY = (int)startPoint.Y == startY ? (int)endPoint.Y : (int)startPoint.Y; //if the start point is the start point stay the same if not flip
                int endX = (int)startPoint.X == startX ? (int)endPoint.X : (int)startPoint.X; //^
                #endregion

                for (int y = (int)startY; y < endY; y++)
                {
                    for (int x = (int)startX; x < endX; x++)
                    {
                        IUnit unit = wh.GetUnit(new Vector2(x, y));
                        if (unit != null)
                        {
                            unitSelection.Add(unit);
                        }
                    }
                }
                if (unitSelection.Count > 0)
                {
                    if (unitSelection.Where(l => l.GetType() != unitSelection[0].GetType()).Count() > 0) // if there are any units not like the first unit in the list they aren't the same otherwise they are
                    {
                        UnitsTheSame = false;
                    }
                }
                if (UnitsTheSame && unitSelection.Count > 0)
                {
                    overlay.RemoveAllComponents();
                    AddQueueables((Tile)unitSelection[0]);
                    if (unitSelection.Count == 1) SelectedUnitDisplay((ModifiableTile)unitSelection[0]);
                    else displaySelectedUnits(unitSelection);
                }
                if (unitSelection.Count > 0)
                {
                    return new SelectCommand(unitSelection);
                }
            }
            return null;
        }

        private void displaySelectedUnits(List<IUnit> units)
        {
            Component com = null;
            float comX = 217;
            foreach (IUnit unit in units)
            {
                com = new ImageBox(((ModifiableTile)unit).block.texture, new Vector2(comX, 359), new Point(1, 1), Color.White);
                com.Scale = 1;
                overlay.AddComponent(com);
                com = new ImageBox(unit.healthBar.Health, new Vector2(comX, 359 + 19), new Point(1, 1), Color.White);
                com.Scale = 1;
                overlay.AddComponent(com);
                comX += (unit.Size.X * 16) + 5;
            }
        }

        private void SelectedUnitDisplay(ModifiableTile tile)
        {
            Component com = new ImageBox(tile.block.texture, new Vector2(227, 359), (tile.Size * 16).ToPoint(), Color.White);
            com.Scale = 2 / (tile.Size.X);
            overlay.AddComponent(com);
            float y = 380;
            for (int i = 0; i < tile.stats.Count; i++)
            {
                if (tile.stats[i] is Health)
                {
                    com = new ImageBox(tile.healthBar.Health, new Vector2(227, 359 + 32), new Point((int)com.Scale, 1), Color.White);
                    com.Scale = 2;
                    overlay.AddComponent(com);
                    com = com = new Label(new Vector2(217 + 32, 359 + 64), $"{tile.CurrentHealth}/{tile.TotalHealth}", Color.White);
                }
                else
                {
                    com = new ImageBox(tile.stats[i].Texture, new Vector2(300, y - 8), new Point(1, 1), Color.White);
                    com.Scale = 0.25f;
                    overlay.AddComponent(com);
                    com = new Label(new Vector2(330, y), tile.stats[i].Value.ToString(), Color.White);
                    com.Scale = 1;
                }
                com.drawComponent = true;
                overlay.AddComponent(com);
                y += 12 + 5;
            }
        }

        private void AddQueueables(Tile tile)
        {
            //BuildQueue size
            //Pos: (591,391) Size: (200,120)
            float x = 591;//MaxX = 791
            float y = 359;//MaxY = 511
            float scale = 0.25f;
            if (tile is BasicUnit && !(tile is HostileMob) && ((BasicUnit)tile).QueueableThings != null) // HACK need to move the queueable things up the chain
            {
                foreach (IQueueable<TextureValue> queueable in ((BasicUnit)tile).QueueableThings)
                {
                    if (queueable is Building)
                    {
                        ((Building)queueable).UpdatePosition(Game.GraphicsDevice, new Vector2(x, y));
                        Component com = new CommandButton(Game.GraphicsDevice, new BuildSelectCommand((Building)queueable), new Vector2(x, y), queueable.Icon, new Point(32));
                        float width = ((Building)queueable).Size.X;
                        float height = ((Building)queueable).Size.Y;
                        com.Scale = 0.25f;
                        overlay.AddComponent(com);
                        x += 128 * scale;
                        if (x + 128 * scale > 791)
                        {
                            x = 591;
                            y += 128 * scale;
                        }
                    }
                }
            }
            else if (tile is Building && ((ModifiableTile)tile).built)
            {
                foreach (IQueueable<TextureValue> queueable in ((Building)tile).QueueableThings)
                {
                    if (queueable is BasicUnit)
                    {
                        ((BasicUnit)queueable).UpdatePosition(Game.GraphicsDevice, new Vector2(x, y));
                        Component com = new CommandButton(Game.GraphicsDevice, new TrainCommand((IUnit)queueable, (Building)tile), queueable, new Point(32));
                        float width = ((BasicUnit)queueable).Size.X;
                        float height = ((BasicUnit)queueable).Size.Y;
                        com.Scale = 2;
                        overlay.AddComponent(com);
                        x += 128 * scale;
                        if (x + 128 * scale > 791)
                        {
                            x = 591;
                            y += 128 * scale;
                        }
                    }
                    if (queueable is ITech)
                    {
                        ((Technology)queueable).Position = new Vector2(x, y);
                        Component com = new CommandButton(Game.GraphicsDevice, new LearnCommand((ITech)queueable, (Building)tile), queueable, new Point(32));
                        float width = ContentHandler.DrawnTexture(((Technology)queueable).Icon).Bounds.Size.X;
                        float height = ContentHandler.DrawnTexture(((Technology)queueable).Icon).Bounds.Size.Y;
                        com.Scale = 0.5f;
                        overlay.AddComponent(com);
                        x += 128 * scale;
                        if (x + 128 * scale > 791)
                        {
                            x = 591;
                            y += 128 * scale;
                        }
                    }
                }
            }
        }
    }
}
