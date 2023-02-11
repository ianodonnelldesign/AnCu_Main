using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace SG
{
    public class LookRange : MonoBehaviour
    {
        private Rig rig;

        private void Awake()
        {
            rig = GetComponent<Rig>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Player entered the look range of NPC");
                rig.weight = Mathf.Lerp(1, 0, Time.deltaTime * 10f);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Player left the look range of NPC");
            rig.weight = Mathf.Lerp(0, 1, Time.deltaTime * 10f);
        }
    }
}
