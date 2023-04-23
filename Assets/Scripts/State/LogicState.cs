using System.Collections;
using UnityEngine;

namespace SurviveStayAlive
{
    public enum LogicStateEnum
    {
        PlayState,
        WinState,
        PauseState,
    }

    public class LogicState
    {
        private LogicStateEnum currentLogicState;

        public LogicStateEnum CurrentLogicState
        {
            get { return currentLogicState; }
        }

        public LogicState(LogicStateEnum logicStateValue)
        {
            ChangeState(logicStateValue);
        }

        public void ChangeState(LogicStateEnum logicStateValue)
        {

            currentLogicState = logicStateValue;

            if (logicStateValue == LogicStateEnum.WinState) {
                // TODO
            }
        }

        public void PauseUnPauseGame()
        {
            if (currentLogicState == LogicStateEnum.PlayState) {
                ChangeState(LogicStateEnum.PauseState);
            } else if (currentLogicState == LogicStateEnum.PauseState) {
                ChangeState(LogicStateEnum.PlayState);
            }
        }
    }
}