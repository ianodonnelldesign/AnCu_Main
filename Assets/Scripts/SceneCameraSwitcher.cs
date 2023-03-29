using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace SG
{
    public class SceneCameraSwitcher : MonoBehaviour
    {
        public CinemachineVirtualCamera cutsceneCamera;

        CameraHandler cameraHandler;
        InputHandler inputHandler;

        public void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            inputHandler = FindObjectOfType<InputHandler>();
        }

        public void OnEnable()
        {
            SwitchToCutsceneCamera();
            inputHandler.interactFlag = true;
        }

        public void OnDisable()
        {
            SwitchToPlayerCamera();
            inputHandler.interactFlag = false;
        }

        public void SwitchToCutsceneCamera()
        {
            cutsceneCamera.Priority = 11;
        }

        public void SwitchToPlayerCamera()
        {
            cutsceneCamera.Priority = 0;
        }

    }
}


