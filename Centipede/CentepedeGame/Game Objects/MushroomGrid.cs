using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame.ObjectsInGame
{

    public class Mushroomgrid
    {
        Random random;

        public List<Mushroom> mushrooms;

        public int upperY;
        private int upperX;
        public int standardWidth;
        public int standardHeight;

        public int rows;
        public int columns;

        new public void initialize(int  upperY,   int upperX, int  standardwidth, int  standardheight)
        { 
            random = new Random();
            mushrooms = new List<Mushroom>();
            this.upperY = upperY;
            this.upperX= upperX;
            this.standardWidth = standardwidth;
            this.standardHeight = standardheight;

            rows = upperY / standardWidth;
            columns = upperX / standardHeight;

            resetMushrooms();
        }

        public void resetMushrooms() { 
            mushrooms.Clear();

            for (int r = 2; r < rows-2; r++)
            {
                for (int c = 0; c < columns; c++) {

                    if (random.NextDouble() > .9)
                    {
                        addMushroom(c, r);
                    }
                }
            }
        }

        public void update(GameTime gameTime, Collider c) {
            foreach (Mushroom mushroom in mushrooms) { 
                mushroom.update(gameTime, c);
            }
        }

        public void addMushroomSpecificLoc(int x, int y) {
            int properX = (int)(x / standardWidth) ;
            int properY = (int)(y / standardHeight);
            addMushroom(properX, properY);
        }

        private void addMushroom(int x_index, int y_index) {
            if (x_index >= 0 && y_index >= 0 && x_index < columns && y_index < rows) {
                int x_pos = x_index * standardWidth;
                int y_pos = y_index * standardHeight;
                if (!mushroomInLoc(x_pos, y_pos)) { 
                    Mushroom new_mushroom = new Mushroom();
                    new_mushroom.initialize(x_pos, y_pos, standardWidth, standardHeight);
                    mushrooms.Add(new_mushroom);
                }
            }
        }

        public bool mushroomInLoc(int x_pos, int y_pos) {
            foreach (Mushroom mushroom in mushrooms)
            {
                if (mushroom.x == x_pos && mushroom.y == y_pos) { 
                    return true;
                }
            }

            return false;            
        }
    }
}
    