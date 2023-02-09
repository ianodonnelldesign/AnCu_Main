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

        public void UpdateWeaponQuickSlotsUI(WeaponItem weapon)
        {
            if (weapon.itemIcon != null)
            {
                rightSlot01.sprite = weapon.itemIcon;
                rightSlot01.enabled = true;
            }
            else
            {
                rightSlot01.sprite = null;
                rightSlot01.enabled = false;
            }

            //if (isLeft == false)
            //{



            //}
            //else
            //{
            //    if (weapon.itemIcon != null)
            //    {
            //        leftWeaponIcon.sprite = weapon.itemIcon;
            //        leftWeaponIcon.enabled = true;
            //    }
            //    else
            //    {
            //        leftWeaponIcon.sprite = null;
            //        leftWeaponIcon.enabled = false;
            //    }
            //}
        }
    }
}