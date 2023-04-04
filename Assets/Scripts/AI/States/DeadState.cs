using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DeadState : State
    {
        public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, AICharacterAnimatorManager enemyAnimatorManager)
        {
            if (enemyManager.currentTarget != null)
            {
                enemyManager.isInteracting = true;
                enemyManager.currentTarget = null;
                enemyManager.canRotate = false;
                return this;
            }
            else
            {
                enemyManager.isInteracting = true;
                enemyManager.currentTarget = null;
                enemyManager.canRotate = false;
                return this;
            }
        }
    }

}