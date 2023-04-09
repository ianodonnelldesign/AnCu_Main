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
                rig.weight = Mathf.Lerp(1, 0, Time.deltaTime * 10f);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            rig.weight = Mathf.Lerp(0, 1, Time.deltaTime * 10f);
        }
    }
}
