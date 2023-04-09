using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class NPCDialogueSetManager : MonoBehaviour
    {
        public NPCInteract cathbad;
        public NPCInteract king;
        public GameObject cullan;
        
        PlayerManager playerManager;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        public void ChangeDialogueSet()
        {
            if(playerManager.isTalkingTo == "Cathbad" && cathbad.currentNPCDialogueSet <= 1)
            {
                cathbad.currentNPCDialogueSet = 1;
                playerManager.talkedToCathbad = true;
                if(cathbad.currentNPCDialogueSet == 1)
                {
                    king.currentNPCDialogueSet = 1;
                }
            }
            else if (playerManager.isTalkingTo == "King Conchobar" && playerManager.talkedToCathbad)
            {
                playerManager.talkedToKing = true;
                king.currentNPCDialogueSet = 2;
                cathbad.currentNPCDialogueSet = 2;
            }
            else if (playerManager.isTalkingTo == "Cathbad" && cathbad.currentNPCDialogueSet == 2 && playerManager.talkedToKing)
            {
                cathbad.currentNPCDialogueSet = 3;
                playerManager.doneTalking = true;
                cullan.SetActive(false);
            }
        }
    }
}

