using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJump", menuName = "AbilitySystem/Abilities/DoubleJump")]
public class DoubleJump : ModAbility
{
    public int amountOfJumps = 2;

    public override void ActivateAbility()
    {
        gameManager.player.GetComponent<PlayerMovement>().maxJumps = amountOfJumps;
    }

    public override void DeactivateAbility()
    {
        gameManager.player.GetComponent<PlayerMovement>().maxJumps = 1;
    }
}
