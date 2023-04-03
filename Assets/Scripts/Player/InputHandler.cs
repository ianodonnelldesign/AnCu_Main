using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;

        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool sprint_Input;
        public bool roll_Input;
        public bool interact_Input;
        public bool x_Input;
        public bool twoHandMode_Input;
        public bool lightAttack_Input;
        public bool heavyAttack_Input;
        public bool block_Input;
        public bool parry_Input;
        public bool critical_Attack_Input;
        public bool jump_Input;
        public bool inventory_Input;
        public bool lockOnInput;
        public bool right_Stick_Right_Input;
        public bool right_Stick_Left_Input;
        public bool zTarget_Input;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool rollFlag;
        public bool twoHandFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool lockOnFlag;
        public bool inventoryFlag;
        //public float rollInputTimer;

        public bool interactFlag;

        public Transform criticalAttackRayCastStartPoint;

        PlayerControls inputActions;
        PlayerCombatManager playerCombatManager;
        PlayerInventoryManager playerInventoryManager;
        PlayerManager playerManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerStatsManager playerStatsManager;
        BlockingCollider blockingCollider;
        PlayerWeaponSlotManager weaponSlotManager;
        CameraHandler cameraHandler;
        UIManager uiManager;
        CinematicBars cinematicBars;

        PlayerLocomotionManager playerLocomotionManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerManager = GetComponent<PlayerManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            weaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
            blockingCollider = GetComponentInChildren<BlockingCollider>();
            uiManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            cinematicBars = FindObjectOfType<CinematicBars>();
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }

        public void Update()
        {
            if (interactFlag == true)
            {
                LockInput();
            }
            else if (interactFlag == false)
            {
                UnlockInput();
            }
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                inputActions.PlayerActions.LightAttack.performed += i => lightAttack_Input = true;
                inputActions.PlayerActions.HeavyAttack.performed += i => heavyAttack_Input = true;
                inputActions.PlayerActions.Block.performed += i => block_Input = true;
                inputActions.PlayerActions.Block.canceled += i => block_Input = false;
                inputActions.PlayerActions.Parry.performed += i => parry_Input = true;
                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;
                inputActions.PlayerQuickSlots.DPadDown.performed += i => d_Pad_Down = true;
                inputActions.PlayerQuickSlots.DPadUp.performed += i => d_Pad_Up = true;
                inputActions.PlayerActions.Interact.performed += i => interact_Input = true;
                inputActions.PlayerActions.Sprint.performed += i => sprint_Input = true;
                inputActions.PlayerActions.Sprint.canceled += i => sprint_Input = false;
                inputActions.PlayerActions.Roll.performed += i => roll_Input = true;
                inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
                inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
                inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;
                inputActions.PlayerActions.TwoHand.performed += i => twoHandMode_Input = true;
                inputActions.PlayerActions.CriticalAttack.performed += i => critical_Attack_Input = true;
                inputActions.PlayerActions.ZTarget.performed += i => zTarget_Input = true;
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            HandleMoveInput(delta);
            HandleSprintInput(delta);
            HandleRollInput();
            HandleCombatInput(delta);
            HandleQuickSlotsInput();
            HandleInventoryInput();
            HandleLockOnInput();
            HandleTwoHandInput();
            HandleCriticalAttackInput();
        }

        private void LockInput()
        {
            uiManager.hudWindow.SetActive(false);
            //dont want to disable Inventory because inventory will be pause/escape
            inputActions.PlayerMovement.Movement.Disable();
            inputActions.PlayerMovement.Camera.Disable();

            movementInput.x = 0;
            movementInput.y = 0;
            moveAmount = 0;

            cameraInput.x = 0;
            cameraInput.y = 0;
        }

        private void UnlockInput()
        {
            uiManager.hudWindow.SetActive(true);
            inputActions.PlayerActions.Inventory.Enable();
            inputActions.PlayerMovement.Movement.Enable();
            inputActions.PlayerMovement.Camera.Enable();
        }

        private void HandleMoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleSprintInput(float delta)
        {
            if (sprint_Input)
            {
                if (playerStatsManager.currentStamina <= 0)
                {
                    sprint_Input = false;
                    sprintFlag = false;
                }

                if (moveAmount > 0.5f && playerStatsManager.currentStamina > 0)
                {
                    sprintFlag = true;
                }
            }
            else
            {
                sprintFlag = false;
            }
        }

        private void HandleRollInput()
        {
            if(roll_Input)
            {
                if(playerStatsManager.currentStamina <= 0)
                {
                    roll_Input = false;
                    rollFlag = false;
                }
                else
                {
                    sprintFlag = false;

                    if (playerStatsManager.currentStamina >= 15f)
                    {
                        rollFlag = true;
                        roll_Input = false;
                    }
                }
            }
            else
            {
                rollFlag = false;
            }
        }

        private void HandleCombatInput(float delta)
        {

            if (lightAttack_Input)
            {
                playerCombatManager.HandleLightAttackAction();
            }

            if (heavyAttack_Input)
            {
                playerCombatManager.HandleHeavyAttackAction(playerInventoryManager.equippedItem);
            }

            if (parry_Input)
            {
                if (twoHandFlag)
                {
                    //parry with the shield
                }
                else
                {
                    playerCombatManager.HandleTwoHandAction(); //which doesn't do anything right now
                }
            }

            if (block_Input)
            {
                playerCombatManager.HandleBlockAction();
            }
            else
            {
                playerManager.isBlocking = false;

                if (blockingCollider.blockingCollider.enabled)
                {
                    blockingCollider.DisableBlockingCollider();
                }
            }
        }

        private void HandleQuickSlotsInput()
        {
            if (d_Pad_Right)
            {
                playerInventoryManager.QuickSlotThree();
            }
            else if (d_Pad_Left)
            {
                playerInventoryManager.QuickSlotTwo();
            }
            else if (d_Pad_Up)
            {
                playerInventoryManager.QuickSlotOne();
            }
            else if (d_Pad_Down)
            {
                playerInventoryManager.QuickSlotFour();
            }
        }

        private void HandleInventoryInput()
        {
            if (inventory_Input)
            {
                inventoryFlag = !inventoryFlag;
                if (inventoryFlag)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    uiManager.OpenPauseWindow();
                    //uiManager.inventoryWindow.SetActive(true);

                    uiManager.UpdateUI();
                    uiManager.hudWindow.SetActive(false);
                    //playerManager.isInteracting = true;

                    interactFlag = true;
                }
                else
                {
                    interactFlag = false;

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    uiManager.ClosePauseWindow();
                    uiManager.CloseAllInventoryWindows();
                    uiManager.hudWindow.SetActive(true);
                    //playerManager.isInteracting = false;
                }
            }
        }

        private void HandleLockOnInput()
        {
            if (lockOnInput == true && lockOnFlag == false)
            {
                lockOnInput = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.nearestLockOnTarget != null)
                {
                    //size of bar, time to get to that size
                    cinematicBars.ShowCinematicBars(200, .3f);
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }

            else if (lockOnInput && lockOnFlag == true && interactFlag == false)
            {
                //time to hide bars
                cinematicBars.HideCinematicBars(.3f);   
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }

            if (lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
            }

            if (lockOnFlag && right_Stick_Right_Input)
            {
                right_Stick_Right_Input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.rightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }

            cameraHandler.SetCameraHeight();
        }

        private void HandleTwoHandInput()
        {
            //when you press the two hand button, get the shield out. If the shield is already out, put it away.

            if (twoHandMode_Input)
            {
                twoHandMode_Input = false;

                twoHandFlag = !twoHandFlag;

                if (twoHandFlag)
                {
                    //only have the right hand weapon out
                    weaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.equippedItem, false);
                }
                else
                {
                    //get the shield out too
                    weaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.equippedItem, false);
                    weaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.equippedShield, true);

                }
            }
        }

        private void HandleCriticalAttackInput()
        {
            if (critical_Attack_Input)
            {
                critical_Attack_Input = false;
                playerCombatManager.AttemptBackStabOrRiposte();
            }
        }
    }
}