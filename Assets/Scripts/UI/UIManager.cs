using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using UnityEngine.EventSystems;

namespace SG
{
    public class UIManager : MonoBehaviour
    {
        public SceneField gameEndScene;

        public PlayerInventoryManager playerInventory;
        public EquipmentWindowUI equipmentWindowUI;

        InputHandler inputHandler;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject pauseWindow;
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

        [Header("Menu Buttons")]
        public GameObject pauseFirstButton;

        private void Awake()
        {
            inputHandler = FindObjectOfType<InputHandler>();
        }

        private void Start()
        {
            pauseWindow.SetActive(false);
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

        public void OpenPauseWindow()
        {
            pauseWindow.SetActive(true);
            Time.timeScale = 0f;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }

        public void ClosePauseWindow()
        {
            pauseWindow.SetActive(false);
            Time.timeScale = 1f;

            inputHandler.inventoryFlag = false;
            inputHandler.interactFlag = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            hudWindow.SetActive(true);
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            pauseWindow.SetActive(false);

            //maybe add confirmation screen
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