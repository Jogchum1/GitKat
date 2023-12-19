using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGiver : MonoBehaviour
{
    public GameManager gameManager;
    public ModifierScript modifierToGive;
    public void ActivateAbility()
    {
        Debug.Log("ACTIVATE ABILITY");
        FindObjectOfType<AbilitySwitcher>().enabled = true;
    }

    public void GiveTopModifier()
    {
        gameManager.abilitySwitcher.topModifier = modifierToGive;
    }

    public void GiveBottomModifier()
    {
        gameManager.abilitySwitcher.bottomModifier = modifierToGive;
    }
}
