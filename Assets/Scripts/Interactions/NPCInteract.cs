using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SG
{
    public class NPCInteract : Interactable
    {
        InputHandler inputHandler;
        PlayerManager playerManager;
        MouseLook mouseLook;
        CameraHandler cameraHandler;
        CinematicBars cinematicBars;

        public NPC npc;

        bool isTalking = false;

        float distance;
        // private float currentResponseTracker = 0;
        public int dialogueAdvance = 0;
        public int dialogueLength = 0;

        public GameObject player;
        public GameObject dialogueUI;

        public TextMeshProUGUI npcName;
        public TextMeshProUGUI npcDialogueBox;
        public Button playerResponse;

      public void Awake()
        {
            playerResponse.onClick.AddListener(AdvanceDialogue);
            playerManager = FindObjectOfType<PlayerManager>();
            mouseLook = FindObjectOfType<MouseLook>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            inputHandler = FindObjectOfType<InputHandler>();
            cinematicBars = FindObjectOfType<CinematicBars>();
        }

        public void Start()
        {
            dialogueUI.SetActive(false);
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            InteractWithNPC();
            LookAtNPC();
        }


        public void InteractWithNPC()
        {
            Debug.Log("You're talking to this NPC");
            if (isTalking == false)
            {
                inputHandler.interactFlag = true;
                playerManager.isInteracting = true;

                dialogueLength = npc.npcDialogue.Length - 1;
                StartConversation();
            }
            else if (isTalking == true)
            {
                inputHandler.interactFlag = false;

                isTalking = false;
                cinematicBars.HideCinematicBars(.3f);
                mouseLook.TurnMouseOff();


                dialogueUI.SetActive(false);
                dialogueAdvance = 0;
                dialogueLength = 0;

                inputHandler.lockOnInput = false;
                inputHandler.lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();

                EndDialogue();
            }

        }

        public void LookAtNPC()
        {
            Debug.Log("Looking at NPC");
            //if (inputHandler.lockOnFlag == false)
            //{
            //    inputHandler.lockOnInput = false;
            //    cameraHandler.HandleLockOn();
            //    //might need logic in the if so I don't accidentally lock onto an enemy?
            //    if (cameraHandler.nearestLockOnTarget != null)
            //    {
            //        cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
            //        inputHandler.lockOnFlag = true;
            //    }
            //}
        }

        void StartConversation()
        {
            cinematicBars.ShowCinematicBars(200, .3f);

            mouseLook.TurnMouseOn();
            npcDialogueBox.text = npc.npcDialogue[0];
            isTalking = true;
            // currentResponseTracker = 0;
            dialogueUI.SetActive(true);
            npcName.text = npc.npcName;
        }
        public void AdvanceDialogue()
        {
            if (dialogueLength > dialogueAdvance)
            {
                dialogueAdvance++;
                npcDialogueBox.text = npc.npcDialogue[dialogueAdvance];
            }
            else
            {
                EndDialogue();
            }
        }
        void EndDialogue()
        {
            cinematicBars.HideCinematicBars(.3f);

            mouseLook.TurnMouseOff();
            isTalking = false;

            dialogueUI.SetActive(false);
            dialogueAdvance = 0;
            dialogueLength = 0;

            inputHandler.lockOnInput = false;
            inputHandler.lockOnFlag = false;
            cameraHandler.ClearLockOnTargets();

            inputHandler.interactFlag = false;
        }

    }

}

