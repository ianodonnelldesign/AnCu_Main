using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DeathAnimationEventManager : MonoBehaviour
    {
        public GameObject deathScreenButtons;
        public Animator deathScreenAnimator;

        public void EnableDeathScreenButtons()
        {
            deathScreenButtons.SetActive(true);
        }

        public void FadeOutDeathScreen()
        {
            deathScreenAnimator.SetTrigger("ButtonClicked");
        }
    }
}

