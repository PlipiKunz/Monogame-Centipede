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

        private static Persist p = new Persist();

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
                if (Persist.m_loadedControls != null)
                {
                    actionToKey = Persist.m_loadedControls;
                    loaded = true;
                }
                else if (Persist.m_loadedControls == null && Persist.controlsExists == false)
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
