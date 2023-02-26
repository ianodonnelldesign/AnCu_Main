using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SG
{
    public class WorldItemDataBase : MonoBehaviour
    {
        public static WorldItemDataBase Instance;

        public List <ActionItem> ActionItems = new List<ActionItem>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public ActionItem GetWeaponItemByID(int weaponID)
        {
            return ActionItems.FirstOrDefault(weapon => weapon.itemID == weaponID);
        }
    }
}
