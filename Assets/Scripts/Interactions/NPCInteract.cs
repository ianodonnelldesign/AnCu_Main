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
        }

        public void Start()
        {
            dialogueUI.SetActive(false);
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            InteractWithNPC();
        }

  

        public void InteractWithNPC()
        {
            
            Debug.Log("You're talking to this NPC");
            if (isTalking == false)
            {
                playerManager.isInteracting = true;
                dialogueLength = npc.npcDialogue.Length - 1;
                StartConversation();
            }
            else if (isTalking == true)
            {
                playerManager.isInteracting = false;
                EndDialogue();
            }

        }

        void StartConversation()
        {
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
            mouseLook.TurnMouseOff();
            isTalking = false;
            dialogueUI.SetActive(false);
            dialogueAdvance = 0;
            dialogueLength = 0;
        }

    }

}

