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
    private bool switching = false;

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
        updateModifierSlotSprites();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R Pressed");
            if (!switching)
            {
                SwitchModifierPos();
            }
        }
        bottomModifier?.UpdateActiveAbility();
        topModifier?.UpdateActiveAbility();
    }

    public void SwitchModifierPos()
    {
        //returns if there are no modifiers
        if (topModifier == null && bottomModifier == null)
        {
            Debug.Log("returned from SwitchModifierPos");
            return;
        }

        switching = true;

        if (topModifier == null)
        {
            Debug.Log("topmodifier null starting");
            bottomModifier.SwitchToTop();
            topModifier = bottomModifier;
            bottomModifier = null;
            animateAndUpdateModSlotSprites(switchTimeSeconds);
            Debug.Log("topmodifier null finished");
            return;
        }

        if (bottomModifier == null)
        {
            Debug.Log("bottommodifier null starting");
            topModifier.SwitchToBottom();
            bottomModifier = topModifier;
            topModifier = null;
            animateAndUpdateModSlotSprites(switchTimeSeconds);
            Debug.Log("bottommodifier null finished");
            return;
        }

        Debug.Log("modifier switch starting");
        topModifier.SwitchToBottom();
        bottomModifier.SwitchToTop();
        ModifierScript newTopMod = bottomModifier;
        ModifierScript newBottomMod = topModifier;
        topModifier = newTopMod;
        bottomModifier = newBottomMod;
        animateAndUpdateModSlotSprites(switchTimeSeconds); 
        Debug.Log("modifier switch finished");
    }

    private void animateAndUpdateModSlotSprites(float animateSlotsForSeconds)
    {
        StopAllCoroutines();
        StartCoroutine(animateAndChangeModSlots(switchTimeSeconds));
    }

    private void updateModifierSlotSprites()
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
        topModifierSlot.SetModifierAndAbilitySprite(topModifier);
        bottomModifierSlot.SetModifierAndAbilitySprite(bottomModifier);
    }

    private IEnumerator animateAndChangeModSlots(float showForSeconds)
    {
        topModifierSlot.gameObject.SetActive(true);
        bottomModifierSlot.gameObject.SetActive(true);
        animator.speed = 1 / showForSeconds;
        animator.SetTrigger("modSlotChange");
        yield return new WaitForSeconds(showForSeconds / 2);
        updateModifierSlotSprites();
        yield return new WaitForSeconds(showForSeconds/2);
        switching = false;
        //topModifierSlot.gameObject.SetActive(false);
        //bottomModifierSlot.gameObject.SetActive(false);
    }

}
