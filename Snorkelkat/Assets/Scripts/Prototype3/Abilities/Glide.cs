using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Glide", menuName = "AbilitySystem/Abilities/Glide")]

public class Glide : ModAbility
{
    public override void ActivateAbility()
    {
        gameManager.player.GetComponent<PlayerMovement>().canGlide = true;
    }

    public override void DeactivateAbility()
    {
        gameManager.player.GetComponent<PlayerMovement>().canGlide = false;
    }


}
