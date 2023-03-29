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
        DeathManager deathManager;
        public GameObject player;
        public GameObject respawnPoint;

        private void Awake()
        {
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
    }
}