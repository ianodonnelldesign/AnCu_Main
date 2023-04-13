using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WorldEventManager : MonoBehaviour
    {
        //Fog Wall
        public UIBossHealthBar bossHealthBar;
        public EnemyBossManager boss;

        public bool bossFightIsActive;  //Is currently fighting boss
        public bool bossHasBeenAwakened; //Woke the boss/watched cutscene but died during fight
        public bool bossHasBeenDefeated; //Boss has been defeated

        private void Awake()
        {
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        }

        public void ActivateBossFight()
        {
            bossFightIsActive = true;
            bossHasBeenAwakened = true;

            //play the animation for the bossfight here instead
            //play the wake up animation
            bossHealthBar.SetUIHealthBarToActive();

            //turn on the timeline

        }

        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            bossFightIsActive = false;

            //Deactivate Fog Walls
        }
    }
}
