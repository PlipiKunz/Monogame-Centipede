using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{
    public class Player : ObjectInGame
    {
        public int lives;
        public bool hit;
        public float pixelsToMoveEverySecond = 250f / 1000f;

        public void initialize(int   x, int  y, int  width, int  height)
        { 
            base.initialize(x, y, width, height);
            lives = 3;
            hit = false;
        }

        public override void update(GameTime gameTime) { 

        }

        public void moveLeft(GameTime gameTime) { 
            move(-1,0,gameTime, pixelsToMoveEverySecond);
        }
        public void moveRight(GameTime gameTime)
        {
            move(1, 0, gameTime, pixelsToMoveEverySecond);
        }
        public void moveUp(GameTime gameTime)
        {
            move(0, -1, gameTime, pixelsToMoveEverySecond);
        }
        public void moveDown(GameTime gameTime)
        {
            move(0, 1, gameTime, pixelsToMoveEverySecond);
        }
    }
}
