using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BecomeSaus", menuName = "AbilitySystem/Abilities/BecomeSaus")]
public class BecomeSaus : ModAbility
{
    public float changedPlayerScaleY = 0.5f;
    [SerializeField]
    private PlayerPhysicsStateMachine.State sausState;
    private CapsuleCollider2D playerCollider;
    private Vector2 colliderSize;
    public override void ActivateAbility()
    {
        Debug.Log(gameManager.name);
        playerCollider = gameManager.player.GetComponent<CapsuleCollider2D>();
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
        if(gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (Input.GetKey(KeyCode.Mouse0) && !gameManager.playerMovement.isSaus)
        {
            playerCollider.size = new Vector2(playerCollider.size.x, changedPlayerScaleY);
            CenterCollider();

            gameManager.playerPhysicsStateMachine.state = sausState;
            gameManager.playerMovement.isSaus = true;
            gameManager.playerMovement.anim.SetBool("IsSaus", true);
        }
        else if (!Input.GetKey(KeyCode.Mouse0) && gameManager.playerMovement.isSaus)
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