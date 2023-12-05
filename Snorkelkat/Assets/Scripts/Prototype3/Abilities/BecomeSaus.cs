using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BecomeSaus", menuName = "AbilitySystem/Abilities/BecomeSaus")]
public class BecomeSaus : ModAbility
{
    private bool isSaus;
    public float sausY = 0.5f;

    [SerializeField]
    private PhysicsMaterial2D slipperyPhysicsMaterial2D;
    private PhysicsMaterial2D normalPhysicsMaterial2D;

    public override void ActivateAbility()
    {
        normalPhysicsMaterial2D = playerRigidbody2D.sharedMaterial;
    }

    public override void DeactivateAbility()
    {
        playerRigidbody2D.sharedMaterial = normalPhysicsMaterial2D;
        gameManager.player.transform.localScale = new Vector3(1, 1, 1);
        playerMovement.isSlippery = false;
        isSaus = false;
    }

    public override void UpdateAbility()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !isSaus)
        {
            player.transform.localScale = new Vector3(1, sausY, 1);
            playerRigidbody2D.sharedMaterial = slipperyPhysicsMaterial2D;
            playerMovement.isSlippery = true;
            isSaus = true;
        }
        else if (!Input.GetKey(KeyCode.Mouse0) && isSaus)
        {
            playerRigidbody2D.sharedMaterial = normalPhysicsMaterial2D;
            gameManager.player.transform.localScale = new Vector3(1, 1, 1);
            playerMovement.isSlippery = false;
            isSaus = false;
        }
    }
}