using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.SceneManagement;

public class LevelTransitioner : MonoBehaviour
{
    public Animator levelTransitioner;
    public GameObject blackFade;

    public SceneField levelToLoad;

    private void Awake()
    {
        levelTransitioner = GetComponent<Animator>();
    }
    void Start()
    {
        blackFade.SetActive(false);
    }

    public void DoLevelTransition()
    {
        blackFade.SetActive(true);
        
        PlayTransitionAnimation();
    }

    public void PlayTransitionAnimation()
    {
        levelTransitioner.SetTrigger("FadeToBlack");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void OnDestroy()
    {
        levelTransitioner.ResetTrigger("FadeToBlack");
    }

}
