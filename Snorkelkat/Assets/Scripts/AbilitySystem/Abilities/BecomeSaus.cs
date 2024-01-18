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
        playerCollider.size = colliderSize;
        gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.Normal;
        gameManager.playerMovement.isSaus = false;
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
            gameManager.syncAudio.PlaybecomeSaus();
            playerCollider.size = new Vector2(playerCollider.size.x, changedPlayerScaleY);
            CenterCollider();

            if (gameManager.playerMovement.IsGrounded() && rigidbody.velocity.x < activationForwardSpeed)
            {
                rigidbody.velocity = new Vector2(activationForwardSpeed * gameManager.playerMovement.aheadAmount, rigidbody.velocity.y);
            }
            gameManager.playerPhysicsStateMachine.state = sausState;
            gameManager.playerMovement.isSaus = true;
            gameManager.playerMovement.anim.SetBool("IsSaus", true);
        }
        else if (gameManager.playerMovement.vertical >= 0 && gameManager.playerMovement.isSaus)
        {
            playerCollider.size = new Vector2(playerCollider.size.x, colliderSize.y);
            CenterCollider();

            gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.Normal;
            gameManager.playerMovement.isSaus = false;
            gameManager.playerMovement.anim.SetBool("IsSaus", false);
        }
    }

    private void CenterCollider()
    {
        float offsetY = (playerCollider.size.y * 0.5f) - 1;
        playerCollider.offset = new Vector2(playerCollider.offset.x, offsetY);
    }
}