using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayUlstermen : MonoBehaviour
    {
        private void OnEnable()
        {
            AudioManager.Instance.PlayMusic("Ulstermen");
        }
    }
}


