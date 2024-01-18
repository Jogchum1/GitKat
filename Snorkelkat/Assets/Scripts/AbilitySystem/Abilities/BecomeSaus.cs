using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BecomeSaus", menuName = "AbilitySystem/Abilities/BecomeSaus")]
public class BecomeSaus : ModAbility
{
    public float changedPlayerScaleY = 0.5f;
    [SerializeField]
    public float activationForwardSpeed;
    [SerializeField]
    private PlayerPhysicsStateMachine.State sausState;
    private CapsuleCollider2D playerCollider;
    private Vector2 colliderSize;
    private Rigidbody2D rigidbody;
    public override void ActivateAbility()
    {
        gameManager = GameManager.instance;
        Debug.Log(gameManager);
        Debug.Log("HALLO?");
        playerCollider = gameManager.player.GetComponent<CapsuleCollider2D>();
        rigidbody = gameManager.playerMovement.rb;
        colliderSize = playerCollider.size;
    }

    public override void DeactivateAbility()
    {
        SetAbility(false);
    }

    public override void UpdateAbility()
    {
        if(playerCollider == null)
        {
            playerCollider = gameManager.player.GetComponent<CapsuleCollider2D>();
            colliderSize = playerCollider.size;
        }

        if (gameManager.playerMovement.vertical < 0  && !gameManager.playerMovement.isSaus)
        {
            SetAbility(true);
            gameManager.playerMovement.canWallJump = false;
        }
        else if (gameManager.playerMovement.vertical >= 0 && gameManager.playerMovement.isSaus)
        {
            SetAbility(false);
            gameManager.playerMovement.canWallJump = true;
        }
    }

    private void SetAbility(bool state)
    {
        if (state)
        {
            gameManager.syncAudio.PlaybecomeSaus();
            playerCollider.size = new Vector2(playerCollider.size.x, changedPlayerScaleY);
            gameManager.playerPhysicsStateMachine.state = sausState;

            if (gameManager.playerMovement.IsGrounded() && rigidbody.velocity.x < activationForwardSpeed)
            {
                rigidbody.velocity = new Vector2(activationForwardSpeed * gameManager.playerMovement.aheadAmount, rigidbody.velocity.y);
            }
        }
        else
        {
            playerCollider.size = new Vector2(playerCollider.size.x, colliderSize.y);
            gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.Normal;
        }

        CenterCollider();
        gameManager.playerMovement.isSaus = state;
        gameManager.playerMovement.anim.SetBool("IsSaus", state);
    }

    private void CenterCollider()
    {
        float offsetY = (playerCollider.size.y * 0.5f) - 1;
        playerCollider.offset = new Vector2(playerCollider.offset.x, offsetY);
    }
}