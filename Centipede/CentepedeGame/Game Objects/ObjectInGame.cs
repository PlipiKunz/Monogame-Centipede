using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.Game_Objects
{
    public abstract class ObjectInGame
    {
        //these are all set up 0-1 relative to the screen as a whole
        public int x;
        public int y;
        public int width;
        public int height;

        public void initialize(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

        }

        public Rectangle getBoundingBox() {

            int adjustedW = (int)width;
            int adjustedH = (int)height;
            int adjustedX = (int)x;
            int adjustedY = (int) y;

            return new Rectangle(adjustedX,adjustedY,adjustedW,adjustedH);
        }

        public abstract void update(GameTime gameTime);

    }

}
