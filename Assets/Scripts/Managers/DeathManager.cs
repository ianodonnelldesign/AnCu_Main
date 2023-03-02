using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG

{
    public class DeathManager : MonoBehaviour
    {
        public Animator deathAnimationHandler;
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
            Debug.Log("Setanta Died.");
        }

        public void ChowsaintDeath()
        {
            Debug.Log("Chowsaint was killed");
        }

    }
}

