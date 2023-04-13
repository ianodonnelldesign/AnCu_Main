using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DisableAnimator : MonoBehaviour
    {
        public Animator enemyAnimator;
        public AICharacterAnimatorManager enemyAnimatorAI;
        private void OnEnable()
        {
            enemyAnimator.enabled = false;
            enemyAnimatorAI.enabled = false;
        }
    }
}

