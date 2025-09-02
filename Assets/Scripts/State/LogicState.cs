namespace SurviveStayAlive
{
    public enum LogicStateEnum
    {
        PlayState,
        PauseState,
        WinState,
        LoseState,
    }

    public class LogicState
    {
        public LogicStateEnum CurrentLogicState { get; private set; }

        public LogicState(LogicStateEnum logicStateValue)
        {
            ChangeState(logicStateValue);
        }

        public void ChangeState(LogicStateEnum logicStateValue)
        {
            CurrentLogicState = logicStateValue;

            if (logicStateValue == LogicStateEnum.WinState) {
                // TODO
            }
        }

        public void PauseUnPauseGame()
        {
            if (CurrentLogicState == LogicStateEnum.PlayState) {
                ChangeState(LogicStateEnum.PauseState);
            } else if (CurrentLogicState == LogicStateEnum.PauseState) {
                ChangeState(LogicStateEnum.PlayState);
            }
        }
    }
}
