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

        public void MainMenu()
        {
            SceneManager.LoadScene(mainMenu);
            deathAnimationHandler.SetTrigger("ButtonClicked");
            gameObject.SetActive(false);
        }

        public void Continue()
        {
            SceneManager.LoadScene(continueFromSave);
            deathAnimationHandler.SetTrigger("ButtonClicked");
            gameObject.SetActive(false);
            //will send player back to their previous save once saving works.
            //for now since the only place you can die is the boss, I guess send them back to the forest with the scene changer.
        }
    }
}


