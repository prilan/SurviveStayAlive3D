using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SurviveStayAlive
{
    public class ControlController : MonoBehaviour
    {


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                AppModel.Instance.LogicState.PauseUnPauseGame();
            }

            if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PauseState)
                return;

            if (Input.GetKeyDown(KeyCode.Tab)) {
                PlayersManager.Instance.SetNextCurrentPlayer();
            }
        }
    }
}
