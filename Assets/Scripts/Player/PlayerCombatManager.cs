using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerCombatManager : MonoBehaviour
    {
        InputHandler inputHandler;
        CameraHandler cameraHandler;
        PlayerManager playerManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerEquipmentManager playerEquipmentManager;
        PlayerStatsManager playerStatsManager;
        PlayerInventoryManager playerInventoryManager;
        PlayerWeaponSlotManager playerWeaponSlotManager;

        public string lastAttack;

        LayerMask backStabLayer = 1 << 12;
        LayerMask riposteLayer = 1 << 13;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerManager = GetComponent<PlayerManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
            inputHandler = GetComponent<InputHandler>();
        }

        #region Input Actions

        public void HandleLightAttackAction()
        {
            if (playerInventoryManager.equippedItem.isMeleeWeapon)
            {
                PerformLightAttackAction();
            }
            else if (playerInventoryManager.equippedItem.isConsumable)
            {
                PerformConsumableAction();
            }
            else if (playerInventoryManager.equippedItem.isGearItem)
            {
                PerformGearAction();
            }
        }

        public void HandleHeavyAttackAction()
        {
            if (playerInventoryManager.equippedItem.isMeleeWeapon)
            {
                HandleHeavyAttackAction();
            }
            else if (playerInventoryManager.equippedItem.isConsumable)
            {
                PerformAlternateConsumableAction();
            }
            else if (playerInventoryManager.equippedItem.isGearItem)
            {
                PerformAlternateGearAction();
            }
        }

        public void HandleTwoHandAction()
        {
            //if (playerInventoryManager.leftWeapon.isShieldWeapon)
            //{
            //    PerformLTWeaponArt(inputHandler.twoHandFlag);
            //}
            //else if (playerInventoryManager.leftWeapon.isMeleeWeapon)
            //{
            //    //do a light attack
            //}
        }

        public void HandleBlockAction()
        {
            PerformLBBlockAction();
        }
        #endregion

        #region Actions
        private void PerformLightAttackAction()
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventoryManager.equippedItem);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;

                playerAnimatorManager.animator.SetBool("isUsingRightHand", true);
                HandleLightAttack(playerInventoryManager.equippedItem);
            }
        }
        public void HandleLightAttack(ActionItem weapon)
        {
            if (playerStatsManager.currentStamina <= 0)
                return;

            playerWeaponSlotManager.attackingWeapon = weapon;

            if (inputHandler.twoHandFlag)
            {
                playerAnimatorManager.PlayTargetAnimation(weapon.th_action_01, true);
                lastAttack = weapon.th_action_01;
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation(weapon.light_action_01, true);
                lastAttack = weapon.light_action_01;
            }
        }
        public void HandleHeavyAttackAction(ActionItem weapon)
        {
            if (playerStatsManager.currentStamina <= 0)
                return;

            playerWeaponSlotManager.attackingWeapon = weapon;

            if (inputHandler.twoHandFlag)
            {

            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation(weapon.heavy_action_01, true);
                lastAttack = weapon.heavy_action_01;
            }
        }

        public void HandleWeaponCombo(ActionItem weapon)
        {
            if (playerStatsManager.currentStamina <= 0)
                return;

            if (inputHandler.comboFlag)
            {
                playerAnimatorManager.animator.SetBool("canDoCombo", false);

                if (lastAttack == weapon.light_action_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(weapon.light_action_02, true);
                }
                else if (lastAttack == weapon.th_action_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(weapon.th_action_02, true);
                }
            }
        }


        private void PerformConsumableAction()
        {
            Debug.Log("You ate the item");
        }
        private void PerformAlternateConsumableAction()
        {
            Debug.Log("You alternately ate the item");
        }

        private void PerformGearAction()
        {
            Debug.Log("You used the gear's action.");
        }
        private void PerformAlternateGearAction()
        {
            Debug.Log("You alternately used the gear's action.");
        }


        private void PerformTHWeaponArt(bool isTwoHanding)
        {
            if (playerManager.isInteracting)
                return;

            if (isTwoHanding)
            {
                //If we are two handing perform weapon art for right weapon
            }
            //else
            //{
            //    playerAnimatorManager.PlayTargetAnimation(playerInventoryManager.leftWeapon.weapon_art, true);
            //}
        }

        #endregion

        #region Defense Actions
        private void PerformLBBlockAction()
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.isBlocking)
                return;

            playerAnimatorManager.PlayTargetAnimation("Block Start", false, true);
            playerEquipmentManager.OpenBlockingCollider();
            playerManager.isBlocking = true;
        }
        #endregion

        public void AttemptBackStabOrRiposte()
        {
            if (playerStatsManager.currentStamina <= 0)
                return;

            RaycastHit hit;

            if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position,
                transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null)
                {
                    //CHECK FOR TEAM I.D (So you cant back stab friends or yourself?
                    playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPosition.position;

                    Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int criticalDamage = playerInventoryManager.equippedItem.criticalDamageMuiltiplier * rightWeapon.currentWeaponDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    playerAnimatorManager.PlayTargetAnimation("Back Stab", true);
                    enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Back Stabbed", true);
                    //do damage
                }
            }
            else if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
            {
                //CHECK FOR TEAM I.D
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
                {
                    playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPosition.position;

                    Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int criticalDamage = playerInventoryManager.equippedItem.criticalDamageMuiltiplier * rightWeapon.currentWeaponDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    playerAnimatorManager.PlayTargetAnimation("Riposte", true);
                    enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Riposted", true);
                }
            }
        }
    }
}