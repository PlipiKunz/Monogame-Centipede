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
    public abstract class renderer
    {

        protected GraphicsDeviceManager m_graphics;
        protected SpriteBatch m_spriteBatch;

        public virtual void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            m_graphics = graphics;
            m_spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public abstract void loadContent(ContentManager contentManager);

        protected void startDraw() {
            m_spriteBatch.Begin();
        }

        protected void stopDraw()
        {
            m_spriteBatch.End();
        }

    }
}
