using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using Utilities;
using UnityEngine.EventSystems;

public class VideoHandler : MonoBehaviour


{

    public GameObject skipButton;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] SceneField newGameStart;

    private void Start()
    {
        videoPlayer.loopPointReached += AutoSceneTransition;
        EventSystem.current.SetSelectedGameObject(skipButton);
    }

    void AutoSceneTransition(VideoPlayer vp)
    {
        SceneManager.LoadScene(newGameStart);
    }
}
