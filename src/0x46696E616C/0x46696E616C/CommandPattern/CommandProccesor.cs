using _0x46696E616C.Buildings;
using _0x46696E616C.CommandPattern.Commands;
using _0x46696E616C.Input;
using _0x46696E616C.MobHandler.Units;
using _0x46696E616C.WorldManager.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;
using WorldManager;

namespace _0x46696E616C.CommandPattern
{
    class CommandProccesor : GameComponent
    {
        WorldHandler wh;
        MouseKeyboard input;
        CommandComponent cc;
        Camera camera;
        List<ICommand> commands;
        public CommandProccesor(Game game, List<IUnit> startingUnits, WorldHandler wh, MouseKeyboard input, CommandComponent command, Camera camera) : base(game)
        {
            this.cc = command;
            this.wh = wh;
            this.input = input;
            this.camera = camera;
            commands = new List<ICommand>();
        }
        public override void Update(GameTime gameTime)
        {   
            Command command = null;
            if (input is MouseKeyboard && Game.IsActive)//TODO make input more dynamic instead of hard coding this
            {
                if (input.LeftClick())
                {
                    command = new BuildCommand(new SolarPanel(Game, TextureValue.SolarPanel, (camera.Position + (input.inputPos / (Tile.Zoom * 16))).ToPoint().ToVector2()), wh, (camera.Position + (input.inputPos / (Tile.Zoom * 16))).ToPoint().ToVector2());
                }
                else if (input.RightClick())
                {
                    command = new Movecommand((camera.Position + (input.inputPos / (Tile.Zoom * 16))).ToPoint().ToVector2());
                }
            }
            if(command != null)
            {
                commands.Add(command);
                command.Execute(cc);
            }
            base.Update(gameTime);
        }
    }
}
