using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        PlayerWeaponSlotManager playerWeaponSlotManager;

        [Header("Quick Slot Items")]
        public ActionItem equippedItem;
        public ActionItem equippedShield;

        [Header("Current Equipment")]
        public List<ActionItem> itemsInRightHandSlots;
        public List<ActionItem> shield;

        public int currentRightItemIndex = 0;

        [Header("Inventory")]
        public List<Item> inventory;

        private void Awake()
        {
            playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        }

        private void Start()
        {
            equippedItem = itemsInRightHandSlots[currentRightItemIndex];
            equippedShield = shield[0];

            playerWeaponSlotManager.LoadWeaponOnSlot(equippedItem, false);
            playerWeaponSlotManager.LoadWeaponOnSlot(equippedShield, true);
        }

        public void QuickSlotOne()
        {
            equippedItem = itemsInRightHandSlots[0];
            playerWeaponSlotManager.LoadWeaponOnSlot(itemsInRightHandSlots[0], false);
        }
        public void QuickSlotTwo()
        {
            equippedItem = itemsInRightHandSlots[1];
            playerWeaponSlotManager.LoadWeaponOnSlot(itemsInRightHandSlots[1], false);
        }
        public void QuickSlotThree()
        {
            equippedItem = itemsInRightHandSlots[2];
            playerWeaponSlotManager.LoadWeaponOnSlot(itemsInRightHandSlots[2], false);
        }
        public void QuickSlotFour()
        {
            equippedItem = itemsInRightHandSlots[3];
            playerWeaponSlotManager.LoadWeaponOnSlot(itemsInRightHandSlots[3], false);
        }

        //public void ChangeRightWeapon()
        //{
        //    currentRightWeaponIndex = currentRightWeaponIndex + 1;

        //    if (currentRightWeaponIndex == 0 && weaponsInRightHandSlots[0] != null)
        //    {
        //        rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
        //        playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
        //    }
        //    else if (currentRightWeaponIndex == 0 && weaponsInRightHandSlots[0] == null)
        //    {
        //        currentRightWeaponIndex = currentRightWeaponIndex + 1;
        //    }

        //    else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlots[1] != null)
        //    {
        //        rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
        //        playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
        //    }
        //    else if (currentRightWeaponIndex == 1 && weaponsInRightHandSlots[1] == null)
        //    {
        //        currentRightWeaponIndex = currentRightWeaponIndex + 1;
        //    }

        //    if (currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
        //    {
        //        currentRightWeaponIndex = -1;
        //        rightWeapon = playerWeaponSlotManager.unarmedWeapon;
        //        playerWeaponSlotManager.LoadWeaponOnSlot(playerWeaponSlotManager.unarmedWeapon, false);
        //    }
        //}

        //public void ChangeLeftWeapon()
        //{
        //    currentLeftWeaponIndex = currentLeftWeaponIndex + 1;

        //    if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlots[0] != null)
        //    {
        //        leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
        //        playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
        //    }
        //    else if (currentLeftWeaponIndex == 0 && weaponsInLeftHandSlots[0] == null)
        //    {
        //        currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
        //    }

        //    else if (currentLeftWeaponIndex == 1 && weaponsInLeftHandSlots[1] != null)
        //    {
        //        leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
        //        playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
        //    }
        //    else if (currentLeftWeaponIndex == 1 && weaponsInLeftHandSlots[1] == null)
        //    {
        //        currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
        //    }

        //    if (currentLeftWeaponIndex > weaponsInLeftHandSlots.Length - 1)
        //    {
        //        currentLeftWeaponIndex = -1;
        //        leftWeapon = playerWeaponSlotManager.unarmedWeapon;
        //        playerWeaponSlotManager.LoadWeaponOnSlot(playerWeaponSlotManager.unarmedWeapon, true);
        //    }
        //}
    }
}