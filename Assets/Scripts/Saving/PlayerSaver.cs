using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerSaver : MonoBehaviour
    {
        public static PlayerSaver Instance;

        private void Awake()
        {
            PlayerPrefs.SetString("LastExitName", "Spawned");

            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}

