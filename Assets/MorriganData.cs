using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class MorriganData : MonoBehaviour
    {
        public GameObject spawn1;
        public GameObject spawn2;
        public GameObject spawn3;
        public GameObject spawn4;
        public GameObject spawn5;

        NPCInteract morrigan;
        PlayerManager player;

        private void Awake()
        {
            morrigan = GetComponent<NPCInteract>();
            player = FindObjectOfType<PlayerManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                if (morrigan.currentNPCDialogueSet == 0)
                {
                    morrigan.InteractWithNPC();
                }
                else if (morrigan.currentNPCDialogueSet == 1)
                {
                    morrigan.InteractWithNPC();
                }
                else if (morrigan.currentNPCDialogueSet == 2)
                {
                    morrigan.InteractWithNPC();
                }
                else if (morrigan.currentNPCDialogueSet == 3)
                {
                    morrigan.InteractWithNPC();
                }
                else if (morrigan.currentNPCDialogueSet == 4)
                {
                    morrigan.InteractWithNPC();
                }
            }

        }

        public void MoveMorrigan()
        {
            if(morrigan.currentNPCDialogueSet == 0 && player.isTalkingTo == "Morrigan")
            {
                morrigan.currentNPCDialogueSet += 1;
                transform.position = spawn1.transform.position;
                transform.eulerAngles = spawn1.transform.eulerAngles;
            }
            else if(morrigan.currentNPCDialogueSet == 1 && player.isTalkingTo == "Morrigan")
            {
                morrigan.currentNPCDialogueSet += 1;
                transform.position = spawn2.transform.position;
                transform.eulerAngles = spawn2.transform.eulerAngles;
            }
            else if (morrigan.currentNPCDialogueSet == 2 && player.isTalkingTo == "Morrigan")
            {
                morrigan.currentNPCDialogueSet += 1;
                transform.position = spawn3.transform.position;
                transform.eulerAngles = spawn3.transform.eulerAngles;
            }
            else if (morrigan.currentNPCDialogueSet == 3 && player.isTalkingTo == "Morrigan")
            {
                morrigan.currentNPCDialogueSet += 1;
                transform.position = spawn4.transform.position;
                transform.eulerAngles = spawn4.transform.eulerAngles;
            }
            else if (morrigan.currentNPCDialogueSet == 4 && player.isTalkingTo == "Morrigan")
            {
                morrigan.currentNPCDialogueSet += 1;
                transform.position = spawn5.transform.position;
                transform.eulerAngles = spawn5.transform.eulerAngles;
            }
            else if (morrigan.currentNPCDialogueSet == 5)
            {
                return;
            }
        }

    }
}

