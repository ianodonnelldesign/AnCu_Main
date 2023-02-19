using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool rightHandSlot03Selected;
        public bool rightHandSlot04Selected;

        public HandEquipmentSlotUI[] handEquipmentSlotUI;

        private void Start()
        {
            handEquipmentSlotUI = GetComponentsInChildren<HandEquipmentSlotUI>();
        }


        //to the array in here, add the same things in the player inventory right hand slots array. Techinically this should be the other way around?
        //Add from here to the Quickslot UI? Not certain if it matters. Might have the same results.
        public void LoadWeaponsOnEquipmentScreen(PlayerInventoryManager playerInventory)
        {
            for (int i = 0; i < handEquipmentSlotUI.Length; i++)
            {
                if (handEquipmentSlotUI[i].rightHandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.itemsInRightHandSlots[0]);
                }
                else if (handEquipmentSlotUI[i].rightHandSlot02)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.itemsInRightHandSlots[1]);
                }
                else if (handEquipmentSlotUI[i].rightHandSlot03)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.itemsInRightHandSlots[2]);
                }
                else if (handEquipmentSlotUI[i].rightHandSlot04)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.itemsInRightHandSlots[3]);
                }
            }
        }

        public void SelectRightHandSlot01()
        {
            rightHandSlot01Selected = true;
        }

        public void SelectRightHandSlot02()
        {
            rightHandSlot02Selected = true;
        }

        public void SelectRightHandSlot03()
        {
            rightHandSlot03Selected = true;
        }

        public void SelectRightHandSlot04()
        {
            rightHandSlot04Selected = true;
        }
    }
}