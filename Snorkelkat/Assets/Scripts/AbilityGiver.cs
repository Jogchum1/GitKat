using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGiver : MonoBehaviour
{
    public void ActivateAbility()
    {
        Debug.Log("ACTIVATE ABILITY");
        FindObjectOfType<AbilitySwitcher>().enabled = true;
    }
}
