using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CS5410.Input;
using CS5410.Persistence;
using CS5410.CentepedeGame;


namespace CS5410
{
    public class GamePlayView : GameStateView
    {

        public static bool keyboardUpToDate = false;

        private KeyboardInput m_keyboardInput;

        public static GameModel m_gameModel;
        private GameRenderer m_gameRenderer;

        public override void initialize(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            base.initialize(graphicsDevice, graphics);

            m_gameModel = new GameModel();
            m_gameModel.initialize(new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

            m_gameRenderer = new GameRenderer();
            m_gameRenderer.initialize(graphicsDevice, graphics, m_gameModel);

            m_keyboardInput = new KeyboardInput();
        }

        public override void loadContent(ContentManager contentManager)
        {

            m_gameRenderer.loadContent(contentManager);
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ScorePersistence.score = m_gameModel.score;
                return GameStateEnum.EndGameConfirm;
            }

            return checkIfDone();
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            m_gameRenderer.render(gameTime);

            m_spriteBatch.End();
        }

        public override void update(GameTime gameTime)
        {
            m_keyboardInput.Update(gameTime);
            updateKeyboardBindings();

            m_gameModel.update(gameTime);
        }

        private void updateKeyboardBindings() {
            if (!keyboardUpToDate) {
                m_keyboardInput = new KeyboardInput();
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Left], false, m_gameModel.moveLeft);
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Right], false,  m_gameModel.moveRight);
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Up], false,  m_gameModel.moveUp);
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Down], false,  m_gameModel.moveDown);
                m_keyboardInput.registerCommand(KeyboardPersistence.actionToKey[KeyboardActions.Fire], true, m_gameModel.fire);
                keyboardUpToDate = true;
            }
        }

        private GameStateEnum checkIfDone() {
            if (m_gameModel.isDone)
            {
                done();
                return GameStateEnum.GameFinished;
            }
            
            return GameStateEnum.GamePlay;
        }

        public static void done()
        {
            ScorePersistence.addScore(m_gameModel.score);

            m_gameModel.reset();
        }
    }
}
