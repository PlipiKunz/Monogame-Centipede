using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CS5410.CentepedeGame.Renderers;
using CS5410.CentepedeGame.ObjectsInGame;

namespace CS5410.CentepedeGame
{
    public class GameRenderer
    {
        protected GraphicsDeviceManager m_graphics;
        protected SpriteBatch m_spriteBatch;
        protected GameModel m_model;

        private Rectangle screen;

        private SpriteFont m_font;

        private ObjectRenderer m_objectRenderer;

        public virtual void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameModel gameModel)
        {
            m_graphics = graphics;
            m_spriteBatch = new SpriteBatch(graphicsDevice);
            m_model = gameModel;

            screen = new Rectangle(0, 0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight);

            m_objectRenderer = new ObjectRenderer();
            m_objectRenderer.initialize(graphicsDevice, graphics);
        }

        public void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");

            m_model.loadContent(contentManager);
            m_objectRenderer.loadContent(contentManager);
        }

        public void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            m_objectRenderer.Update(gameTime);

            m_objectRenderer.renderRect(screen, Color.CornflowerBlue);

            renderPlayer(gameTime);

            renderCentepede(gameTime);

            renderMushrooms(gameTime);

            renderBullets(gameTime);

            renderOptionals(gameTime);

            renderUI();
            m_spriteBatch.End();
        }

        public void renderPlayer(GameTime gameTime) {
            m_objectRenderer.renderPlayer(m_model.player);
        }
        public void renderMushrooms(GameTime gameTime) {
            foreach (Mushroom m in m_model.mushrooms.mushrooms) {
                renderMushroom(gameTime, m);
            }
        }



        public void renderOptionals(GameTime gameTime)
        {
            if (m_model.oh.f != null) {
                renderFlea(gameTime, m_model.oh.f);
            }
            if (m_model.oh.s != null)
            {
                renderScorpion(gameTime, m_model.oh.s);
            }
            if (m_model.oh.sp != null) { 
                renderSpider(gameTime, m_model.oh.sp);
            }

        }

        public void renderFlea(GameTime gameTime, Flea f) {
            m_objectRenderer.renderFlea( f);
        }
        public void renderScorpion(GameTime gameTime, Scorpion s)
        {
            m_objectRenderer.renderScorpion( s);
        }
        public void renderSpider(GameTime gameTime, Spider s)
        {
            m_objectRenderer.renderSpider(s);
        }

        public void renderBullets(GameTime gameTime)
        {
            foreach (Bullet b in m_model.bullets)
            {
                renderBullet(gameTime, b);
            }
        }

        public void renderBullet(GameTime gameTime, Bullet b)
        {
            m_objectRenderer.renderObject( b, Color.AliceBlue);
        }

        public void renderMushroom(GameTime gameTime, Mushroom m)
        {
            m_objectRenderer.renderMushroom( m);
        }

        public void renderCentepede(GameTime gameTime) {
            foreach (CentepedeSegment segment in m_model.segments) {
                renderCentepedeSegment(gameTime, segment);
            }
        }
        public void renderCentepedeSegment(GameTime gameTime, CentepedeSegment s)
        {
            m_objectRenderer.renderSegment( s);
        }

        public void renderUI() { 
            int lives = m_model.player.lives;

            int x_pos = 0;
            int y_pos = 0;
            for (int i = 0; i < lives; i++)
            {
                m_objectRenderer.renderPlayerSprite(new Rectangle(x_pos, y_pos, (int)(m_model.standardWidth*.75), (int)(m_model.standardHeight*.75)));
                x_pos += (int)(m_model.standardWidth);
            }

            String text = "Score: " + m_model.score;
            Vector2 stringSize = m_font.MeasureString(text);
            Vector2 pos =  new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, 0 + stringSize.Y / 2);
            fancyDraw(text, pos);
        }

        public void fancyDraw(String text, Vector2 pos)
        {

            Vector2 newPos = pos;

            newPos.X -= 1;
            newPos.Y -= 1;

            SimpleDraw(
                    text,
                    newPos,
                    Color.White);



            newPos = pos;

            newPos.X += 1;
            newPos.Y += 1;
            SimpleDraw(
                    text,
                    newPos,
                    Color.White);




            SimpleDraw(
                    text,
                    pos,
                    Color.Black);
        }

        public void SimpleDraw(String text, Vector2 pos, Color c)
        {
            
            m_spriteBatch.DrawString(
                    m_font,
                    text,
                    pos,
                    
                    c);
        }
}

}
