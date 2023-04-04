using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace SG
{
    public class EnemyStatsManager : CharacterStatsManager
    {
        AICharacterAnimatorManager enemyAnimatorManager;
        EnemyBossManager enemyBossManager;
        public UIEnemyHealthBar enemyHealthBar;
        EnemyManager enemyManager;

        PlayerManager playerManager;

        public bool isBoss;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            enemyAnimatorManager = GetComponent<AICharacterAnimatorManager>();
            enemyManager = GetComponent<EnemyManager>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        void Start()
        {
            if (!isBoss)
            {
                enemyHealthBar.SetMaxHealth(maxHealth);
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public override void TakeDamageNoAnimation(int damage)
        {
            base.TakeDamageNoAnimation(damage);

            if (!isBoss)
            {

                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
                
            }
        }

        public void BreakGuard()
        {
            enemyAnimatorManager.PlayTargetAnimation("Break Guard", true);
        }

        public override void TakeDamage(int damage, string damageAnimation = "Damage_01")
        {

            base.TakeDamage(damage, damageAnimation = "Damage_01");

            if (!isBoss)
            {
                enemyHealthBar.SetHealth(currentHealth);
            }
            else if (isBoss && enemyBossManager != null)
            {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0)
            {
                HandleDeath();
                //if(isDead)
                //{
                //    return;
                //}
                //else
                //{
                //    HandleDeath();
                //}
            }
        }

        private void HandleDeath()
        {
            Debug.Log("Enemy died, and death is being handled");
            currentHealth = 0;
            enemyAnimatorManager.PlayTargetAnimation("Dead_01", true);
            isDead = true;
            
            if (isBoss && enemyBossManager != null)
            {
                enemyBossManager.HandleBossDeath();
            }

        }
    }
}