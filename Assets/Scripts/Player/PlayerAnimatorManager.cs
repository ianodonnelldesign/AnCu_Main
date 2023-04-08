using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerAnimatorManager : AnimatorManager
    {
        InputHandler inputHandler;
        PlayerManager playerManager;
        PlayerStatsManager playerStatsManager;
        PlayerLocomotionManager playerLocomotionManager;

        int vertical;
        int horizontal;


        public void Initialize()
        {
            inputHandler = GetComponentInParent<InputHandler>();
            animator = GetComponentInParent<Animator>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStatsManager = GetComponentInParent<PlayerStatsManager>();
            playerLocomotionManager = GetComponentInParent<PlayerLocomotionManager>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            if (isSprinting && inputHandler.moveAmount > 0)
            {
                v = 2;
                h = horizontalMovement;
            }

            animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public override void TakeCriticalDamageAnimationEvent()
        {
            playerStatsManager.TakeDamageNoAnimation(playerManager.pendingCriticalDamage);
            playerManager.pendingCriticalDamage = 0;
        }

        public void DisableCollision()
        {
            playerLocomotionManager.characterCollider.enabled = false;
            playerLocomotionManager.characterCollisionBlockerCollider.enabled = false;
        }

        public void EnableCollision()
        {
            playerLocomotionManager.characterCollider.enabled = true;
            playerLocomotionManager.characterCollisionBlockerCollider.enabled = true;
        }

        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
                return;

            float delta = Time.deltaTime;
            playerLocomotionManager.rigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotionManager.rigidbody.velocity = velocity;
        }

    }
}