using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WallJump", menuName = "AbilitySystem/Abilities/WallJump")]
public class WallJump : ModAbility
{
    public override void ActivateAbility()
    {
        gameManager.player.GetComponent<PlayerMovement>().canWallJump = true;
    }

    public override void DeactivateAbility()
    {
        gameManager.player.GetComponent<PlayerMovement>().canWallJump = false;
    }
}
