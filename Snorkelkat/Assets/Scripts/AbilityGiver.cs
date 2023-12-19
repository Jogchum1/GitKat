using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGiver : MonoBehaviour
{
    private GameManager gameManager;
    public ModifierScript modifierToGive;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ActivateAbility()
    {
        Debug.Log("ACTIVATE ABILITY");
        FindObjectOfType<AbilitySwitcher>().enabled = true;
    }

    public void GiveModifier()
    {
        if(gameManager.abilitySwitcher.topModifier == null)
        {
            gameManager.abilitySwitcher.topModifier = modifierToGive;
        }else if(gameManager.abilitySwitcher.bottomModifier == null)
        {
            gameManager.abilitySwitcher.bottomModifier = modifierToGive;
        }
    }
}
