using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CharacterManager : MonoBehaviour
    {
        public CharacterController characterController;
        public Animator animator;
        public CharacterStatsManager characterStatsManager;
        public InputHandler inputHandler;
        //Animator animator;
        public CameraHandler cameraHandler;
        public PlayerStatsManager playerStatsManager;
        public PlayerAnimatorManager playerAnimatorManager;
        public PlayerLocomotionManager playerLocomotion;
        public PlayerInventoryManager playerInventoryManager;
        public PlayerWeaponSlotManager playerWeaponSlotManager;

        //public InteractableUI interactableUI;


        [Header("Lock On Transform")]
        public Transform lockOnTransform;

        [Header("Combat Colliders")]
        public CriticalDamageCollider backStabCollider;
        public CriticalDamageCollider riposteCollider;

        [Header("Interaction")]
        public bool isInteracting;

        [Header("Combat Flags")]
        public bool canBeRiposted;
        public bool canBeParried;
        public bool canDoCombo;
        public bool isParrying;
        public bool isBlocking;
        public bool isInvulnerable;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;

        [Header("Movement Flags")]
        public bool isRotatingWithRootMotion;
        public bool canRotate;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;

        [Header("Spells")]
        public bool isFiringSpell;

        //Damage will be inflicted during an animation event
        //Used in backstab or riposte animations
        public int pendingCriticalDamage;
        
        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
            inputHandler = GetComponent<InputHandler>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            //animator = GetComponent<Animator>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerLocomotion = GetComponent<PlayerLocomotionManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            //interactableUI = FindObjectOfType<InteractableUI>();
            playerWeaponSlotManager = FindObjectOfType<PlayerWeaponSlotManager>();
        }

    }
}