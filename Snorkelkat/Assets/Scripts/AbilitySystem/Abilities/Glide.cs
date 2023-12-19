using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Glide", menuName = "AbilitySystem/Abilities/Glide")]

public class Glide : ModAbility
{
    public override void ActivateAbility()
    {
        gameManager.playerMovement.canGlide = true;
    }

    public override void DeactivateAbility()
    {
        gameManager.playerMovement.canGlide = false;
    }


}
