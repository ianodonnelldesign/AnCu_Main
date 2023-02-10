using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        PlayerWeaponSlotManager playerWeaponSlotManager;

        [Header("Quick Slot Items")]
        public WeaponItem equippedWeapon;
        //public WeaponItem leftWeapon;

        [Header("Current Equipment")]


        public List<WeaponItem> weaponsInRightHandSlots;
        //public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];

        

        public int currentRightWeaponIndex = 0;
        //public int currentLeftWeaponIndex = 0;

        public List<WeaponItem> weaponsInventory;

        private void Awake()
        {
            playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        }

        private void Start()
        {
            equippedWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];
            //leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];
            playerWeaponSlotManager.LoadWeaponOnSlot(equippedWeapon);
            //playerWeaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
        }

        public void QuickSlotOne()
        {
            equippedWeapon = weaponsInRightHandSlots[0];
            playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[0]);
        }
        public void QuickSlotTwo()
        {
            equippedWeapon = weaponsInRightHandSlots[1];
            playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[1]);
        }
        public void QuickSlotThree()
        {
            equippedWeapon = weaponsInRightHandSlots[2];
            playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[2]);
        }
        public void QuickSlotFour()
        {
            equippedWeapon = weaponsInRightHandSlots[3];
            playerWeaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[3]);
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