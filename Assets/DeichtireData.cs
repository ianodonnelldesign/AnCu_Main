using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DeichtireData : MonoBehaviour
    {

        NPCInteract deichtire;

        private void Awake()
        {
            deichtire = GetComponent<NPCInteract>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                deichtire.InteractWithNPC();
            }
        }
    }
}

