using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySwitcher : MonoBehaviour
{
    [SerializeField] private ModifierScript topModifier;
    [SerializeField] private ModifierScript bottomModifier;

    private void Start()
    {
        GameManager gameManager = GameManager.instance;
        if (topModifier != null)
        {
            topModifier.InstatiateAbilities(gameManager);
            topModifier.SwitchToTop();
        }
        if (bottomModifier != null)
        {
            bottomModifier.InstatiateAbilities(gameManager);
            bottomModifier.SwitchToBottom();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchModifierPos();
        }
    }

    public void SwitchModifierPos()
    {
        if (topModifier == null && bottomModifier == null)
        {
            return;
        }

        if (topModifier == null)
        {
            bottomModifier.SwitchToTop();
            topModifier = bottomModifier;
            bottomModifier = null;
            return;
        }

        if (bottomModifier == null)
        {
            topModifier.SwitchToBottom();
            bottomModifier = topModifier;
            topModifier = null;
            return;
        }

        topModifier.SwitchToBottom();
        bottomModifier.SwitchToTop();
        ModifierScript newTopMod = bottomModifier;
        ModifierScript newBottomMod = topModifier;
        topModifier = newTopMod;
        bottomModifier = newBottomMod;
    }

}
