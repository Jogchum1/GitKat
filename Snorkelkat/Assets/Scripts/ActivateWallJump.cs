using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWallJump : MonoBehaviour, Ability
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void Activate()
    {
        playerMovement.canWallJump = true;
    }

    public void Deactivate()
    {
        playerMovement.canWallJump = false;
    }
}
