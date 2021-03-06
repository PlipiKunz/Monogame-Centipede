using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using CS5410.Input;
using CS5410.Persistence;

namespace CS5410
{
    public class ControlsView : MenuView
    {
        new public enum MenuState
        {
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown,
            Fire,
            Return
        }

        //tracks binding state
        public KeyboardActions? curBindingKey = null;
        public int timeSinceBind = 0;
        private int bindBufferTimeMS = 500;

        public override void loadContent(ContentManager contentManager)
        {
            m_Color = Color.Blue;
            m_selectedColor = Color.Yellow;
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {

            //if no key has been selected to be bound
            if (curBindingKey == null)
            {
                base.processInput(gameTime);
                if (!m_waitForKeyRelease && Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    //make sure this gets set so that menu navigation doesnt wig out
                    m_waitForKeyRelease = true;

                    if (m_currentSelection == (int)MenuState.MoveLeft)
                    {
                        curBindingKey = KeyboardActions.Left;
                        timeSinceBind = bindBufferTimeMS;
                    }
                    else if (m_currentSelection == (int)MenuState.MoveRight)
                    {
                        curBindingKey = KeyboardActions.Right;
                        timeSinceBind = bindBufferTimeMS;
                    }
                    else if (m_currentSelection == (int)MenuState.MoveUp)
                    {
                        curBindingKey = KeyboardActions.Up;
                        timeSinceBind = bindBufferTimeMS;
                    }
                    else if (m_currentSelection == (int)MenuState.MoveDown)
                    {
                        curBindingKey = KeyboardActions.Down;
                        timeSinceBind = bindBufferTimeMS;
                    }
                    else if (m_currentSelection == (int)MenuState.Fire)
                    {
                        curBindingKey = KeyboardActions.Fire;
                        timeSinceBind = bindBufferTimeMS;
                    }
                    else if (m_currentSelection == (int)MenuState.Return)
                    {
                        return GameStateEnum.MainMenu;
                    }
                }
            }

            //handles the control rebinding
            else
            {
                //pressing escape backs out of binding sub menu
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    curBindingKey = null;
                }
                else
                {
                    if (timeSinceBind == 0)
                    {
                        Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
                        if (keysPressed.Length != 0)
                        {
                            rebind((KeyboardActions)curBindingKey, keysPressed[0]);
                            curBindingKey = null;
                        }
                    }

                }

            }
            return GameStateEnum.Controls;
        }
        public override void update(GameTime gameTime)
        {
            //updates binding time
            if (timeSinceBind > 0)
            {
                timeSinceBind -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (timeSinceBind < 0)
            {
                timeSinceBind = 0;
            }
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            if (curBindingKey == null)
            {
                // I split the first one's parameters on separate lines to help you see them better

                float bottom = drawMenuItem(m_fontMenu, "Controls Menu", 200, Color.Red);
                bottom = drawMenuItem(m_fontMenu, "Select a Control to rebind InGame by pressing Enter", bottom, Color.Red);
                bottom = drawMenuItem(m_fontMenu, "(will not rebind menu controls)", bottom, Color.Red);
                bottom += 25;

                bottom = drawSelectedControlMenuItem("Move Left", KeyboardPersistence.actionToKey[KeyboardActions.Left], bottom, m_currentSelection == (int)MenuState.MoveLeft);
                bottom = drawSelectedControlMenuItem("Move Right", KeyboardPersistence.actionToKey[KeyboardActions.Right], bottom, m_currentSelection == (int)MenuState.MoveRight);
                bottom = drawSelectedControlMenuItem("Move Up", KeyboardPersistence.actionToKey[KeyboardActions.Up], bottom, m_currentSelection == (int)MenuState.MoveUp);
                bottom = drawSelectedControlMenuItem("Move Down", KeyboardPersistence.actionToKey[KeyboardActions.Down], bottom, m_currentSelection == (int)MenuState.MoveDown);
                bottom = drawSelectedControlMenuItem("Fire", KeyboardPersistence.actionToKey[KeyboardActions.Fire], bottom, m_currentSelection == (int)MenuState.Fire);
                
                bottom = drawSelectedMenuItem("Back", bottom, m_currentSelection == (int)MenuState.Return);
            }
            else
            {
                float bottom = drawSelectedMenuItem("Press a key to bind to the " + curBindingKey + " action", 200, false);
            }

            m_spriteBatch.End();
        }

        protected float drawSelectedControlMenuItem(string actionText, Keys curButton, float y, bool selected)
        {
            return drawSelectedMenuItem(actionText + ": " + curButton, y, selected);
        }

        protected override int getMinMenuOption()
        {
            return (int)MenuState.MoveLeft;
        }

        protected override int getMaxMenuOption()
        {
            return (int)MenuState.Return;
        }

        private void rebind(KeyboardActions ka, Keys k)
        {
            KeyboardPersistence.bind(ka, k);
        }
    }
}
