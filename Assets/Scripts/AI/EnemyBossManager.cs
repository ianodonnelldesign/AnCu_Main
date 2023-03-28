using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace SG
{
    public class EnemyBossManager : CharacterManager
    {
        public string bossName;

        UIBossHealthBar bossHealthBar;
        EnemyStatsManager enemyStats;
        EnemyAnimatorManager enemyAnimatorManager;
        BossCombatStanceState bossCombatStanceState;

        public SceneField bossDeathScene;

        [Header("Second Phase FX")]
        public GameObject particleFX;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            enemyStats = GetComponent<EnemyStatsManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }

        public void UpdateBossHealthBar(int currentHealth, int maxHealth)
        {
            bossHealthBar.SetBossCurrentHealth(currentHealth);

            if (currentHealth <= maxHealth / 2)
            {
                ShiftToSecondPhase();
            }
        }

        public void ShiftToSecondPhase()
        {
            //Play an animation /w an event that triggers the boss's change
            //Switch attack actions to the new phase attacks

            enemyAnimatorManager.PlayTargetAnimation("Phase Shift", true);
            bossCombatStanceState.hasPhaseShifted = true;

        }
    }
}
