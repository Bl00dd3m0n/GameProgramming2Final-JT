using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.CommandPattern.GameCommands;
using _0x46696E616C.Input;
using _0x46696E616C.MobHandler;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.UIComponents;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
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
        MouseKeyboard input;
        internal CommandComponent cc { get; private set; }
        public Camera camera { get; private set; }
        List<ICommand> commands;
        List<CommandButton> buttons;
        public Vector2 CurrentPos { get; private set; }
        public Overlay overlay { get; set; }
        public CommandProccesor(Game game, List<IUnit> startingUnits, WorldHandler wh, MouseKeyboard input, CommandComponent command, Camera camera) : base(game)
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
            CurrentPos = (camera.Position + (input.inputPos / (Tile.Zoom * 16)));
            Command command = null;
            if (input is MouseKeyboard && Game.IsActive)
            {
                if (input.GetKeyDown(Keys.Escape))
                {
                    cc.selectedBuild = null;
                }
                if (input.inputPos.Y >= 33 && input.inputPos.Y <= 345) // Overlay positioning - this should probably be more dynamic
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
            if (input.LeftClick())
            {

                if (cc.selectedBuild != null)
                {
                    return new BuildCommand(cc.selectedBuild, wh, (CurrentPos).ToPoint().ToVector2());
                }
                else
                {
                    ModifiableTile tile = null;
                    for (int i = 0; i < cc.Units.Count; i++)
                    {
                        bool XLessThan = cc.Units[0].Position.X <= CurrentPos.X;
                        bool YLessThan = cc.Units[0].Position.Y <= CurrentPos.Y;
                        bool XPlusSizeLessThan = cc.Units[0].Position.X + cc.Units[0].Size.X >= CurrentPos.X;
                        bool YPlusSizeLessThan = cc.Units[0].Position.Y + cc.Units[0].Size.Y >= CurrentPos.Y;
                        if (XLessThan && YLessThan && XPlusSizeLessThan && YPlusSizeLessThan)
                        {
                            tile = (ModifiableTile)cc.Units[i];
                        }
                    }

                    if (tile == null) {
                        tile = wh.GetTile(CurrentPos);
                    }
                    if (tile is ReferenceTile)
                    {
                        tile = ((ReferenceTile)tile).tile;
                    }
                    if (tile != null)
                    {
                        overlay.RemoveAllComponents(typeof(CommandButton));
                        if (tile is UnitComponent)
                        {
                            //BuildQueue size
                            //Pos: (591,391) Size: (200,120)
                            float x = 591;//MaxX = 791
                            float y = 359;//MaxY = 511
                            float scale = 0.25f;
                            foreach (IQueueable<TextureValue> queueable in ((UnitComponent)tile).QueueableThings)
                            {
                                if (queueable is Building)
                                {
                                    ((Building)queueable).UpdatePosition(new Vector2(x,y));
                                    Component com = new CommandButton(Game.GraphicsDevice, new BuildSelectCommand((Building)queueable), queueable,new Point(32));
                                    float width = ((Building)queueable).Size.X;
                                    float height = ((Building)queueable).Size.Y;
                                    com.Scale = 0.25f;
                                    overlay.AddComponent(com);
                                    x += 128 * scale;
                                    if (x +128 * scale> 791)
                                    {
                                        x = 591;
                                        y += 128 * scale;
                                    }
                                }
                            }
                        }
                        else if(tile is Building)
                        {
                            float x = 591;//MaxX = 791
                            float y = 359;//MaxY = 511
                            float scale = 0.25f;
                            Component com = null;
                            foreach (IQueueable<TextureValue> queueable in ((Building)tile).QueueableThings)
                            {
                                if (queueable is UnitComponent)
                                {
                                    ((UnitComponent)queueable).UpdatePosition(new Vector2(x, y));
                                    com = new CommandButton(Game.GraphicsDevice, new TrainCommand((IUnit)queueable, (Building)tile), queueable, new Point(32));
                                    float width = ((UnitComponent)queueable).Size.X;
                                    float height = ((UnitComponent)queueable).Size.Y;
                                    com.Scale = 2;
                                    overlay.AddComponent(com);
                                    x += 16 * scale;
                                    if (x + 16 * scale > 791)
                                    {
                                        x = 591;
                                        y += 16 * scale;
                                    }
                                }
                            }
                            com = new CommandButton(Game.GraphicsDevice, new SetSpawnPointCommand(currentPos,(Building)tile), new Vector2(757,447), TextureValue.SpawnPoint, new Point(32));
                        }
                    }
                }
            }
            else if (input.RightClick())
            {
                Tile tile = wh.GetTile(CurrentPos);
                if(tile is ReferenceTile)
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
                    return new Movecommand((CurrentPos).ToPoint().ToVector2());
                }
            }
            else
            {
                return null;
            }
            return null;
        }
    }
}
