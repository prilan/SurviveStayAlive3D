using Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemies
{
    public class Enemy
    {
        public int Damage;
        public int Speed;
        public int AttackDistance;

        public Color Color;
    
        public Enemy(EnemyFactory enemyFactory)
        {
            Damage = enemyFactory.Damage;
            Speed = enemyFactory.Speed;
            AttackDistance = enemyFactory.AttackDistance;

            Color = enemyFactory.Color;
        }
    }
}
