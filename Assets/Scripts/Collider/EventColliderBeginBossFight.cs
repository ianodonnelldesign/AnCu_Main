using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EventColliderBeginBossFight : MonoBehaviour
    {
        WorldEventManager worldEventManager;

        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Walked into boss wall");

            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Started the boss fight");
                worldEventManager.ActivateBossFight();
            }
        }
    }
}