using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class QuickSlotsUI : MonoBehaviour
    {
        //public Image leftWeaponIcon;
        public Image rightSlot01;
        public Image rightSlot02;
        public Image rightSlot03;
        public Image rightSlot04;

        public PlayerInventoryManager playerInventoryManager = null;

        public void UpdateWeaponQuickSlotsUI(WeaponItem weapon)
        {
            if (weapon.itemIcon != null)
            {
                    rightSlot01.sprite = playerInventoryManager.weaponsInRightHandSlots[0].itemIcon;
                    rightSlot01.enabled = true;

                    rightSlot02.sprite = playerInventoryManager.weaponsInRightHandSlots[1].itemIcon;
                    rightSlot02.enabled = true;

                    rightSlot03.sprite = playerInventoryManager.weaponsInRightHandSlots[2].itemIcon;
                    rightSlot03.enabled = true;

                    rightSlot04.sprite = playerInventoryManager.weaponsInRightHandSlots[3].itemIcon;
                    rightSlot04.enabled = true;
            }
            else
            {
                rightSlot01.sprite = null;
                rightSlot01.enabled = false;
                rightSlot02.sprite = null;
                rightSlot02.enabled = false;
                rightSlot03.sprite = null;
                rightSlot03.enabled = false;
                rightSlot04.sprite = null;
                rightSlot04.enabled = false;
            }
        }
    }
}