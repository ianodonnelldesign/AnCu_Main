using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using Utilities;

public class VideoHandler : MonoBehaviour
{
    public Button skipIntro;

    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] SceneField newGameStart;

    private void Start()
    {
        videoPlayer.loopPointReached += AutoSceneTransition;
    }

    void AutoSceneTransition(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
