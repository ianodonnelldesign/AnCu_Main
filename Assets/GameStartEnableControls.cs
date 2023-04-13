using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class GameStartEnableControls : MonoBehaviour
    {
        InputHandler inputHandler;
        private void Awake()
        {
            inputHandler = FindObjectOfType<InputHandler>();
        }

        private void OnEnable()
        {
            inputHandler.interactFlag = false;
        }
    }
}

