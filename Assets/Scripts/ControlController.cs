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
        }
    }
}
