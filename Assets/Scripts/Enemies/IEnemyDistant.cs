using Players;
using UnityEngine;

namespace Enemies
{
    public interface IEnemyDistant
    {
        int Distance { get; }

        void ActionWhenNear(Transform transform, PlayerController playerController);
    }
}
