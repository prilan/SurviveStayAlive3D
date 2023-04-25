using System;
using Unity.VisualScripting;
using Utility;

namespace EventEmitter
{
    public class GameEventEmitter : AbstractSingleton<GameEventEmitter>
    {
        private event Action playersInited = () => { };
        private event Action restartLevel = () => { };

        private event Action winLevel = () => { };
        private event Action loseLevel = () => { };

        /*****************************************************************************************/

        public static void OnPlayersInited()
        {
            Instance.playersInited();
        }

        public static void OnRestartLevel()
        {
            Instance.restartLevel();
        }

        public static void OnWinLevel()
        {
            Instance.winLevel();
        }

        public static void OnLoseLevel()
        {
            Instance.loseLevel();
        }

        /*****************************************************************************************/

        public static event Action PlayersInited
        {
            add { Instance.playersInited += value; }
            remove { Instance.playersInited -= value; }
        }

        public static event Action RestartLevel
        {
            add { Instance.restartLevel += value; }
            remove { Instance.restartLevel -= value; }
        }

        public static event Action WinLevel
        {
            add { Instance.winLevel += value; }
            remove { Instance.winLevel -= value; }
        }

        public static event Action LoseLevel
        {
            add { Instance.loseLevel += value; }
            remove { Instance.loseLevel -= value; }
        }
    }
}
