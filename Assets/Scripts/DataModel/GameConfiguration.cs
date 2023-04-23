using UnityEngine;

namespace DataModel
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "SurviveStayAlive/Game Configuration", order = 0)]
    public class GameConfiguration : ScriptableObject
    {
        [Header("Configs")]

        public string ConfigPath;

    }
}
