using UnityEngine;

[CreateAssetMenu(fileName = "Modifier", menuName = "AbilitySystem/Modifier", order = 1)]

public class ModifierScript : ScriptableObject
{
    public string modName;
    public Sprite modSprite;
    [SerializeField]
    private ModAbility topAbility;
    [SerializeField]
    private ModAbility bottomAbility;
    private ModAbility activeAbility;

    public void InstatiateAbilities(GameManager gameManager)
    {
        topAbility.InstantiateAbility(gameManager);
        bottomAbility.InstantiateAbility(gameManager);
    }

    public void UpdateActiveAbility()
    {
        activeAbility.UpdateAbility();
    }

    public void SwitchToTop()
    {
        bottomAbility.DeactivateAbility();
        topAbility.ActivateAbility();
        activeAbility = topAbility;
    }

    public void SwitchToBottom()
    {
        topAbility.DeactivateAbility();
        bottomAbility.ActivateAbility();
        activeAbility = bottomAbility;
    }
}
