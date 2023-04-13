using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SG
{
    public class EventColliderBeginBossFight : MonoBehaviour
    {
        public GameObject startFightTimeline;
        public CinemachineVirtualCamera timelineCamera;

        WorldEventManager worldEventManager;
        bool fightStarted = false;

        private void Awake()
        {
            worldEventManager = FindObjectOfType<WorldEventManager>();
            startFightTimeline.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                startFightTimeline.SetActive(true);
                timelineCamera.Priority = 11;

                if (fightStarted == false)
                {
                    fightStarted = true;

                    gameObject.SetActive(false);
                    worldEventManager.ActivateBossFight();

                    AudioManager.Instance.StartCoroutine(AudioManager.Instance.PlayMusicSequence("GuardLoopIn", "GuardLoop"));
                }
                else
                return;
            }
        }
    }
}