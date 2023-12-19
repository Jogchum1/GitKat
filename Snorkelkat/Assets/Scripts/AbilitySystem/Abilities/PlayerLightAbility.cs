using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLightAbility", menuName = "AbilitySystem/Abilities/PlayerLight")]
public class PlayerLightAbility : ModAbility
{
    public override void ActivateAbility()
    {
        gameManager.player.GetComponent<LightAbility>().PlayerLight();
    }

    public override void DeactivateAbility()
    {
        gameManager.player.GetComponent<LightAbility>().FungiLight();
    }
}
