using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EventColliderBeginBossFight : MonoBehaviour
    {
        WorldEventManager worldEventManager;
        bool fightStarted = false;

        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (fightStarted == false)
                {
                    fightStarted = true;

                    gameObject.SetActive(this);
                    Debug.Log("Started the boss fight");
                    worldEventManager.ActivateBossFight();

                    AudioManager.Instance.StartCoroutine(AudioManager.Instance.PlayMusicSequence("GuardLoopIn", "GuardLoop"));
                }
                else
                return;
            }
        }
    }
}