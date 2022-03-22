using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{
    public abstract class ObjectInGame
    {
        public int x;
        public int y;
        public int width;
        public int height;

        private int origionalX;
        private int origionalY;
        private int origionalWidth;
        private int origionalHeight;

        public void initialize(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            origionalX = x;
            origionalY = y;
            origionalWidth = width;
            origionalHeight = height;
        }

        public Rectangle getBoundingBox() {

            int adjustedW = width;
            int adjustedH = height;
            int adjustedX = x;
            int adjustedY = y;

            return new Rectangle(adjustedX,adjustedY,adjustedW,adjustedH);
        }

        public abstract void update(GameTime gameTime, Collider c);

        public void resetPos() {
            x = origionalX;
            y = origionalY;
            width = origionalWidth;
            height = origionalHeight;
        }


        public void move(float x_movement, float y_movement, GameTime gameTime, float pixelsToMoveEverySecond)
        {
            Vector2 movement = new Vector2(x_movement, y_movement);
            movement.Normalize();
            x_movement = movement.X;
            y_movement = movement.Y;

            float old_x = x;
            float old_y = y;

            x += (int)(x_movement * pixelsToMoveEverySecond * gameTime.ElapsedGameTime.TotalMilliseconds);
            y += (int)(y_movement * pixelsToMoveEverySecond * gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        protected bool bulletCollision(GameTime game, Collider c)
        {
            List<collisionType> bulletCollision = c.checkCollision(this, new List<collisionType>() { collisionType.Bullet });
            if (bulletCollision.Count > 0)
            {
                return true;
            }

            return false;
        }
    }

}
