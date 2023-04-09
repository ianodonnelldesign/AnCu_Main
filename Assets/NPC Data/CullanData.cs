using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

namespace SG
{
    public class CullanData : MonoBehaviour
    {
        PlayerManager player;
        NPCInteract npcInteract;

        private void Awake()
        {
            player = FindObjectOfType<PlayerManager>();
            npcInteract = GetComponent<NPCInteract>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                npcInteract.InteractWithNPC();
            }
        }
    }

}

