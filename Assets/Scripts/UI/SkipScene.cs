using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class SkipScene : MonoBehaviour
{
    public void SkipCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Loaded cutscene");
    }

    public void Main()
    {
        SceneManager.LoadScene("Level_01_MainMenu");
    }
}
