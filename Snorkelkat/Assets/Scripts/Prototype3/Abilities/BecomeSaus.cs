using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BecomeSaus", menuName = "AbilitySystem/Abilities/BecomeSaus")]
public class BecomeSaus : ModAbility
{
    private bool isSaus;
    public float sausY = 0.5f;

    public override void ActivateAbility()
    {
    }

    public override void DeactivateAbility()
    {
        gameManager.player.transform.localScale = new Vector3(1, 1, 1);
        isSaus = false;
    }

    public override void UpdateAbility()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !isSaus)
        {
            gameManager.player.transform.localScale = new Vector3(1, sausY, 1);
            isSaus = true;
            gameManager.player.GetComponent<PlayerMovement>().isSlippery = true;
        }
        else if (!Input.GetKey(KeyCode.Mouse0) && isSaus)
        {
            gameManager.player.transform.localScale = new Vector3(1, 1, 1);
            isSaus = false;
            gameManager.player.GetComponent<PlayerMovement>().isSlippery = false;
        }
    }
}