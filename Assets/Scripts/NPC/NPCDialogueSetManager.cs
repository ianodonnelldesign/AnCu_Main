using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SG
{
    public class NPCDialogueSetManager : MonoBehaviour
    {
        public NPCInteract cathbad;
        public NPCInteract king;
        public NPCInteract cullan;

        public GameObject cullanGameObject;
        public CullanData cullanData;

        public CinemachineVirtualCamera interactionCamera;

        public GameObject creditsTimeline;
        public GameObject gameStartTimeline;
        public GameObject deichtire;

        PlayerManager playerManager;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        public void ChangeDialogueSet()
        {
            if(playerManager.isTalkingTo == "Deichtire")
            {
                deichtire.SetActive(false);
                gameStartTimeline.SetActive(true);
            }

            if(playerManager.isTalkingTo == "Cathbad" && cathbad.currentNPCDialogueSet <= 1)
            {
                cathbad.currentNPCDialogueSet = 1;
                playerManager.talkedToCathbad = true;
                if(cathbad.currentNPCDialogueSet == 1)
                {
                    king.currentNPCDialogueSet = 1;
                }
            }
            else if (playerManager.isTalkingTo == "King Conchobar" && playerManager.talkedToCathbad && king.currentNPCDialogueSet == 1)
            {
                playerManager.talkedToKing = true;
                king.currentNPCDialogueSet = 2;
                cathbad.currentNPCDialogueSet = 2;
            }
            else if (playerManager.isTalkingTo == "Cathbad" && cathbad.currentNPCDialogueSet == 2 && playerManager.talkedToKing)
            {
                cathbad.currentNPCDialogueSet = 3;
                playerManager.doneTalking = true;
                cullanGameObject.SetActive(false);
                cullanData.blocker.enabled = false;
                cullanData.trigger.enabled = false;

            }

            if (playerManager.isTalkingTo == "Cathbad" && cathbad.currentNPCDialogueSet == 4)
            {
                playerManager.talkedToCathbadAgain = true;
            }
            if (playerManager.isTalkingTo == "King Conchobar" && king.currentNPCDialogueSet == 3)
            {
                playerManager.talkedToKingAgain = true;
            }
            if (playerManager.isTalkingTo == "Cullan" && cullan.currentNPCDialogueSet == 1)
            {
                playerManager.talkedToCullanAgain = true;
            }

            if (playerManager.talkedToCathbadAgain && playerManager.talkedToKingAgain && playerManager.talkedToCullanAgain && king.currentNPCDialogueSet == 3)
            {
                king.currentNPCDialogueSet = 4;
            }

            else if (playerManager.talkedToCathbadAgain && playerManager.talkedToKingAgain && playerManager.talkedToCullanAgain && king.currentNPCDialogueSet == 4)
            {
                creditsTimeline.SetActive(true);
            }

        }
    }
}

