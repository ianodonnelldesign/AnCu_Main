using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG

{
    public class DeathManager : MonoBehaviour
    {
        public Animator deathAnimationHandler;
        public Animator bossAnimationHandler;
        public GameObject deathScreen;

        InputHandler inputHandler;
        MouseLook mouseLook;

        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            mouseLook = FindObjectOfType<MouseLook>();
        }
        public void PlayerDeath()
        {
            deathScreen.SetActive(true);
            mouseLook.TurnMouseOn();

            inputHandler.interactFlag = true;
            deathAnimationHandler.SetTrigger("PlayerIsDead");
        }

        public void Respawn()
        {
            deathScreen.SetActive(false);
            mouseLook.TurnMouseOff();

            inputHandler.interactFlag = false;
            deathAnimationHandler.SetTrigger("ButtonClicked");
        }

    }
}

