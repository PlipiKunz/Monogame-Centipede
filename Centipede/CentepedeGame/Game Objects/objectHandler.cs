using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{

    public class objectHandler
    {
        Random random;
        Mushroomgrid mushroomGrid;

        public Flea? f = null;
        public Scorpion? s = null;
        public Spider? sp = null;
        new public void initialize(Mushroomgrid mushroomgrid)
        {
            random = new Random();
            mushroomGrid = mushroomgrid;
        }

        public void reset() { 
            f = null;
            s = null;
            sp = null;
        }

        public void update(GameTime gameTime, Collider c) {
            if (f == null)
            {
                if (random.NextDouble() > .995) { 
                    f = new Flea();

                    int fleaX = random.Next(0, mushroomGrid.columns) * mushroomGrid.standardWidth;
                    int fleaY = 0;

                    f.initialize(fleaX, fleaY, mushroomGrid.standardWidth, mushroomGrid.standardHeight, mushroomGrid, random);
                }
            }
            else { 
                f.update(gameTime, c);

                if (c.screenBoundaryCollision(f).Count > 0) {
                    f = null;
                }
            }

            if (s == null)
            {
                if (random.NextDouble() > .999)
                {
                    s = new Scorpion();

                    int scorpX = 0;
                    int scorpY = random.Next(2, 2*mushroomGrid.rows/3) * mushroomGrid.standardWidth; ;

                    s.initialize(scorpX, scorpY, mushroomGrid.standardWidth, mushroomGrid.standardHeight);
                }
            }
            else
            {
                s.update(gameTime, c);

                if (c.screenBoundaryCollision(s).Count > 0)
                {
                    s = null;
                }
            }

            if (sp == null)
            {
                if (random.NextDouble() > .999)
                {
                    sp = new Spider();

                    int spiderX = 0;
                    int spiderY = random.Next(mushroomGrid.rows / 3+1, mushroomGrid.rows -3) * mushroomGrid.standardWidth; ;

                    sp.initialize(spiderX, spiderY, mushroomGrid.standardWidth, mushroomGrid.standardHeight, random, mushroomGrid);
                }
            }
            else
            {
                sp.update(gameTime, c);

            }
        }
    }
}
    