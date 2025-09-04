using UnityEngine;

namespace SurviveStayAlive
{
    public class PlayerSwitchController : MonoBehaviour
    {
        private void Update()
        {
            if (AppModel.Instance.LogicState.CurrentLogicState == LogicStateEnum.PauseState)
                return;

            if (Input.GetKeyDown(KeyCode.Tab)) {
                PlayersManager.Instance.SetNextCurrentPlayer();
            }
        }
    }
}
