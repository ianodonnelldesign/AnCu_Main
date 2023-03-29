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
            if (other.tag == "Player")
            {
                gameObject.SetActive(this);
                Debug.Log("Started the boss fight");
                worldEventManager.ActivateBossFight();
            }
        }
    }
}