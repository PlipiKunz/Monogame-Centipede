
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

        float widthResolutionScaler;
        float heightResolutionScaler;
        int standardHeight;
        int standardWidth;

        public int score;
        //if the game is over
        public bool isDone;
        //pause is used when the player is hit, game resumes when any input is given
        public bool paused;

        public Player player;
        public List<CentepedeSegment> segments;

        public void initialize(Vector2 resolution)
        {
            screenResolution = resolution;
            player = new Player();
            segments = new List<CentepedeSegment>();
            reset();
        }

        public void reset()
        {
            widthResolutionScaler = screenResolution.X;
            heightResolutionScaler = screenResolution.Y;

            //scales things based on starting at 1980/1020
            standardWidth = (int)(50 * (1980 / widthResolutionScaler));
            standardHeight = (int)(50 * (1020 / heightResolutionScaler));

            int score = 0;
            isDone = false;
            paused = false;
            player.initialize((int)(widthResolutionScaler * .5f), (int)(heightResolutionScaler - standardHeight), standardWidth, standardHeight);

            resetScreen();
        }

        public void resetScreen()
        {
            player.resetPos();
            segments = CentepedeSegment.generateCentepede(0, 0, standardWidth, standardHeight, 12);
        }


        public void update(GameTime gameTime)
        {
            //if game hasnt been paused (after the player was hit)
            if (!paused)
            {
                //update game items
                player.update(gameTime);
                CentepedeSegment.update(gameTime, segments);


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
                    segment.split();
                    if (prevSegment != null)
                    {
                        prevSegment.nextSegment = null;
                    }
                    segmentsToRemove.Add(i);
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
            onInput();
        }

        public void onInput() {
            paused = false;
        }
    }
}