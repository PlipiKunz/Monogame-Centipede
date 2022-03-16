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
        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            base.initialize(graphicsDevice, graphics);
        }

        public override void loadContent(ContentManager contentManager) {
            boxTexture = contentManager.Load<Texture2D>("Sprites/SquareSprite");
        }

        public void renderObject(GameTime gameTime, ObjectInGame item, Color c) {
            startDraw();
            m_spriteBatch.Draw(boxTexture, item.getBoundingBox(), c);
            stopDraw();
        }
        public void renderRect( Rectangle r, Color c)
        {
            startDraw();
            m_spriteBatch.Draw(boxTexture, r, c);
            stopDraw();
        }
    }
}
