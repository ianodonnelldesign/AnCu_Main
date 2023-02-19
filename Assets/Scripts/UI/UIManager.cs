using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class UIManager : MonoBehaviour
    {
        public PlayerInventoryManager playerInventory;
        public EquipmentWindowUI equipmentWindowUI;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject inventoryWindow;
        public GameObject settingsWindow;

        [Header("Equipment Window Slot Selected")]
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool rightHandSlot03Selected;
        public bool rightHandSlot04Selected;

        [Header("Inventory")]
        public GameObject inventorySlotPrefab;
        public Transform inventorySlotsParent;
        InventorySlot[] weaponInventorySlots;

        private void Awake()
        {
            
        }

        private void Start()
        {
            weaponInventorySlots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        }

        public void UpdateUI()
        {
            #region Weapon Inventory Slots
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerInventory.inventory.Count)
                {
                    if (weaponInventorySlots.Length < playerInventory.inventory.Count)
                    {
                        Instantiate(inventorySlotPrefab, inventorySlotsParent);
                        weaponInventorySlots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
                    }

                    //weaponInventorySlots[i].AddItem(playerInventory.inventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }
            #endregion
        }

        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }

        public void OpenSettingsWindow()
        {
            settingsWindow.SetActive(true);
        }

        public void CloseSettingsWindow()
        {
            settingsWindow.SetActive(false);
        }

        public void CloseAllInventoryWindows()
        {
            ResetAllSelectedSlots();
            inventoryWindow.SetActive(false);
            settingsWindow.SetActive(false);
        }

        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            rightHandSlot03Selected = false;
            rightHandSlot04Selected = false;
        }
    }
}