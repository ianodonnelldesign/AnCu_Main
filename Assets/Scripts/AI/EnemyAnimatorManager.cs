using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyAnimatorManager : CharacterAnimatorManager
    {
        EnemyManager enemyCharacter;
        EnemyBossManager bossManager;

        protected override void Awake()
        {
            base.Awake();
            enemyCharacter = GetComponent<EnemyManager>();
        }

        public void AwardSoulsOnDeath()
        {
            //PlayerStatsManager playerStats = FindObjectOfType<PlayerStatsManager>();
            //SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();

            //if (playerStats != null)
            //{
            //    playerStats.AddSouls(aiCharacter.aiCharacterStatsManager.soulsAwardedOnDeath);

            //    if (soulCountBar != null)
            //    {
            //        soulCountBar.SetSoulCountText(playerStats.currentSoulCount);
            //    }
            //}
        }

        public void InstantiateBossParticleFX()
        {
            BossFXTransform bossFxTransform = GetComponentInChildren<BossFXTransform>();
            GameObject phaseFX = Instantiate(bossManager.particleFX, bossFxTransform.transform);
        }

        public void PlayWeaponTrailFX()
        {
            //enemyCharacter.aiCharacterEffectsManager.PlayWeaponFX(false);
        }

        public override void OnAnimatorMove()
        {
            if (character.isInteracting == false)
                return;

            Vector3 velocity = character.animator.deltaPosition;
            character.characterController.Move(velocity);

            if (enemyCharacter.isRotatingWithRootMotion)
            {
                character.transform.rotation *= character.animator.deltaRotation;
            }
        }
    }
}