using UnityEngine;

namespace Enemies.Interfaces
{
    public interface IEnemyMovable
    {
        int Speed { get; }

        void Move(Transform transform);
        void Move(Transform transform, Vector3 moveTo);
    }
}
