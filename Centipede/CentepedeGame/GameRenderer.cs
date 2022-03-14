using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410.CentepedeGame
{
    public class GameRenderer
    {
        protected GraphicsDeviceManager m_graphics;
        protected SpriteBatch m_spriteBatch;
        protected GameModel m_model;

        private Rectangle screen;

        private SpriteFont m_font;

        public virtual void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, GameModel gameModel)
        {
            m_graphics = graphics;
            m_spriteBatch = new SpriteBatch(graphicsDevice);
            m_model = gameModel;

            screen = new Rectangle(0, 0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight);
        }

        public void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");
        }

        public void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            renderPlayer(gameTime);


            m_spriteBatch.End();
        }

        public void renderPlayer(GameTime gameTime) {
            Rectangle playerRect = m_model.player.getBoundingBox();
            Vector2 stringSize = m_font.MeasureString(m_model.message);
            m_spriteBatch.DrawString(
                m_font,
                m_model.message,
                new Vector2(playerRect.X, playerRect.Y),
                Color.Blue);
        }
    }
}
