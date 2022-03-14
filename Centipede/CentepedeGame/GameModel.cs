
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CS5410.CentepedeGame.Game_Objects;

namespace CS5410.CentepedeGame
{
    public class GameModel
    {

        public string message;
        public bool isDone;
        public Player player;

        Vector2 screenResolution;

        public void initialize(Vector2 resolution)
        {
            screenResolution = resolution;
            player = new Player();
            reset();
        }

        public void reset()
        {
            float widthResolutionScaler =  screenResolution.X;
            float heightResolutionScaler = screenResolution.Y;

            int standardWidth = (int)(.1f * widthResolutionScaler);
            int standardHeight = (int)(.1f * heightResolutionScaler);

            player.initialize((int)(widthResolutionScaler * .5f), (int)(heightResolutionScaler * .5f),standardWidth, standardHeight);
            message = "Isn't this game fun!";
            isDone = false;
        }

        public void update(GameTime gameTime)
        {
        }
        public void moveLeft(GameTime gameTime, float scale)
        {
            message = "left";
            player.moveLeft(gameTime);

        }
        public void moveRight(GameTime gameTime, float scale)
        {
            message = "right";
            player.moveRight(gameTime);
        }
        public void moveUp(GameTime gameTime, float scale)
        {
            message = "up";
            player.moveUp(gameTime);
        }
        public void moveDown(GameTime gameTime, float scale)
        {
            message = "down";
            player.moveDown(gameTime);
        }
        public void fire(GameTime gameTime, float scale)
        {
            isDone = true;
        }
    }
}
