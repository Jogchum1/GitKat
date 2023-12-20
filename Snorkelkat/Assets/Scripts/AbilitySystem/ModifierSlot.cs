using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifierSlot : MonoBehaviour
{
    [SerializeField] private Image modifierImage;
    [SerializeField] private Image abilityImage;
    [SerializeField] private Image controlsImage;

    public void SetModifierAndAbilitySprite(ModifierScript modifier)
    {
        if (modifier == null)
        {
            modifierImage.sprite = null;
            abilityImage.sprite = null;
            controlsImage.sprite = null;
            return;
        }
        modifierImage.sprite = modifier.modSprite;
        abilityImage.sprite = modifier.activeAbility.abilitySprite;
        controlsImage.sprite = modifier.activeAbility.controlsSprite;
    }

    public void SetModifierSprite(ModifierScript modifier)
    {
        if (modifier == null)
        {
            modifierImage.sprite = null;
            return;
        }
        modifierImage.sprite = modifier.modSprite;
    }
    public void SetAbilitySprite(ModifierScript modifier)
    {
        if (modifier == null)
        {
            abilityImage.sprite = null;
            controlsImage.sprite = null;
            return;
        }
        abilityImage.sprite = modifier.activeAbility.abilitySprite;
        controlsImage.sprite = modifier.activeAbility.controlsSprite;
    }
}
