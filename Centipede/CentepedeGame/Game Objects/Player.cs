using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.Game_Objects
{
    public class Player : ObjectInGame
    {
        public int lives;

        public float pixelsToMoveEverySecond = 250f / 1000f;

        public void initialize(int   x, int  y, int  width, int  height)
        { 
            base.initialize(x, y, width, height);
            lives = 3;
        }

        public override void update(GameTime gameTime) { 

        }

        public void move(float x_movement, float y_movement, GameTime gameTime) {
            Vector2 movement = new Vector2(x_movement, y_movement);
            movement.Normalize();
            x_movement = movement.X;
            y_movement = movement.Y;

            float old_x = x;
            float old_y = y;

            x += (int) (x_movement * pixelsToMoveEverySecond * gameTime.ElapsedGameTime.TotalMilliseconds);
            y += (int)(y_movement * pixelsToMoveEverySecond * gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void moveLeft(GameTime gameTime) { 
            move(-1,0,gameTime);
        }
        public void moveRight(GameTime gameTime)
        {
            move(1, 0, gameTime);
        }
        public void moveUp(GameTime gameTime)
        {
            move(0, -1, gameTime);
        }
        public void moveDown(GameTime gameTime)
        {
            move(0, 1, gameTime);
        }
    }
}
