using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierSlot : MonoBehaviour
{
    [SerializeField] private Image modifierImage;
    [SerializeField] private Image abilityImage;

    public void SetModifierAndAbilitySprite(ModifierScript modifier)
    {
        if (modifier == null)
        {
            modifierImage.sprite = null;
            abilityImage.sprite = null;
            return;
        }
        modifierImage.sprite = modifier.modSprite;
        abilityImage.sprite = modifier.activeAbility.abilitySprite;
    }
}
