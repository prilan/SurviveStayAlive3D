using System;
using Unity.VisualScripting;
using Utility;

namespace EventEmitter
{
    public class GameEventEmitter : AbstractSingleton<GameEventEmitter>
    {
        private event Action playersInited = () => { };
        private event Action winLevel = () => { };

        /*****************************************************************************************/

        public static void OnPlayersInited()
        {
            Instance.playersInited();
        }

        public static void OnWinLevel()
        {
            Instance.winLevel();
        }

        /*****************************************************************************************/

        public static event Action PlayersInited
        {
            add { Instance.playersInited += value; }
            remove { Instance.playersInited -= value; }
        }

        public static event Action WinLevel
        {
            add { Instance.winLevel += value; }
            remove { Instance.winLevel -= value; }
        }
    }
}
