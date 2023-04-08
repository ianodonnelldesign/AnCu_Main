using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName = "NPC Dialogues", menuName = "NPC/NPC Dialogue")]
    public class NPCDialogueList : ScriptableObject
    {
        public int currentNPCDialogueSet;

        public NPC[] npcDialogues;
    }
}
