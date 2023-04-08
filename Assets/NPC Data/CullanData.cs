using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

namespace SG
{
    public class CullanData : NPCInteract
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                InteractWithNPC();
            }
        }
        //if something is true, remove cullan so the player can get by

    }

}

