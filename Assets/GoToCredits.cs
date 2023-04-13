using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.SceneManagement;

namespace SG
{
    public class GoToCredits : MonoBehaviour
    {
        public SceneField credits;

        private void OnEnable()
        {
            SceneManager.LoadScene(credits);
        }


    }
}

