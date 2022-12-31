using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager characterManager;
        Collider damageCollider;
        public bool enabledDamageColliderOnStartUp = false;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseBonus;

        [Header("Damage")]
        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = enabledDamageColliderOnStartUp;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                PlayerStatsManager playerStats = collision.GetComponent<PlayerStatsManager>();
                CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
                BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();

                if (enemyCharacterManager != null)
                {
                    if (enemyCharacterManager.isParrying)
                    {
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (shield != null && enemyCharacterManager.isBlocking)
                    {
                        float physicalDamageAfterBlock =
                            currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;

                        if (playerStats != null)
                        {
                            playerStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                            return;
                        }
                    }
                }

                if (playerStats != null)
                {
                    playerStats.poiseResetTimer = playerStats.totalPoiseResetTime;
                    playerStats.totalPoiseDefence = playerStats.totalPoiseDefence - poiseBreak;

                    if (playerStats.totalPoiseDefence > poiseBreak)
                    {
                        playerStats.TakeDamageNoAnimation(currentWeaponDamage);
                        Debug.Log("Enemy Poise is currently " + playerStats.totalPoiseDefence);
                    }
                    else
                    {
                        playerStats.TakeDamage(currentWeaponDamage);
                    }
                }
            }

            if (collision.tag == "Enemy")
            {
                EnemyStatsManager enemyStats = collision.GetComponent<EnemyStatsManager>();
                CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
                BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();

                if (enemyCharacterManager != null)
                {
                    if (enemyCharacterManager.isParrying)
                    {
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (shield != null && enemyCharacterManager.isBlocking)
                    {
                        float physicalDamageAfterBlock =
                            currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;

                        if (enemyStats != null)
                        {
                            enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Block Guard");
                            return;
                        }
                    }
                }

                if (enemyStats != null)
                {

                    enemyStats.poiseResetTimer = enemyStats.totalPoiseResetTime;
                    enemyStats.totalPoiseDefence = enemyStats.totalPoiseDefence - poiseBreak;

                    if (enemyStats.totalPoiseDefence > poiseBreak)
                    {
                        enemyStats.TakeDamageNoAnimation(currentWeaponDamage);
                        Debug.Log("Enemy Poise is currently " + enemyStats.totalPoiseDefence);
                    }
                    else
                    {
                        enemyStats.TakeDamage(currentWeaponDamage);
                    }
                }
            }
        }
    }
}