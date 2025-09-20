using UnityEngine;

namespace Enemies
{
    public interface IEnemyMovable
    {
        int Speed { get; }

        void Move(Transform transform);
        void Move(Transform transform, Vector3 moveTo);
    }
}
