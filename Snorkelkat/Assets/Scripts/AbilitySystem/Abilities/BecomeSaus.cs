using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "BecomeSaus", menuName = "AbilitySystem/Abilities/BecomeSaus")]
public class BecomeSaus : ModAbility
{
    [Header("Normal State")]
    [SerializeField] private PlayerPhysicsStateMachine.State normalState;
    [SerializeField] private Vector2 normalColliderSize;
    [SerializeField] private Vector2 normalColliderOffset;

    [Header("Saus State")]
    [SerializeField] private PlayerPhysicsStateMachine.State sausState;
    [SerializeField] private Vector2 sausColliderSize;
    [SerializeField] private Vector2 sausColliderOffset;

    public override void ActivateAbility()
    {
        SetAbility(false);
    }

    public override void DeactivateAbility()
    {
        SetAbility(false);
    }

    public override void UpdateAbility()
    {
        if (gameManager.playerMovement.vertical < 0  && !gameManager.playerMovement.isSaus)
        {
            SetAbility(true);
        }
        else if (gameManager.playerMovement.vertical >= 0 && gameManager.playerMovement.isSaus)
        {
            SetAbility(false);
        }
    }

    private void SetAbility(bool state)
    {
        CapsuleCollider2D collider = gameManager.player.GetComponent<CapsuleCollider2D>();

        if (state == true)
        {
            gameManager.syncAudio.PlaybecomeSaus();

            gameManager.playerPhysicsStateMachine.state = sausState;

            collider.size = sausColliderSize;
            collider.offset = sausColliderOffset;

        }
        else
        {
            gameManager.playerPhysicsStateMachine.state = normalState;

            collider.size = normalColliderSize;
            collider.offset = normalColliderOffset;
        }

        gameManager.playerMovement.canWallJump = !state;
        gameManager.playerMovement.isSaus = state;
        gameManager.playerMovement.anim.SetBool("IsSaus", state);
    }
}