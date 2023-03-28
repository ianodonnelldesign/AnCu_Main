using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace SG
{
    public class SceneEntrance : MonoBehaviour
    {
        public string lastExitName;

        void Start()
        {
            if(PlayerPrefs.GetString("LastExitName") == lastExitName)
            {
                //PlayerSaver.Instance.transform.eulerAngles = transform.eulerAngles;

                PlayerSaver.Instance.transform.position = transform.position;
            }
        }


    }
}

