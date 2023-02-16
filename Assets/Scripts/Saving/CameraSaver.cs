using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CameraSaver : MonoBehaviour
    {
        public static CameraSaver Instance;
        private void Awake()
        {
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

