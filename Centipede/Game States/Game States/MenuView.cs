﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public abstract class MenuView : GameStateView
    {
        protected int m_currentSelection = 0;

        //set key wait corerctly for navigating correctly for nested menus, so selection in a parent menu doesnt cascade
        static protected bool m_waitForKeyRelease = false;


        protected SpriteFont m_fontMenu;
        protected SpriteFont m_fontMenuSelect;

        protected Color m_Color;
        protected Color m_selectedColor;


        public override GameStateEnum processInput(GameTime gameTime)
        {
            // This is the technique I'm using to ensure one keypress makes one menu navigation move
            if (!m_waitForKeyRelease)
            {
                // Arrow keys to navigate the menu
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        m_currentSelection = m_currentSelection + 1;
                        m_waitForKeyRelease = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        m_currentSelection = m_currentSelection - 1;
                        m_waitForKeyRelease = true;
                    }

                    if (m_currentSelection < getMinMenuOption())
                    {
                        m_currentSelection = getMinMenuOption();
                    }
                    else if (m_currentSelection > getMaxMenuOption()) {
                        m_currentSelection = getMaxMenuOption();
                    }
                }

            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                m_waitForKeyRelease = false;
            }

            return GameStateEnum.MainMenu;
        }


        protected abstract int getMinMenuOption();
        protected abstract int getMaxMenuOption();

        protected float drawSelectedMenuItem(string text, float y,bool selected) { 
        
            return drawMenuItem(selected ? m_fontMenuSelect : m_fontMenu,text, y,selected ? m_selectedColor: m_Color);
        }

        protected float drawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            m_spriteBatch.DrawString(
                font,
                text,
                new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                color);

            return y + stringSize.Y;
        }
    }
}