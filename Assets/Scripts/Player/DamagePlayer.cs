using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class DamagePlayer : MonoBehaviour
    {
        public int fireDamage;

        private void OnTriggerEnter(Collider other)
        {
            PlayerStatsManager playerStats = other.GetComponentInParent<PlayerStatsManager>();

            if (playerStats != null)
            {
                Debug.Log("Player took fire damage");
                playerStats.TakeDamage(fireDamage);
            }
        }
    }
}