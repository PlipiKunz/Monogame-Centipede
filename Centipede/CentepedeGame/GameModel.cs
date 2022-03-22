
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CS5410.CentepedeGame.ObjectsInGame;

namespace CS5410.CentepedeGame
{
    public class GameModel
    {

        Vector2 screenResolution;

        public float widthResolutionScaler;
        public float heightResolutionScaler;
        public int standardHeight;
        public int standardWidth;

        public int score;
        //if the game is over
        public bool isDone;
        //pause is used when the player is hit, game resumes when any input is given
        public bool paused;

        public Collider collider;

        public Player player;
        public List<CentepedeSegment> segments;
        public Mushroomgrid mushrooms;
        public List<Bullet> bullets;
        public objectHandler oh;

        public void initialize(Vector2 resolution)
        {
            collider = new Collider();
            collider.initialize(this);

            screenResolution = resolution;
            widthResolutionScaler = screenResolution.X;
            heightResolutionScaler = screenResolution.Y;

            //scales things based on starting at 1980/1020
            standardWidth = (int)(50 * (1980 / widthResolutionScaler));
            standardHeight = (int)(50 * (1020 / heightResolutionScaler));


            player = new Player();
            segments = new List<CentepedeSegment>();
            mushrooms = new Mushroomgrid();
            bullets = new List<Bullet>();
            oh = new objectHandler();
            reset();
        }

        public void reset()
        {
            score = 0;
            isDone = false;
            paused = false;
            player.initialize((int)(widthResolutionScaler * .5f), (int)(heightResolutionScaler - standardHeight), standardWidth, standardHeight);
            mushrooms.initialize((int)heightResolutionScaler, (int)widthResolutionScaler, standardWidth, standardHeight);
            oh.initialize(mushrooms);
            resetScreen();
        }

        public void resetScreen()
        {
            player.resetPos();
            segments = CentepedeSegment.generateCentepede(0, 0, standardWidth, standardHeight, 12);
            mushrooms.resetMushrooms();
            bullets.Clear();
            oh.reset();
        }

        public void update(GameTime gameTime)
        {
            //if game hasnt been paused (after the player was hit)
            if (!paused)
            {
                if (segments.Count == 0) {
                    segments = CentepedeSegment.generateCentepede(0, 0, standardWidth, standardHeight, 12);
                }
                foreach (Bullet bullet in bullets)
                {
                    bullet.update(gameTime, collider);
                }
                //update game items
                player.update(gameTime, collider);
                CentepedeSegment.update(gameTime, segments, collider);
                mushrooms.update(gameTime, collider);
                oh.update(gameTime, collider);

                //see if player has died and if so set the game as done
                if (player.lives <= 0)
                {
                    isDone = true;
                }

                
                hitCheck();
            }
        }

        public void hitCheck() {
            //performs hit checks and takes appropriate action on objects that have been hit
            centepedeHitCheck();
            playerHitCheck();
            bulletHitCheck();
            mushroomHitCheck();
            fleaHItCheck();
            scorpHItCheck();
            spiderHItCheck();   
        }

        public void playerHitCheck() {
            //if player gets hit reset screen position
            if (player.hit) {
                resetScreen();
                player.hit = false;
                paused = true;
            }
        }

        public void centepedeHitCheck() {
            CentepedeSegment prevSegment = null;

            List<int> segmentsToRemove = new List<int>();
            for (int i = 0; i < segments.Count; i++)
            {
                CentepedeSegment segment = segments[i];

                if (segment.hit)
                {
                    if (segment.segmentType == SegmentType.Head)
                    {
                        score += 100;
                    }
                    else {
                        score += 10;
                    }

                    segment.split();
                    if (prevSegment != null)
                    {
                        prevSegment.nextSegment = null;
                    }
                    segmentsToRemove.Add(i);
                    //add mushroom
                    mushrooms.addMushroomSpecificLoc(segment.x, segment.y);
                }

                prevSegment = segment;
            }

            //removes 
            int removedLocations = 0;
            for (int i = 0; i < segmentsToRemove.Count; i++)
            {

                segments.RemoveAt(segmentsToRemove[i] - removedLocations);
                removedLocations++;
                
            }
        }

        public void bulletHitCheck() {
            List<int> bulletsToRemove = new List<int>();
            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet b =  bullets[i];

                if (b.hit)
                {
                    bulletsToRemove.Add(i);
                }
            }

            //removes 
            int removedLocations = 0;
            for (int i = 0; i < bulletsToRemove.Count; i++)
            {
                bullets.RemoveAt(bulletsToRemove[i] - removedLocations);
                removedLocations++;

            }
        }

        public void fleaHItCheck() {
            if (oh.f != null) {
                if (oh.f.hit) {
                    score += 200;
                      oh.f = null;
                }
            }
        }

        public void scorpHItCheck()
        {
            if (oh.s != null)
            {
                if (oh.s.hit)
                {
                    score += 1000;
                    oh.s = null;
                }
            }
        }

        public void spiderHItCheck()
        {
            if (oh.sp != null)
            {
                if (oh.sp.hit)
                {
                    if (oh.sp.y > ((screenResolution.Y / 2) + ((1 / 3) * (screenResolution.Y / 2))))
                    {
                        score += 900;
                    }
                    else if (oh.sp.y > ((screenResolution.Y / 2) + ((2 / 3) * (screenResolution.Y / 2))))
                    {
                        score += 600;
                    }
                    else {
                        score += 300;
                    }

                    oh.sp = null;
                }
            }
        }


        public void mushroomHitCheck() {
            List<int> mushroomsToRemove = new List<int>();
            for (int i = 0; i < mushrooms.mushrooms.Count; i++)
            {
                Mushroom m = mushrooms.mushrooms[i];

                if (m.hit) { 
                    m.hit = false;
                        score += 4;
                }

                if (m.lives <= 0)
                {
                    mushroomsToRemove.Add(i);
                }
            }

            //removes 
            int removedLocations = 0;
            for (int i = 0; i < mushroomsToRemove.Count; i++)
            {
                mushrooms.mushrooms.RemoveAt(mushroomsToRemove[i] - removedLocations);
                removedLocations++;

            }
        }

        public void moveLeft(GameTime gameTime, float scale)
        {
            player.moveLeft(gameTime);
            onInput();
        }
        public void moveRight(GameTime gameTime, float scale)
        {
            player.moveRight(gameTime);
            onInput();
        }
        public void moveUp(GameTime gameTime, float scale)
        {
            player.moveUp(gameTime);
            onInput();
        }
        public void moveDown(GameTime gameTime, float scale)
        {
            player.moveDown(gameTime);
            onInput();
        }
        public void fire(GameTime gameTime, float scale)
        {
            addBullet();
            onInput();
        }

        private void addBullet() {
            if (bullets.Count < 3)
            {
                int bulletHeight = 10;
                int bulletWidth = 5;

                Bullet b = new Bullet();
                b.initialize((player.x + (player.width / 2)), player.y - bulletHeight, bulletWidth, bulletHeight);
                bullets.Add(b);
            }
        }

        public void onInput() {
            paused = false;
        }
    }
}