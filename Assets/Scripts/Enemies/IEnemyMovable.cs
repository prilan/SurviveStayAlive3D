using System;
using UnityEngine;

namespace Enemies
{
    public interface IEnemyMovable
    {
        int Speed { get; }

        void Move(Transform transform);
    }
}
