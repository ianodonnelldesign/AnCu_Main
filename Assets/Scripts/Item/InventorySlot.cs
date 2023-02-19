using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class InventorySlot : MonoBehaviour
    {
        PlayerInventoryManager playerInventory;
        PlayerWeaponSlotManager weaponSlotManager;
        UIManager uiManager;

        public Image icon;
        ActionItem item;

        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventoryManager>();
            weaponSlotManager = FindObjectOfType<PlayerWeaponSlotManager>();
            uiManager = FindObjectOfType<UIManager>();
        }

        public void AddItem(ActionItem newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipThisItem()
        {
            if (uiManager.rightHandSlot01Selected)
            {
                playerInventory.inventory.Add(playerInventory.itemsInRightHandSlots[0]);
                playerInventory.itemsInRightHandSlots[0] = item;
                playerInventory.inventory.Remove(item);
                playerInventory.equippedItem = playerInventory.itemsInRightHandSlots[playerInventory.currentRightItemIndex];
            }
            else if (uiManager.rightHandSlot02Selected)
            {
                playerInventory.inventory.Add(playerInventory.itemsInRightHandSlots[1]);
                playerInventory.itemsInRightHandSlots[1] = item;
                playerInventory.inventory.Remove(item);
                playerInventory.equippedItem = playerInventory.itemsInRightHandSlots[playerInventory.currentRightItemIndex];
            }
            else if (uiManager.rightHandSlot03Selected)
            {
                playerInventory.inventory.Add(playerInventory.itemsInRightHandSlots[2]);
                playerInventory.itemsInRightHandSlots[2] = item;
                playerInventory.inventory.Remove(item);
                playerInventory.equippedItem = playerInventory.itemsInRightHandSlots[playerInventory.currentRightItemIndex];
            }
            else if (uiManager.rightHandSlot04Selected)
            {
                playerInventory.inventory.Add(playerInventory.itemsInRightHandSlots[3]);
                playerInventory.itemsInRightHandSlots[3] = item;
                playerInventory.inventory.Remove(item);
                playerInventory.equippedItem = playerInventory.itemsInRightHandSlots[playerInventory.currentRightItemIndex];
            }
            else
            {
                return;
            }

            playerInventory.equippedItem = playerInventory.itemsInRightHandSlots[playerInventory.currentRightItemIndex];

            weaponSlotManager.LoadWeaponOnSlot(playerInventory.equippedItem);

            uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
            uiManager.ResetAllSelectedSlots();
        }

    }
}