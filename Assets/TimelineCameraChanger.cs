using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TimelineCameraChanger : MonoBehaviour
{
    public CinemachineVirtualCamera timelineCamera;

    private void OnEnable()
    {
        timelineCamera.Priority = 0;
    }
}
