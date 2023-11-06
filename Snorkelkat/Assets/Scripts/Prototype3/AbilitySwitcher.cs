using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySwitcher : MonoBehaviour
{
    [SerializeField] private ModifierScript topModifier;
    [SerializeField] private ModifierScript bottomModifier;
    [SerializeField] private ModifierSlot topModifierSlot;
    [SerializeField] private ModifierSlot bottomModifierSlot;
    [SerializeField] private float switchTimeSeconds;

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
        setModSlots();
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
            setModSlots(switchTimeSeconds);
            return;
        }

        if (bottomModifier == null)
        {
            topModifier.SwitchToBottom();
            bottomModifier = topModifier;
            topModifier = null;
            setModSlots(switchTimeSeconds);
            return;
        }

        topModifier.SwitchToBottom();
        bottomModifier.SwitchToTop();
        ModifierScript newTopMod = bottomModifier;
        ModifierScript newBottomMod = topModifier;
        topModifier = newTopMod;
        bottomModifier = newBottomMod;
        setModSlots(switchTimeSeconds);
    }


    private void setModSlots()
    {
        if (topModifier != null) { topModifierSlot.SetModSprite(topModifier.modSprite); }
        else { topModifierSlot.SetModSprite(null); }
        if (bottomModifier != null) { bottomModifierSlot.SetModSprite(bottomModifier.modSprite); }
        else { bottomModifierSlot.SetModSprite(null); }
    }

    private void setModSlots(float animateSlotsForSeconds)
    {
        StartCoroutine(showModSlots(switchTimeSeconds));
    }

    private IEnumerator showModSlots(float showForSeconds)
    {
        topModifierSlot.gameObject.SetActive(true);
        bottomModifierSlot.gameObject.SetActive(true);
        yield return new WaitForSeconds(showForSeconds / 2);
        setModSlots();
        yield return new WaitForSeconds(showForSeconds / 2);
        topModifierSlot.gameObject.SetActive(false);
        bottomModifierSlot.gameObject.SetActive(false);
    }

}
