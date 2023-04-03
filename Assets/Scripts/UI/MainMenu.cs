using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator mainMenuAnimator;
    public void PlayButtonPress()
    {
        mainMenuAnimator.SetBool("PlayGame", true);
    }

    public void QuitButtonPress()
    {
        //Don't do anything for this build
        //mainMenuAnimator.SetBool("QuitGame", true);
        Debug.Log("You closed the game");
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }

    public void OnDestroy()
    {
        mainMenuAnimator.SetBool("PlayGame", false);
        mainMenuAnimator.SetBool("QuitGame", false);
    }

}
