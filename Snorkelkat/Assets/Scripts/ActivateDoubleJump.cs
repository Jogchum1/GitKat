using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDoubleJump : MonoBehaviour, Ability
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void Activate()
    {
        playerMovement.maxJumps = 2;
    }

    public void Deactivate()
    {
        playerMovement.maxJumps = 1;
    }
}
