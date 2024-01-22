using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        colliderSize = gameManager.player.GetComponent<CapsuleCollider2D>().size;
    }

    public override void DeactivateAbility()
    {
        SetAbility(false);
        //colliderSize = gameManager.player.GetComponent<CapsuleCollider2D>().size;
        gameManager.player.GetComponent<CapsuleCollider2D>().size = new Vector2(0.75f, 2);
        gameManager.player.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0);
        CenterCollider();
    }

    public override void UpdateAbility()
    {
        playerCollider = gameManager.player.GetComponent<CapsuleCollider2D>();
        CenterCollider();
        //colliderSize = playerCollider.size;

        if (gameManager.playerMovement.vertical < 0  && !gameManager.playerMovement.isSaus)
        {
            SetAbility(true);
            gameManager.player.GetComponent<CapsuleCollider2D>().size = new Vector2(0.75f, 0);
            gameManager.player.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -0.6f);
            gameManager.playerMovement.canWallJump = false;
        }
        else if (gameManager.playerMovement.vertical >= 0 && gameManager.playerMovement.isSaus)
        {
            SetAbility(false);
            gameManager.player.GetComponent<CapsuleCollider2D>().size = new Vector2(0.75f, 2);
            gameManager.player.GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0);
            gameManager.playerMovement.canWallJump = true;
        }
    }

    private void SetAbility(bool state)
    {
        if (state == true)
        {
            gameManager.syncAudio.PlaybecomeSaus();
            gameManager.player.GetComponent<CapsuleCollider2D>().size = new Vector2(gameManager.player.GetComponent<CapsuleCollider2D>().size.x, changedPlayerScaleY);
            CenterCollider();
            gameManager.playerPhysicsStateMachine.state = sausState;

            if (gameManager.playerMovement.IsGrounded() && rigidbody.velocity.x < activationForwardSpeed)
            {
                rigidbody.velocity = new Vector2(activationForwardSpeed * gameManager.playerMovement.aheadAmount, rigidbody.velocity.y);
            }
        }
        else
        {
            gameManager.player.GetComponent<CapsuleCollider2D>().size = new Vector2(gameManager.player.GetComponent<CapsuleCollider2D>().size.x, colliderSize.y);
            CenterCollider();
            gameManager.playerPhysicsStateMachine.state = PlayerPhysicsStateMachine.State.Normal;
        }

        gameManager.playerMovement.isSaus = state;
        gameManager.playerMovement.anim.SetBool("IsSaus", state);
    }

    private void CenterCollider()
    {
        float offsetY = (gameManager.player.GetComponent<CapsuleCollider2D>().size.y * 0.5f) - 1;
        gameManager.player.GetComponent<CapsuleCollider2D>().offset = new Vector2(gameManager.player.GetComponent<CapsuleCollider2D>().offset.x, offsetY);
    }
}