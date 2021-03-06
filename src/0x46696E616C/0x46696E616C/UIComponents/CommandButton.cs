﻿using _0x46696E616C.CommandPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NationBuilder.DataHandlerLibrary;
using NationBuilder.TileHandlerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechHandler;
using UIProject;

namespace _0x46696E616C.UIComponents
{
    class CommandButton : Button
    {
        public ICommand command;
        //If the button gets clicked
        public override void Click(Game game)
        {
            Clicked = true;
        }
        #region constructors
        //Note - For an xml serialization there has to be an empty constructor however, a graphics device is almost needed for base functions so I made all public facing constructors require a graphics device
        public CommandButton(GraphicsDevice gd) : this() { graphics = gd; }
        public CommandButton(GraphicsDevice gd, ICommand command, IQueueable<TextureValue> queueableObject, Point size) : this(command, queueableObject, size) { graphics = gd; }
        public CommandButton(GraphicsDevice gd, ICommand command, Vector2 position, TextureValue tex, Point size) : this(command, position, tex, size) { graphics = gd; }

        protected CommandButton() : this(null, null, new Point(0, 0)) { }

        protected CommandButton(ICommand command, Vector2 position, TextureValue picture, Point size) : base(position, size, Color.White)
        {
            this.bounds = new Rectangle(position.ToPoint(), Size);
            this.Position = position;
            this.picture = ContentHandler.DrawnTexture(picture);
            this.command = command;
            this.Size = size;
        }

        protected CommandButton(ICommand command, IQueueable<TextureValue> queueableObject, Point Size)
        {
            this.bounds = new Rectangle(queueableObject.Position.ToPoint(), Size);
            this.Position = queueableObject.Position;
            this.picture = ContentHandler.DrawnTexture(queueableObject.Icon);
            this.command = command;
        }
        #endregion

        public override string Description()
        {
            return command.Description();
        }
    }
}