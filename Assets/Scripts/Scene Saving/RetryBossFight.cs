using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace SG 
{
    public class RetryBossFight : MonoBehaviour
    {
        PlayerStatsManager playerStatsManager;
        HealthBar healthBar;
        StaminaBar staminaBar;

        public EnemyStatsManager chowsaintStats;
        UIBossHealthBar bossHealthBar;
        
        DeathManager deathManager;

        public GameObject deathScreen;
        public GameObject player;
        public GameObject respawnPoint;

        public GameObject boss;
        public GameObject bossRespawnPoint;

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            deathManager = FindObjectOfType<DeathManager>();
            playerStatsManager = FindObjectOfType<PlayerStatsManager>();
            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();

        }
        public void Retry()
        {
            ResetHealth();
            ResetPosition();
            ResetStamina();
            deathManager.Respawn();

            ResetBossHealth();
            ResetBossPosition();
        }

        public void ResetHealth()
        {
            deathManager.Respawn();

            playerStatsManager.isDead = false;

            playerStatsManager.currentHealth = playerStatsManager.maxHealth;
            healthBar.SetCurrentHealth(playerStatsManager.maxHealth);
        }

        public void ResetStamina()
        {
            playerStatsManager.currentStamina = playerStatsManager.maxStamina;
            staminaBar.SetCurrentStamina(playerStatsManager.maxStamina);
        }

        public void ResetPosition()
        {
            player.transform.position = respawnPoint.transform.position;
            player.transform.eulerAngles = respawnPoint.transform.eulerAngles;
        }

        public void ResetBossHealth()
        {
            chowsaintStats.isDead = false;

            chowsaintStats.currentHealth = playerStatsManager.maxHealth;
            bossHealthBar.SetBossCurrentHealth(chowsaintStats.maxHealth);
        }
        public void ResetBossPosition()
        {
            boss.transform.position = bossRespawnPoint.transform.position;
            boss.transform.eulerAngles = bossRespawnPoint.transform.eulerAngles;
        }
    }
}