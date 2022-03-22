using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{
    public enum mushroomType { 
        normal,
        poison
    }
    public class Mushroom : ObjectInGame
    {
        public int lives;
        public bool hit;
        public bool remove;
        public mushroomType type;

        new public void initialize(int   x, int  y, int  width, int  height)
        { 
            base.initialize(x, y, width, height);
            lives = 4;
            remove = false;
        }

        public override void update(GameTime gameTime, Collider c) {
            if (bulletCollision(gameTime, c)) {
                this.lives -= 1;
                hit = true;
            }

            if (c.checkCollision(this, new List<collisionType>() { collisionType.scorpion }).Count > 0) {
                type = mushroomType.poison;
            }
            if (c.checkCollision(this, new List<collisionType>() { collisionType.spider }).Count > 0)
            {
                lives = -1;
            }

            if (lives <= 0) { 
                remove = true;
            }
        }



    }
}
    