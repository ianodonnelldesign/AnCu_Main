using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
public class SkyboxFader : MonoBehaviour
{
        Material currentSkybox;
        public Material targetSkybox;
        public GameObject currentLights;
        public GameObject newLights;

        private void Awake()
        {
            currentSkybox = RenderSettings.skybox;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                RenderSettings.skybox = targetSkybox;

                newLights.SetActive(true);
                currentLights.SetActive(false);
            }
        }
}

}