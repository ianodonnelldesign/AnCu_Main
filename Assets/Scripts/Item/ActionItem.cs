using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(menuName = "Items/Action Item")]
    public class ActionItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Animation Override")]
        //public AnimatorOverrideController actionItemController;

        [Header("Item Type")]
        public ItemType itemType;

        [Header("Damage")]
        public int baseDamage = 0;
        public int criticalDamageMuiltiplier = 2;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseBonus;

        [Header("Absorption")]
        public float physicalDamageAbsorption;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }
}