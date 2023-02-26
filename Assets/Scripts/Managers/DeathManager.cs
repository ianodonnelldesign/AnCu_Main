using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG

{
    public class DeathManager : MonoBehaviour
    {
        public Animator deathAnimationHandler;
        public GameObject deathScreenButtons;

        InputHandler inputHandler;
        MouseLook mouseLook;

        private void Start()
        {
            inputHandler = FindObjectOfType<InputHandler>();
            mouseLook = FindObjectOfType<MouseLook>();
        }
        public void PlayerDeath()
        {
            mouseLook.TurnMouseOn();
            inputHandler.interactFlag = true;
            deathAnimationHandler.SetTrigger("PlayerIsDead");
            Debug.Log("Setanta Died.");
        }

        public void EnableDeathScreenButtons()
        {
            deathScreenButtons.SetActive(true);
        }

        public void ChowsaintDeath()
        {
            Debug.Log("Chowsaint was killed");
        }

    }
}

