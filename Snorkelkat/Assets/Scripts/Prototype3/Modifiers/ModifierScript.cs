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
