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

        [Header("Damage")]
        public int baseDamage = 25;
        public int criticalDamageMuiltiplier = 4;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseBonus;

        [Header("Absorption")]
        public float physicalDamageAbsorption;

        [Header("Idle Animations")]
        public string right_hand_idle;
        public string left_hand_idle;
        public string th_idle;

        [Header("Action Animations")]
        public string light_action_01;
        public string light_action_02;
        
        public string heavy_action_01;

        public string th_action_01;
        public string th_action_02;

        [Header("Item Art")]
        public string item_art;

        [Header("Stamina Costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;

        [Header("Item Type")]
        public bool isMeleeWeapon;
        public bool isShieldItem;

        public bool isConsumable;
        public bool isGearItem;

    }
}