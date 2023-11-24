using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuControls : MonoBehaviour
{
    public GameObject controlsCanvas;
    public GameObject backButton;
    public GameObject controlsButton;

    public void ControlsPressed()
    {
        controlsCanvas.SetActive(true);

        //EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void BackPressed()
    {
        controlsCanvas.SetActive(false);

        //EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(controlsButton);
    }
}
