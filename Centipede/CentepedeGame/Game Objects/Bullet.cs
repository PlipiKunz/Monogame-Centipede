using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{

    public class Bullet : ObjectInGame
    {
        public bool hit;
        public float pixelsToMoveEverySecond = 1000f / 1000f;

        new public void initialize(int   x, int  y, int  width, int  height)
        { 
            base.initialize(x, y, width, height);
        }

        public override void update(GameTime gameTime, Collider c)
        {
            move(0, -1, gameTime, pixelsToMoveEverySecond);

            List<collisionType> boundaryCollisions = c.screenBoundaryCollision(this);
            boundaryCollisions.AddRange(c.enemyCollision(this));
            boundaryCollisions.AddRange(c.checkCollision(this, new List<collisionType>() { collisionType.Mushroom }));
            if (boundaryCollisions.Count > 0) { 
                this.hit = true;
            }


        }

    }
}
    