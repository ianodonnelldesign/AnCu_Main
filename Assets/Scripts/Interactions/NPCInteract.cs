using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class NPCInteract : Interactable
    {
        InteractingWithNPC interactingWithNPC;
        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            InteractWithNPC();
        }

        public void InteractWithNPC()
        {
            interactingWithNPC.Interaction();
        }

    }

}

