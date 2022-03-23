using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{
    public enum SpiderMode { 
        updown,
        diagonal
    }

    public class Spider : ObjectInGame
    {
        public float pixelsToMoveEverySecond = 500f / 1000f;
        public bool hit;

        public int xDirection;
        public int yDirection;
        public SpiderMode sm;
        Random random;
        Mushroomgrid mg;
        private bool ableToTurnAround = false;
        private int timeTillTurnAround = 0;
        public void initialize(int  x, int  y, int  width, int height, Random r, Mushroomgrid mg) { 
            base.initialize(x, y, width, height);

            sm = SpiderMode.diagonal;
            xDirection = 1;
            yDirection = 1;
            random = r;
            ableToTurnAround = false;
            this.mg = mg;
        }

        private List<int> possibleDirections = new List<int>() {-1, 1};

        public override void update(GameTime gameTime, Collider c) {
            if (bulletCollision(gameTime, c))
            {
                hit = true;
            }

            if (random.NextDouble() > .97) {
                if (sm == SpiderMode.updown)
                {
                    sm = SpiderMode.diagonal;
                }
                else {
                    sm = SpiderMode.updown;
                }
            }

            List<collisionType> boundaryCollisions = c.screenBoundaryCollision(this);
            if (boundaryCollisions.Count > 0)
            {
                if (boundaryCollisions.Contains(collisionType.ScreenLeft) || boundaryCollisions.Contains(collisionType.ScreenRight))
                {
                    xDirection *= -1;
                    ableToTurnAround = true;
                }
                if (boundaryCollisions.Contains(collisionType.ScreenTop) || boundaryCollisions.Contains(collisionType.ScreenBottom))
                {
                    yDirection *= -1;
                }

            }
            else if (y < (mg.upperY / 3))
            {
                yDirection = 1;
            }
            else
            {

                //random turn around
                if (timeTillTurnAround == 0 && random.NextDouble() > .98)
                {
                    if (ableToTurnAround)
                    {
                        xDirection = possibleDirections[random.Next(0, 2)];
                    }
                    yDirection = possibleDirections[random.Next(0, 2)];
                    timeTillTurnAround = 2000;
                }
            }


            if (sm == SpiderMode.diagonal)
            {
                move(xDirection, yDirection, gameTime, pixelsToMoveEverySecond);
            }
            else
            {
                move(0, yDirection, gameTime, pixelsToMoveEverySecond);
            }

            timeTillTurnAround -= (int)(gameTime.TotalGameTime.TotalMilliseconds);
            if (timeTillTurnAround < 0) {
                timeTillTurnAround = 0;
            }
        }
    }
}
    