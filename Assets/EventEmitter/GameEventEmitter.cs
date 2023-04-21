using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace EventEmitter
{
    public class GameEventEmitter : Singleton<GameEventEmitter>
    {
        private event Action playersInited;

        /*****************************************************************************************/

        public static void OnPlayersInited()
        {
            Instance.playersInited();
        }

        /*****************************************************************************************/

        public static event Action PlayersInited
        {
            add { Instance.playersInited += value; }
            remove { Instance.playersInited -= value; }
        }
    }
}
