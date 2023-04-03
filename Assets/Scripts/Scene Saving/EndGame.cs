using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class EndGame : MonoBehaviour
{
    public SceneField mainMenu;

    public void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Destroy(GameObject.Find("Camera Holder"));
        Destroy(GameObject.Find("Player Setanta"));
        Destroy(GameObject.Find("PlayerUI"));
        Destroy(GameObject.Find("AudioManager"));

        ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
