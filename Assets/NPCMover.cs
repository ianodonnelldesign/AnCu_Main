using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SG
{
    public class NPCMover : MonoBehaviour
    {
        public GameObject cullan;
        public GameObject king;
        public GameObject cathbad;

        public GameObject cullanPosition;
        public GameObject kingPosition;
        public GameObject cathbadPosition;

        public NPCInteract cullanInteract;
        public NPCInteract kingInteract;
        public NPCInteract cathbadInteract;

        public CinemachineVirtualCamera timelineCamera;

        InputHandler inputHandler;
        private void Awake()
        {
            inputHandler = FindObjectOfType<InputHandler>();
        }

        private void OnEnable()
        {
            cullan.SetActive(true);

            inputHandler.interactFlag = true;

            king.transform.position = kingPosition.transform.position;
            king.transform.eulerAngles = kingPosition.transform.eulerAngles;
            kingInteract.currentNPCDialogueSet = 3;

            cullan.transform.position = cullanPosition.transform.position;
            cullan.transform.eulerAngles = cullanPosition.transform.eulerAngles;
            cullanInteract.currentNPCDialogueSet = 1;

            cathbad.transform.position = cathbadPosition.transform.position;
            cathbad.transform.eulerAngles = cathbadPosition.transform.eulerAngles;
            cathbadInteract.currentNPCDialogueSet = 4;
        }

        private void OnDisable()
        {
            timelineCamera.Priority = 0;
            inputHandler.interactFlag = false;
        }

    }
}

