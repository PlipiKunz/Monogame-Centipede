using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using CS5410.Input;

namespace CS5410.Persistence
{
    public static class KeyboardPersistence
    {

        private static PersistControls p = new PersistControls();

        public static Dictionary<KeyboardActions, Keys> actionToKey = new Dictionary<KeyboardActions, Keys>() {
            {KeyboardActions.Left, Keys.Left },
            {KeyboardActions.Right, Keys.Right },
            {KeyboardActions.Up, Keys.Up },
            {KeyboardActions.Down, Keys.Down },
            {KeyboardActions.Fire, Keys.Space}
        };

        public static KeyboardInput k = new KeyboardInput();

        public static Boolean loaded = false;

        public static void getPersistedActionToKey() {
            p.loadControls();
            while (!loaded)
            {
                if (PersistControls.m_loadedControls != null)
                {
                    actionToKey = PersistControls.m_loadedControls;
                    loaded = true;
                }
                else if (PersistControls.m_loadedControls == null && PersistControls.controlsExists == false)
                {
                    loaded = true;
                }
            }
            GamePlayView.keyboardUpToDate = false;
        }

        public static void persistActionToKey()
        {
            p.saveControls();
        }

        public static void bind(KeyboardActions ka, Keys k)
        {
            actionToKey[ka] = k;
            GamePlayView.keyboardUpToDate = false;
        }
    }
}
