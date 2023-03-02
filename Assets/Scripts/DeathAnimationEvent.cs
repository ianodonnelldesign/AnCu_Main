using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DeathAnimationEvent : MonoBehaviour
    {
        public GameObject deathScreenButtons;
        public Animator deathScreenAnimator;

        public void EnableDeathScreenButtons()
        {
            deathScreenButtons.SetActive(true);
        }
    }
}

