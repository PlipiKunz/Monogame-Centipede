using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CS5410.Input;
using CS5410.Persistence;

namespace CS5410
{
    public class GamePlayView : GameStateView
    {
        private SpriteFont m_font;
        private string message = "Isn't this game fun!";

        public static bool keyboardUpToDate = false;
        private KeyboardInput m_keyboardInput;


        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            base.initialize(graphicsDevice, graphics);
            m_keyboardInput = new KeyboardInput();
        }

        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return GameStateEnum.MainMenu;
            }

            return GameStateEnum.GamePlay;
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            Vector2 stringSize = m_font.MeasureString(message);
            m_spriteBatch.DrawString(m_font, message,
                new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, m_graphics.PreferredBackBufferHeight / 2 - stringSize.Y), Color.Yellow);

            m_spriteBatch.End();
        }

        public override void update(GameTime gameTime)
        {
            m_keyboardInput.Update(gameTime);
            updateKeyboardBindings();
        }

        private void updateKeyboardBindings() {
            if (!keyboardUpToDate) {
                m_keyboardInput = new KeyboardInput();
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Left], false, moveLeft);
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Right], false, moveRight);
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Up], false, moveUp);
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Down], false, moveDown);
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Fire], true, fire);
                keyboardUpToDate = true;
            }
        }


        private void moveLeft(GameTime gameTime, float scale)
        {
            message = "left";
        }
        private void moveRight(GameTime gameTime, float scale)
        {
            message = "right";
        }
        private void moveUp(GameTime gameTime, float scale)
        {
            message = "up";
        }
        private void moveDown(GameTime gameTime, float scale)
        {
            message = "down";
        }
        private void fire(GameTime gameTime, float scale)
        {
            message = "fire";
        }
    }
}
