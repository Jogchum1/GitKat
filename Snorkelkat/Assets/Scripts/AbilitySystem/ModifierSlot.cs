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
        modifierImage.color = Color.white;
        abilityImage.sprite = modifier.activeAbility.abilitySprite;
        abilityImage.color = Color.white;
        controlsImage.sprite = modifier.activeAbility.controlsSprite;
        controlsImage.color = Color.white;
    }
}
