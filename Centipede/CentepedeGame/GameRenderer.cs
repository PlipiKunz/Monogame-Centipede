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

            m_objectRenderer.loadContent(contentManager);
        }

        public void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            m_objectRenderer.renderRect(screen, Color.YellowGreen);

            renderPlayer(gameTime);

            renderCentepede(gameTime);

            renderMushrooms(gameTime);

            renderUI();
            m_spriteBatch.End();
        }

        public void renderPlayer(GameTime gameTime) {
            m_objectRenderer.renderObject(gameTime,m_model.player,Color.White);
        }

        public void renderMushrooms(GameTime gameTime) {
            foreach (Mushroom m in m_model.mushrooms.mushrooms) {
                renderMushroom(gameTime, m);
            }
        }

        public void renderMushroom(GameTime gameTime, Mushroom m)
        {
            m_objectRenderer.renderObject(gameTime, m, Color.Yellow);
        }

        public void renderCentepede(GameTime gameTime) {
            foreach (CentepedeSegment segment in m_model.segments) {
                renderCentepedeSegment(gameTime, segment);
            }
        }
        public void renderCentepedeSegment(GameTime gameTime, CentepedeSegment s)
        {
            m_objectRenderer.renderObject(gameTime, s, s.segmentType == SegmentType.Head ? Color.Red: Color.Green);
        }

        public void renderUI() { 
            int lives = m_model.player.lives;

            int x_pos = 0;
            int y_pos = 0;
            for (int i = 0; i < lives; i++)
            {
                m_objectRenderer.renderRect(new Rectangle(x_pos, y_pos, (int)(m_model.standardWidth*.75), (int)(m_model.standardHeight*.75)), Color.Blue);
                x_pos += (int)(m_model.standardWidth);
            }

        }
    }
}
