using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{

    public class Mushroom : ObjectInGame
    {
        public int lives;
        public bool hit;

        public bool makePoison;
        public bool remove;


        new public void initialize(int   x, int  y, int  width, int  height)
        { 
            base.initialize(x, y, width, height);
            lives = 4;
            hit = false;
        }

        public override void update(GameTime gameTime, Collider c) {
        }

    }
}
    