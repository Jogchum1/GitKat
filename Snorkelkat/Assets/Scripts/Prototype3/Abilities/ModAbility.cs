using Unity.VisualScripting;
using UnityEngine;

public abstract class ModAbility : ScriptableObject
{
    [HideInInspector]
    public GameManager gameManager;
    
    public void InstantiateAbility(GameManager gm)
    {
        gameManager = gm;
    }

    public abstract void ActivateAbility();
    public abstract void DeactivateAbility();
}