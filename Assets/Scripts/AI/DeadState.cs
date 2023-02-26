using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DeadState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (enemyManager.currentTarget != null)
            {
                enemyManager.isInteracting = true;
                enemyManager.currentTarget = null;
                enemyAnimatorManager.canRotate = false;
                return this;
            }
            else
            {
                enemyManager.isInteracting = true;
                enemyManager.currentTarget = null;
                enemyAnimatorManager.canRotate = false;
                return this;
            }
        }
    }

}