using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.SceneManagement;

namespace SG
{

    public class SceneExit : MonoBehaviour
    {
        public SceneField sceneToLoad;
        public string exitName;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                SceneManager.LoadScene(sceneToLoad);
                PlayerPrefs.SetString("LastExitName", exitName);
            }
            else { return; }
        }

    }
}

