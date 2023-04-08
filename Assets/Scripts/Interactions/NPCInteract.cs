using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

namespace SG
{
    public class NPCInteract : Interactable
    {
        InputHandler inputHandler;
        public PlayerManager playerManager;
        MouseLook mouseLook;
        CameraHandler cameraHandler;
        CinematicBars cinematicBars;
        

        NPC npc;
        public NPCDialogueList npcDialogueList;
        public int currentNPCDialogueSet;

        bool isTalking = false;

        float distance;
        // private float currentResponseTracker = 0;
        public int dialogueAdvance = 0;
        public int dialogueLength = 0;

        public GameObject player;
        public GameObject dialogueUI;
        GameObject npcLockOn;

        public TextMeshProUGUI npcName;
        public TextMeshProUGUI npcDialogueBox;
        public Button playerResponse;

        public CinemachineVirtualCamera interactionCamera;

        protected override void Awake()
        {
            npcLockOn = this.transform.GetChild(0).gameObject;


            playerResponse.onClick.AddListener(AdvanceDialogue);
            playerManager = FindObjectOfType<PlayerManager>();
            mouseLook = FindObjectOfType<MouseLook>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            cinematicBars = FindObjectOfType<CinematicBars>();
            inputHandler = FindObjectOfType<InputHandler>();
        }

        public void start()
        {
            dialogueUI.SetActive(false);
        }

        private void Update()
        {
            npc = npcDialogueList.npcDialogues[currentNPCDialogueSet];
        }
        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            InteractWithNPC();
        }


        public void InteractWithNPC()
        {
            if (isTalking == false)
            {
                interactionCamera.Priority = 11;
                interactionCamera.LookAt = npcLockOn.transform;

                LookAtNPC();

                inputHandler.interactFlag = true;
                playerManager.isInteracting = true;

                dialogueLength = npc.npcDialogue.Length - 1;
                StartConversation();
            }
            else if (isTalking == true)
            {
                EndDialogue();
            }
        }

        public void LookAtNPC()
        {
           Debug.Log(inputHandler);
           if(inputHandler.lockOnFlag == false)
            {
                cameraHandler.HandleLockOn();
                if (cameraHandler.nearestLockOnTarget != null && cameraHandler.nearestLockOnTarget.GetComponentInParent<NPCInteract>() == true)
                {
                    //size of bar, time to get to that size
                    cinematicBars.ShowCinematicBars(180, .3f);
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    inputHandler.lockOnFlag = true;
                }
            }
        }

        void StartConversation()
        {
            cinematicBars.ShowCinematicBars(180, .3f);

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
                dialogueAdvance += 1;
                npcDialogueBox.text = npc.npcDialogue[dialogueAdvance];
            }
            else if (isTalking == true)
            { 
                EndDialogue(); 
            }
        }
        public void EndDialogue()
        {
            if (playerManager.talkedToCathbad)
            {
                npcDialogueList.currentNPCDialogueSet += 1;
            }

            interactionCamera.Priority = 0;

            cinematicBars.HideCinematicBars(.3f);

            mouseLook.TurnMouseOff();
            isTalking = false;

            dialogueUI.SetActive(false);
            dialogueAdvance = 0;
            dialogueLength = 0;

            inputHandler.lockOnInput = false;
            inputHandler.lockOnFlag = false;
            inputHandler.interactFlag = false;

            cameraHandler.ClearLockOnTargets();
        }

        public void ChangeDialogueSet()
        {

        }

    }

}
