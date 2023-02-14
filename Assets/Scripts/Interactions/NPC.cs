using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SG

{
    [CreateAssetMenu(fileName = "NPC File", menuName = "NPC Files Archive")]

    public class NPC : ScriptableObject
    {
        public string npcName;

        [TextArea(3, 15)]
        public string[] npcDialogue;
        
        [TextArea(3, 15)]
        public string[] playerDialogue;

        

    }

}