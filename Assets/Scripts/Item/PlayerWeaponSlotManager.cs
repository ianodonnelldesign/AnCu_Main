using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerWeaponSlotManager : MonoBehaviour
    {
        QuickSlotsUI quickSlotsUI;

        InputHandler inputHandler;
        Animator animator;
        PlayerManager playerManager;
        PlayerInventoryManager playerInventoryManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerStatsManager playerStatsManager;

        ActionItem actionItem;

        [Header("Attacking Weapon")]
        public ActionItem attackingWeapon;

        [Header("Unarmed Weapon")]
        public ActionItem unarmedWeapon;

        [Header("Weapon Slots")]
        public WeaponHolderSlot rightHandSlot;
        public WeaponHolderSlot backSlot;

        [Header("Damage Colliders")]
        public DamageCollider rightHandDamageCollider;

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerManager = GetComponent<PlayerManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            LoadWeaponHolderSlots();
        }

        private void LoadWeaponHolderSlots()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.isBackSlot)
                {
                    backSlot = weaponSlot;
                }
            }
        }

        public void LoadBothWeaponsOnSlots()
        {
            LoadWeaponOnSlot(playerInventoryManager.equippedItem);
        }

        public void LoadWeaponOnSlot(ActionItem weaponItem)
        {
            if (weaponItem != null)
            {
                if (inputHandler.twoHandFlag)
                {
                }
                else
                {
                    animator.CrossFade("Both Arms Empty", 0.2f);
                    backSlot.UnloadWeaponAndDestroy();
                }

                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(weaponItem);
                //playerAnimatorManager.animator.runtimeAnimatorController = actionItem.actionItemController;
            }
            else
            {
                weaponItem = unarmedWeapon;
            }
        }

        #region Handle Weapon's Damage Collider

        //private void LoadLeftWeaponDamageCollider()
        //{
        //    leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        //    leftHandDamageCollider.currentWeaponDamage = playerInventoryManager.leftWeapon.baseDamage;
        //    leftHandDamageCollider.poiseBreak = playerInventoryManager.leftWeapon.poiseBreak;
        //}

        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            rightHandDamageCollider.currentWeaponDamage = playerInventoryManager.equippedItem.baseDamage;
            rightHandDamageCollider.poiseBreak = playerInventoryManager.equippedItem.poiseBreak;
        }

        public void OpenDamageCollider()
        {
            if (playerManager.isUsingRightHand)
            {
                rightHandDamageCollider.EnableDamageCollider();
            }
            //else if (playerManager.isUsingLeftHand)
            //{
            //    leftHandDamageCollider.EnableDamageCollider();
            //}
        }

        public void CloseDamageCollider()
        {
            if (rightHandDamageCollider != null)
            {
                rightHandDamageCollider.DisableDamageCollider();
            }

            //if (leftHandDamageCollider != null)
            //{
            //    leftHandDamageCollider.DisableDamageCollider();
            //}
        }

        #endregion

        #region Handle Weapon's Stamina Drainage
        public void DrainStaminaLightAttack()
        {
            playerStatsManager.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
        }

        public void DrainStaminaHeavyAttack()
        {
            playerStatsManager.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
        }
        #endregion

        #region Handle Weapon's Poise Bonus

        public void GrantWeaponAttackingPoiseBonus()
        {
            playerStatsManager.totalPoiseDefence = playerStatsManager.totalPoiseDefence + attackingWeapon.offensivePoiseBonus;
        }

        public void ResetWeaponAttackingPoiseBonus()
        {
            playerStatsManager.totalPoiseDefence = playerStatsManager.armorPoiseBonus;
        }

        #endregion
    }
}