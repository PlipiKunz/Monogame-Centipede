using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{
    public class Scorpion : ObjectInGame
    {
        public float pixelsToMoveEverySecond = 100f / 1000f;
        public bool hit;

        public void initialize(int  x, int  y, int  width, int height) { 
            base.initialize(x, y, width, height);
        }

        public override void update(GameTime gameTime, Collider c) {
            if (bulletCollision(gameTime, c))
            {
                hit = true;
            }
            move(1, 0, gameTime, pixelsToMoveEverySecond);

        }
    }
}
    