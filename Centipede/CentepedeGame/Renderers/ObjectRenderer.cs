using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CS5410.CentepedeGame.ObjectsInGame;

namespace CS5410.CentepedeGame.Renderers
{
    public  class ObjectRenderer: renderer
    {
        private Texture2D boxTexture;
        private Texture2D spriteSheet;

        private int ssw = 16;
        private int ssh = 8;

        private int spriteIndex = 0;
        private float timeTillNextFrameUsual = 100;
        private float curTimeTillNextFrame;

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            base.initialize(graphicsDevice, graphics);
            curTimeTillNextFrame = timeTillNextFrameUsual;
        }

        public override void loadContent(ContentManager contentManager) {
            boxTexture = contentManager.Load<Texture2D>("Sprites/SquareSprite");
            spriteSheet = contentManager.Load<Texture2D>("Sprites/spriteSheet");
        }

        public void Update(GameTime g)
        {
            curTimeTillNextFrame -= (float)g.ElapsedGameTime.TotalMilliseconds;
            if (curTimeTillNextFrame < 0) {
                curTimeTillNextFrame = timeTillNextFrameUsual;
                spriteIndex++;
                spriteIndex %= 8;
            }
        }

        public void renderObject(ObjectInGame item, Color c) {
            startDraw();
            m_spriteBatch.Draw(boxTexture, item.getBoundingBox(), c);
            stopDraw();
        }

        public void renderPlayer(Player item)
        {
            startDraw();
            m_spriteBatch.Draw(spriteSheet, item.getBoundingBox(), getSpriteSheetLoc(0,1), Color.White);
            stopDraw();
        }

        public void renderPlayerSprite(Rectangle r)
        {
            startDraw();
            m_spriteBatch.Draw(spriteSheet, r, getSpriteSheetLoc(0, 1), Color.White);
            stopDraw();
        }

        public void renderMushroom( Mushroom item)
        {
            startDraw();

            int mStartX = 67;
            int mStartY = 72;
            int mW = 9;
            int mH = 8;

            int curX = mStartX;
            int curY = mStartY;

            for (int i = 0; i < 4 - item.lives; i++)
            {
                curX += mW;
            }
            if (item.type == mushroomType.poison)
            {
                curY += mH + 1;
            }

            m_spriteBatch.Draw(spriteSheet, item.getBoundingBox(), new Rectangle(curX, curY, mW, mH), Color.White);

            stopDraw();
        }

        public void renderSpider(Spider item)
        {
            startDraw();
            m_spriteBatch.Draw(spriteSheet, item.getBoundingBox(), getSpriteSheetLoc(0 + spriteIndex, 6), Color.White);
            stopDraw();
        }

        public void renderFlea(Flea item)
        {
            startDraw();
            m_spriteBatch.Draw(spriteSheet, item.getBoundingBox(), getSpriteSheetLoc(0 + spriteIndex%4, 7), Color.White);
            stopDraw();
        }

        public void renderScorpion( Scorpion item)
        {
            startDraw();
            m_spriteBatch.Draw(spriteSheet, item.getBoundingBox(), getSpriteSheetLoc(0 + spriteIndex%4, 8), Color.White);
            stopDraw();
        }


        public void renderSegment(CentepedeSegment item)
        {
            startDraw();

            SpriteEffects s = SpriteEffects.None;
            if (item.xDirection > 0) {
                s = SpriteEffects.FlipHorizontally;
            }
            


            if (item.segmentType == SegmentType.Head)
            {
                m_spriteBatch.Draw(spriteSheet, item.getBoundingBox(), getSpriteSheetLoc(0 + spriteIndex, 2), Color.White);
            }
            else
            {
                m_spriteBatch.Draw(spriteSheet, item.getBoundingBox(), getSpriteSheetLoc(0 + spriteIndex, 4), Color.White);
            }

            stopDraw();
        }


        public Rectangle getSpriteSheetLoc(int x, int y)
        {
            int x_offset = 0;
            int y_offset = 0;

            if (x != 0)
            {
                x_offset += x;
            }
            if (y != 0)
            {
                y_offset += y;
            }

            return new Rectangle((x * ssw) + x_offset, (y * ssh) + y_offset, ssw, ssh);
        }

        public void renderRect( Rectangle r, Color c)
        {
            startDraw();
            m_spriteBatch.Draw(boxTexture, r, c);
            stopDraw();
        }
    }
}
