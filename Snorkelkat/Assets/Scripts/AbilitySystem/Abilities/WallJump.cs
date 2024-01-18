using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WallJump", menuName = "AbilitySystem/Abilities/WallJump")]
public class WallJump : ModAbility
{
    public override void ActivateAbility()
    {
        gameManager.playerMovement.canWallJump = true;
    }

    public override void DeactivateAbility()
    {
        gameManager.playerMovement.canWallJump = false;
        gameManager.playerMovement.isWallSliding = false;
        gameManager.playerMovement.anim.SetBool("IsWallSliding", false);
    }
}
