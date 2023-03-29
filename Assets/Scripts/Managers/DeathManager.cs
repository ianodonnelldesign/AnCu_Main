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

        PlayerAnimatorManager playerAnimatorManager;
        InputHandler inputHandler;
        MouseLook mouseLook;

        private void Start()
        {
            playerAnimatorManager = FindObjectOfType<PlayerAnimatorManager>();
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
            mouseLook.TurnMouseOff();

            playerAnimatorManager.animator.SetTrigger("Respawn");

            inputHandler.interactFlag = false;
            deathAnimationHandler.SetTrigger("ButtonClicked");

            deathScreen.SetActive(false);
        }

    }
}

