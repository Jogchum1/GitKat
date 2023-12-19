using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaddoJump", menuName = "AbilitySystem/Abilities/PaddoJump")]
public class PaddoJump : ModAbility
{
    public GameObject paddo;
    
    public override void ActivateAbility()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (gameManager.playerMovement.jumpingPaddo == null)
        {
            gameManager.playerMovement.jumpingPaddo = paddo;
        }

        gameManager.playerMovement.canPaddoJump = true;
    }

    public override void DeactivateAbility()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        gameManager.playerMovement.canPaddoJump = false;
    }
}