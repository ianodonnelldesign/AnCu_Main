using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        Animator animator;
        CameraHandler cameraHandler;
        PlayerStatsManager playerStatsManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerLocomotionManager playerLocomotion;
        PlayerInventoryManager playerInventoryManager;
        PlayerWeaponSlotManager playerWeaponSlotManager;

        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;
        public GameObject itemInteractableGameObject;


        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
            inputHandler = GetComponent<InputHandler>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            animator = GetComponent<Animator>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerLocomotion = GetComponent<PlayerLocomotionManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            interactableUI = FindObjectOfType<InteractableUI>();
            playerWeaponSlotManager = FindObjectOfType<PlayerWeaponSlotManager>();

            WorldSaveGameManager.instance.player = this;
        }

        void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = animator.GetBool("isInteracting");
            canDoCombo = animator.GetBool("canDoCombo");
            isUsingRightHand = animator.GetBool("isUsingRightHand");
            isUsingLeftHand = animator.GetBool("isUsingLeftHand");
            isInvulnerable = animator.GetBool("isInvulnerable");
            isFiringSpell = animator.GetBool("isFiringSpell");
            animator.SetBool("isInAir", isInAir);
            animator.SetBool("isDead", playerStatsManager.isDead);
            animator.SetBool("isBlocking", isBlocking);

            inputHandler.TickInput(delta);
            playerAnimatorManager.canRotate = animator.GetBool("canRotate");
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleJumping();
            playerStatsManager.RegenerateStamina();

            CheckForInteractableObject();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRotation(delta);
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.lightAttack_Input = false;
            inputHandler.heavyAttack_Input = false;
            inputHandler.parry_Input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.interact_Input = false;
            inputHandler.jump_Input = false;
            inputHandler.inventory_Input = false;

            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }



        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 2f, cameraHandler.ignoreLayers))
            {
                if (hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);

                        if (inputHandler.interact_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if (interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }

                if (itemInteractableGameObject != null && inputHandler.interact_Input)
                {
                    itemInteractableGameObject.SetActive(false);
                }
            }
        }

        public void OpenChestInteraction(Transform playerStandsHereWhenOpeningChest)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero; //Stops the player from ice skating
            transform.position = playerStandsHereWhenOpeningChest.transform.position;
            playerAnimatorManager.PlayTargetAnimation("Open Chest", true);
        }

        public void PassThroughFogWallInteraction(Transform fogWallEntrance)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero; //Stops the player from ice skating

            Vector3 rotationDirection = fogWallEntrance.transform.forward;
            Quaternion turnRotation = Quaternion.LookRotation(rotationDirection);
            transform.rotation = turnRotation;
            //Rotate over time so it does not look as rigid

            playerAnimatorManager.PlayTargetAnimation("Pass Through Fog", true);
        }

        public void HandlePlayerInCutscene()
        {
            inputHandler.interactFlag = true;
        }

        public void SaveCharacterDataToCurrentSaveData(ref CharacterSaveData currentCharacterSaveData)
        {
            currentCharacterSaveData.characterName = playerStatsManager.characterName;
            currentCharacterSaveData.characterLevel = playerStatsManager.playerLevel;

            currentCharacterSaveData.xPosition = transform.position.x;
            currentCharacterSaveData.yPosition = transform.position.y;
            currentCharacterSaveData.zPosition = transform.position.z;

            //  EQUIPMENT
            currentCharacterSaveData.currentRightHandWeaponID = playerInventoryManager.equippedItem.itemID;

        }

        public void LoadCharacterDataFromCurrentCharacterSaveData(ref CharacterSaveData currentCharacterSaveData)
        {
            playerStatsManager.characterName = currentCharacterSaveData.characterName;
            playerStatsManager.playerLevel = currentCharacterSaveData.characterLevel;

            transform.position = new Vector3(currentCharacterSaveData.xPosition, currentCharacterSaveData.yPosition, currentCharacterSaveData.zPosition);

            //  EQUIPMENT
            playerInventoryManager.equippedItem = WorldItemDataBase.Instance.GetWeaponItemByID(currentCharacterSaveData.currentRightHandWeaponID);
            playerWeaponSlotManager.LoadBothWeaponsOnSlots();

        }

    }
}