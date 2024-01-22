using Unity.VisualScripting;
using UnityEngine;

public abstract class ModAbility : ScriptableObject
{
    public Sprite abilitySprite;
    public Sprite controlsSprite;
    [HideInInspector]
    public GameManager gameManager;
    public void InstantiateAbility(GameManager gm)
    {
        gameManager = GameManager.instance;
    }

    public abstract void ActivateAbility();
    public abstract void DeactivateAbility();

    public virtual void UpdateAbility()
    {

    }
}