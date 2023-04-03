using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG

{
    public class DeathManager : MonoBehaviour
    {
        public Animator deathAnimationHandler;
        public GameObject deathScreen;

        PlayerAnimatorManager playerAnimatorManager;
        InputHandler inputHandler;
        MouseLook mouseLook;

        UIBossHealthBar bossHealthBar;

        private void Start()
        {
            playerAnimatorManager = FindObjectOfType<PlayerAnimatorManager>();
            inputHandler = GetComponent<InputHandler>();
            mouseLook = FindObjectOfType<MouseLook>();
            deathScreen.SetActive(false);
            bossHealthBar = FindObjectOfType<UIBossHealthBar>();
        }
        public void PlayerDeath()
        {
            deathScreen.SetActive(true);
            mouseLook.TurnMouseOn();

            inputHandler.interactFlag = true;
            deathAnimationHandler.SetTrigger("PlayerIsDead");

            bossHealthBar.SetHealthBarToInactive();
        }

        public void Respawn()
        {
            mouseLook.TurnMouseOff();

            playerAnimatorManager.animator.SetTrigger("Respawn");

            inputHandler.interactFlag = false;

            deathAnimationHandler.SetTrigger("ButtonClicked");
            deathAnimationHandler.ResetTrigger("PlayerIsDead");
        }

    }
}

