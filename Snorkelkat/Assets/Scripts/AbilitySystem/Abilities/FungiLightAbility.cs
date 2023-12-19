using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FungiLightAbility", menuName = "AbilitySystem/Abilities/FungiLight")]
public class FungiLightAbility : ModAbility
{
    public override void ActivateAbility()
    {
        gameManager.player.GetComponent<LightAbility>().FungiLight();
    }

    public override void DeactivateAbility()
    {
        gameManager.player.GetComponent<LightAbility>().PlayerLight();
    }

}
