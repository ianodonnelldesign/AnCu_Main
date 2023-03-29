using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utilities;

namespace SG
{
    public class DeathScreenButtons : MonoBehaviour
    {
        public SceneField mainMenu;
        public SceneField continueFromSave;

        public Animator deathAnimationHandler;

        RetryBossFight retryBossFight;

        private void Awake()
        {
            retryBossFight = FindObjectOfType<RetryBossFight>();
        }

        public void MainMenu()
        {
            deathAnimationHandler.SetTrigger("ButtonClicked");

            SceneManager.LoadScene(mainMenu);

            gameObject.SetActive(false);
        }

        public void Retry()
        {
            deathAnimationHandler.SetTrigger("ButtonClicked");
            
            gameObject.SetActive(false);

            retryBossFight.Retry();
        }
    }
}


