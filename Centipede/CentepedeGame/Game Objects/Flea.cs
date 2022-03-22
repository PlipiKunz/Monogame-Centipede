using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{
    public class Flea : ObjectInGame
    {
        public float pixelsToMoveEverySecond = 200f / 1000f;
        private Mushroomgrid grid;

        public bool hit;
        Random random;

        public void initialize(int  x, int  y, int  width, int height, Mushroomgrid mg, Random r) { 
            base.initialize(x, y, width, height);
            grid = mg;
            random = r;

        }

        public override void update(GameTime gameTime, Collider c) {
            if (bulletCollision(gameTime, c)) { 
                 hit = true;
            }

            move(0, 1, gameTime, pixelsToMoveEverySecond);

            if (random.NextDouble() > .98)
            {
                grid.addMushroomSpecificLoc(x, y);
            }
        }
    }
}
    