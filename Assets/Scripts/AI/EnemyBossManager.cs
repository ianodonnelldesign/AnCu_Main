using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.SceneManagement;

namespace SG
{
    public class EnemyBossManager : CharacterManager
    {
        public string bossName;

        public IEnumerator handleBossDeath;
        UIBossHealthBar bossHealthBar;
        EnemyStatsManager enemyStats;
        AICharacterAnimatorManager enemyAnimatorManager;
        BossCombatStanceState bossCombatStanceState;

        PlayerManager playerManager;

        public SceneField bossDeathScene;

        [Header("Second Phase FX")]
        public GameObject particleFX;

        protected override void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
            enemyStats = GetComponent<EnemyStatsManager>();
            enemyAnimatorManager = GetComponentInChildren<AICharacterAnimatorManager>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }

        private void Update()
        {
            if (AudioManager.Instance.musicSource.isPlaying == false)
            {
                SceneManager.LoadScene(bossDeathScene);
            }
            else if (AudioManager.Instance.musicSource.isPlaying)
            {

            }
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

        public void HandleBossDeath()
        {
            //playerManager.HandlePlayerInCutscene();
            AudioManager.Instance.FadeOutMusic("GuardLoop", 3f, 0f);
            AudioManager.Instance.PlayMusic("GuardLoopOut");
        }
    }
}
