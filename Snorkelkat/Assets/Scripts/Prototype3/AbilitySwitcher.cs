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
    [SerializeField] private Animator animator;

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
        updateModifierSlotsSprites();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
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
            updateModifierSlotsSprites(switchTimeSeconds);
            return;
        }

        if (bottomModifier == null)
        {
            topModifier.SwitchToBottom();
            bottomModifier = topModifier;
            topModifier = null;
            updateModifierSlotsSprites(switchTimeSeconds);
            return;
        }

        topModifier.SwitchToBottom();
        bottomModifier.SwitchToTop();
        ModifierScript newTopMod = bottomModifier;
        ModifierScript newBottomMod = topModifier;
        topModifier = newTopMod;
        bottomModifier = newBottomMod;
        updateModifierSlotsSprites(switchTimeSeconds);
    }


    private void updateModifierSlotsSprites()
    {
        //changes order of children to keep ability on top in UI
        Transform lastChild;
        if (topModifierSlot.gameObject.transform.GetSiblingIndex() != 0)
        {
            lastChild = topModifierSlot.gameObject.transform;
        }
        else
        {
            lastChild = bottomModifierSlot.gameObject.transform;
        }
        lastChild.SetAsFirstSibling();

        //switch sprites
        if (topModifier != null)
        {
            topModifierSlot.SetModSprite(topModifier.modSprite);
        }
        else { topModifierSlot.SetModSprite(null); }

        if (bottomModifier != null) 
        {
            bottomModifierSlot.SetModSprite(bottomModifier.modSprite);
        }
        else { bottomModifierSlot.SetModSprite(null); }
    }

    private void updateModifierSlotsSprites(float animateSlotsForSeconds)
    {
        StopAllCoroutines();
        StartCoroutine(showModSlots(switchTimeSeconds));
    }

    private IEnumerator showModSlots(float showForSeconds)
    {
        //topModifierSlot.gameObject.SetActive(true);
        //bottomModifierSlot.gameObject.SetActive(true);
        animator.speed = 1 / showForSeconds;
        animator.SetTrigger("modSlotChange");
        yield return new WaitForSeconds(showForSeconds / 2);
        updateModifierSlotsSprites();
        yield return new WaitForSeconds(showForSeconds/2);
        //topModifierSlot.gameObject.SetActive(false);
        //bottomModifierSlot.gameObject.SetActive(false);
    }

}
