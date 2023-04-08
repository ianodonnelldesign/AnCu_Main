using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SG
{
    public class PlayerLocomotionManager : MonoBehaviour
    {
        CameraHandler cameraHandler;

        PlayerManager player;
        PlayerStatsManager playerStatsManager;
        InputHandler inputHandler;
        public Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public PlayerAnimatorManager playerAnimatorManager;
        public new Rigidbody rigidbody;


        public CinemachineVirtualCamera normalCamera;
        Transform cameraObject;

        [Header("Ground & Air Detection Stats")]
        public LayerMask groundLayer;
        public float inAirTimer;

        [SerializeField] public Vector3 yVelocity;
        [SerializeField] public float groundedYVelocity = -20;   // THE FORCE APPLIED TO YOU WHILST GROUNDED
        [SerializeField] public float fallStartYVelocity = -7;   // THE FORCE APPLIED TO YOU WHEN YOU BEGIN TO FALL (INCREASES OVER TIME)
        [SerializeField] public float gravityForce = -25;
        [SerializeField] public float groundCheckSphereRadius = 1f;
        protected bool fallingVelocitySet = false;

        [Header("Movement Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float walkingSpeed = 1;
        [SerializeField]
        float sprintSpeed = 7;
        [SerializeField]
        float rotationSpeed = 10;
        //[SerializeField]
        //float fallingSpeed = 100;

        [Header("Jumping")]
        [SerializeField] 
        float jumpHeight = 10f;

        [Header("Stamina Costs")]
        [SerializeField]
        public int rollStaminaCost = 10;
        public int backstepStaminaCost = 10;
        public int sprintStaminaCost = 1;

        [Header("Dodging")]
        public float rollDistance = 2f;
        public float backstepDistance = 2f;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            player = GetComponent<PlayerManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();

        }

        void Start()
        {
            cameraObject = normalCamera.transform;
            myTransform = gameObject.transform;
            playerAnimatorManager.Initialize();

            player.isGrounded = true;
            //groundLayer = ~(1 << 8 | 1 << 11);
            //Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
        }

        private void Update()
        {
            player.isGrounded = Physics.CheckSphere(player.transform.position, groundCheckSphereRadius, groundLayer);
            HandleFalling();
        }

        Vector3 normalVector;
        Vector3 targetPosition;

        public void HandleRotation(float delta)
        {
            if (playerAnimatorManager.canRotate)
            {
                if (inputHandler.lockOnFlag)
                {
                    if (inputHandler.sprintFlag || inputHandler.rollFlag)
                    {
                        Vector3 targetDirection = Vector3.zero;
                        targetDirection = cameraHandler.cameraTransform.forward * inputHandler.vertical;
                        targetDirection += cameraHandler.cameraTransform.right * inputHandler.horizontal;
                        targetDirection.Normalize();
                        targetDirection.y = 0;

                        if (targetDirection == Vector3.zero)
                        {
                            targetDirection = transform.forward;
                        }

                        Quaternion tr = Quaternion.LookRotation(targetDirection);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                        transform.rotation = targetRotation;
                    }
                    else
                    {
                        Vector3 rotationDirection = moveDirection;
                        rotationDirection = cameraHandler.currentLockOnTarget.transform.position - transform.position;
                        rotationDirection.y = 0;
                        rotationDirection.Normalize();
                        Quaternion tr = Quaternion.LookRotation(rotationDirection);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                        transform.rotation = targetRotation;
                    }
                }
                else
                {
                    Vector3 targetDir = Vector3.zero;
                    float moveOverride = inputHandler.moveAmount;

                    targetDir = cameraObject.forward * inputHandler.vertical;
                    targetDir += cameraObject.right * inputHandler.horizontal;

                    targetDir.Normalize();
                    targetDir.y = 0;

                    if (targetDir == Vector3.zero)
                        targetDir = myTransform.forward;

                    float rs = rotationSpeed;

                    Quaternion tr = Quaternion.LookRotation(targetDir);
                    Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

                    myTransform.rotation = targetRotation;
                }
            }
        }

        public void HandleMovement(float delta)
        {
            if (inputHandler.rollFlag)
                return;

            if (player.isInteracting)
                return;

            if (!player.isGrounded)
                return;

            //moveDirection = cameraObject.forward * inputHandler.vertical;
            //moveDirection += cameraObject.right * inputHandler.horizontal;
            //moveDirection.Normalize();
            //moveDirection.y = 0;

            moveDirection = player.cameraHandler.transform.forward * player.inputHandler.vertical;
            moveDirection = moveDirection + player.cameraHandler.transform.right * player.inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            //float speed = movementSpeed;


            if (player.isSprinting)
            {
                player.characterController.Move(moveDirection * sprintSpeed * Time.deltaTime);
            }
            else
            {
                if (player.inputHandler.moveAmount > 0.5f)
                {
                    player.characterController.Move(moveDirection * movementSpeed * Time.deltaTime);

                }
                else if (player.inputHandler.moveAmount <= 0.5f)
                {
                    player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
                }
            }


            //if (inputHandler.sprintFlag && inputHandler.moveAmount > 0.5)
            //{
            //    if(player.isBlocking)
            //    {
            //        movementSpeed = walkingSpeed;
            //        player.isSprinting = false;
            //        moveDirection *= movementSpeed;
            //    }
            //    else
            //    {
            //        movementSpeed = sprintSpeed;
            //        player.isSprinting = true;
            //        moveDirection *= movementSpeed;
            //        playerStatsManager.TakeStaminaDamage(sprintStaminaCost);
            //    }
            //}

            //else
            //{
            //    if (inputHandler.moveAmount < 0.5)
            //    {
            //        moveDirection *= walkingSpeed;
            //        player.isSprinting = false;
            //    }
            //    else
            //    {
            //        moveDirection *= movementSpeed;
            //        player.isSprinting = false;
            //    }
            //}

            //Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            //rigidbody.velocity = projectedVelocity;

            if (inputHandler.lockOnFlag && inputHandler.sprintFlag == false)
            {
                playerAnimatorManager.UpdateAnimatorValues(inputHandler.vertical, inputHandler.horizontal, player.isSprinting);
            }
            else
            {
                playerAnimatorManager.UpdateAnimatorValues(inputHandler.moveAmount, 0, player.isSprinting);
            }
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if (playerAnimatorManager.animator.GetBool("isInteracting"))
                return;

            if (playerStatsManager.currentStamina <= 0)
                return;

            if (inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if (inputHandler.moveAmount > 0)
                {
                    moveDirection.Normalize();
                    playerAnimatorManager.PlayTargetAnimation("Rolling", false);
                    
                    player.characterController.Move(moveDirection * rollDistance * Time.deltaTime);
                    moveDirection.y = 0;

                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;

                    playerStatsManager.TakeStaminaDamage(rollStaminaCost);
                }
                else
                {
                    playerAnimatorManager.PlayTargetAnimation("Backstep", false);
                    player.characterController.Move(-moveDirection * backstepDistance * Time.deltaTime);

                    playerStatsManager.TakeStaminaDamage(backstepStaminaCost);
                }
            }
        }

        public void HandleFalling()
        {
            if (player.isGrounded)
            {
                if(fallingVelocitySet)
                {
                    inAirTimer = 0;
                    fallingVelocitySet = false;
                    yVelocity.y = groundedYVelocity;
                    playerAnimatorManager.PlayTargetAnimation("Land", true);
                }
            }
            else
            {
                if (!fallingVelocitySet)
                {
                    fallingVelocitySet = true;
                    yVelocity.y = fallStartYVelocity;
                    playerAnimatorManager.PlayTargetAnimation("Falling", false);
                }

                inAirTimer += Time.deltaTime;
                yVelocity.y += gravityForce * Time.deltaTime;
            }

            player.characterController.Move(yVelocity * Time.deltaTime);
        }    

        //public void HandleFalling(float delta, Vector3 moveDirection)
        //{
        //    player.isGrounded = false;
        //    RaycastHit hit;
        //    Vector3 origin = myTransform.position;
        //    origin.y += groundDetectionRayStartPoint;

        //    if (Physics.Raycast(origin, myTransform.forward, out hit, 0.8f))
        //    {
        //        moveDirection = Vector3.zero;
        //    }

        //    if (player.isInAir)
        //    {
        //        rigidbody.AddForce(-Vector3.up * fallingSpeed * 10f);
        //        rigidbody.AddForce(moveDirection * fallingSpeed / 2f);
        //    }

        //    Vector3 dir = moveDirection;
        //    dir.Normalize();
        //    origin = origin + dir * groundDirectionRayDistance;

        //    targetPosition = myTransform.position;

        //    Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
        //    if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, groundLayer))
        //    {
        //        normalVector = hit.normal;
        //        Vector3 tp = hit.point;
        //        player.isGrounded = true;
        //        targetPosition.y = tp.y;

        //        if (player.isInAir)
        //        {
        //            if (inAirTimer > 0.5f)
        //            {
        //                Debug.Log("You were in the air for " + inAirTimer);
        //                playerAnimatorManager.PlayTargetAnimation("Land", true);
        //                inAirTimer = 0;
        //            }
        //            else
        //            {
        //                playerAnimatorManager.PlayTargetAnimation("Land", false);
        //                inAirTimer = 0;
        //            }

        //            player.isInAir = false;
        //        }
        //    }
        //    else
        //    {
        //        if (player.isGrounded)
        //        {
        //            player.isGrounded = false;
        //        }

        //        if (player.isInAir == false)
        //        {
        //            if (player.isInteracting == false)
        //            {
        //                playerAnimatorManager.PlayTargetAnimation("Falling", true);
        //            }

        //            Vector3 vel = rigidbody.velocity;
        //            vel.Normalize();
        //            rigidbody.velocity = vel * (movementSpeed / 2);
        //            player.isInAir = true;
        //        }
        //    }

        //    if (player.isInteracting || inputHandler.moveAmount > 0)
        //    {
        //        myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
        //    }
        //    else
        //    {
        //        myTransform.position = targetPosition;
        //    }
        //}

        public void HandleJumping()
        {
            if (player.isInteracting)
                return;

            if (playerStatsManager.currentStamina <= 0)
                return;

            if (inputHandler.jump_Input)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;
                moveDirection.Normalize();

                playerAnimatorManager.PlayTargetAnimation("Jump", false);
                player.characterController.Move(-Vector3.down);

                Quaternion jumpRotation = Quaternion.LookRotation(moveDirection * jumpHeight * Time.deltaTime);
                myTransform.rotation = jumpRotation;
            }
        }

    }
}