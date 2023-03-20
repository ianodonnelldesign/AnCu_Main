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
        

        [Header("Attack Animations")]

        string light_action_01 = "OH_Light_Attack_01";
        string light_action_02 = "OH_Light_Attack_02";
        string light_action_03 = "OH_Light_Attack_03";
        string heavy_action_01 = "OH_Heavy_Attack_01";
        string heavy_action_02 = "OH_Heavy_Attack_02";
        //string th_action_01 = "TH_Attack_01";
        //string th_action_02 = "OH_Light_Attack_03";
        string unarmed_action = "Kick";
        //string weapon_art = 

        public string offHandIdleAnimation = "Left_Arm_Idle_001";

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

        public void HandleLightAttackAction()
        {
            if (playerInventoryManager.equippedItem.itemType == ItemType.Hurley)
            {
                PerformLightAttackAction();
            }
            else if (playerInventoryManager.equippedItem.itemType == ItemType.Consumable)
            {
                PerformConsumableAction();
            }
            else if (playerInventoryManager.equippedItem.itemType == ItemType.Gear)
            {
                PerformGearAction();
            }
            else if (playerInventoryManager.equippedItem.itemType == ItemType.Unarmed)
            {
                PerformUnarmedAction();
            }
        }
        public void HandleHeavyAttackAction()
        {
            if (playerInventoryManager.equippedItem.itemType == ItemType.Hurley)
            {
                HandleHeavyAttackAction();
            }
            else if (playerInventoryManager.equippedItem.itemType == ItemType.Consumable)
            {
                PerformAlternateConsumableAction();
            }
            else if (playerInventoryManager.equippedItem.itemType == ItemType.Gear)
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

            //these are the same in case i ever want different actions with a shield out
            if (inputHandler.twoHandFlag)
            {
                playerAnimatorManager.PlayTargetAnimation(light_action_01, true);
                lastAttack = light_action_01;
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation(light_action_01, true);
                lastAttack = light_action_01;
            }
        }
        public void HandleHeavyAttackAction(ActionItem weapon)
        {
            if (playerStatsManager.currentStamina <= 0)
                return;

            playerWeaponSlotManager.attackingWeapon = weapon;

            //these are the same in case i ever want different actions with a shield out
            if (inputHandler.twoHandFlag)
            {
                playerAnimatorManager.PlayTargetAnimation(heavy_action_01, true);
                lastAttack = heavy_action_01;
            }
            else
            {
                playerAnimatorManager.PlayTargetAnimation(heavy_action_01, true);
                lastAttack = heavy_action_01;
            }
        }
        public void HandleWeaponCombo(ActionItem weapon)
        {
            if (playerStatsManager.currentStamina <= 0)
                return;

            if (inputHandler.comboFlag)
            {
                playerAnimatorManager.animator.SetBool("canDoCombo", false);

                if (lastAttack == light_action_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(light_action_02, true);
                    lastAttack = light_action_02;
                }
                else if (lastAttack == light_action_02)
                {
                    playerAnimatorManager.PlayTargetAnimation(light_action_03, true);
                    lastAttack = light_action_03;
                }
                else if (lastAttack == heavy_action_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(heavy_action_02, true);
                    lastAttack = heavy_action_02;
                }
                //else if (lastAttack == th_action_01)
                //{
                //    playerAnimatorManager.PlayTargetAnimation(th_action_02, true);
                //}
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

        private void PerformUnarmedAction()
        {
            if (playerManager.isInteracting)
                return;
            
            else
            {
                playerAnimatorManager.PlayTargetAnimation(unarmed_action, false);
                lastAttack = unarmed_action;
            }
            
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
            //    playerAnimatorManager.PlayTargetAnimation(weapon_art, true);
            //}
        }


        private void PerformLBBlockAction()
        {
            if (playerManager.isInteracting)
                return;

            if (playerManager.isBlocking)
                return;

            playerAnimatorManager.PlayTargetAnimation("Block_Start", false, true);
            playerEquipmentManager.OpenBlockingCollider();
            playerManager.isBlocking = true;
        }


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