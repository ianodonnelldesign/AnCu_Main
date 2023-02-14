using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.loopPointReached += SceneTransition;
    }


    void SceneTransition(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
