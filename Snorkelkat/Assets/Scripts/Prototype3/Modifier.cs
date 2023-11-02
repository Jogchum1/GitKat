using UnityEngine;

[CreateAssetMenu(fileName = "Modifier", menuName = "ScriptableObjects/AbilitySystem/Modifier", order = 1)]

public class Modifier : ScriptableObject
{
    public string modName;
    public Sprite modSprite;
    [SerializeField]
    public Ability topAbility;
    [SerializeField]
    private ModAbility bottomAbility;

    public void SwitchToTop()
    {
        topAbility.Deactivate();
        bottomAbility.Activate();
    }

    public void SwitchToBottom()
    {
        topAbility.Activate();
        topAbility.Deactivate();
    }
}
