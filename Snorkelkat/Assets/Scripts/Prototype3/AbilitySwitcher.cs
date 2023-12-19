using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySwitcher : MonoBehaviour
{
    [SerializeField] private ModifierScript topModifier;
    [SerializeField] private ModifierScript bottomModifier;
    [SerializeField] private ModifierSlot topModifierSlot;
    [SerializeField] private ModifierSlot bottomModifierSlot; 
    [SerializeField] private GameObject topModifierSprite;
    [SerializeField] private GameObject bottomModifierSprite;
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

        topModifierSlot.SetModifierAndAbilitySprite(topModifier);
        bottomModifierSlot.SetModifierAndAbilitySprite(bottomModifier);
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
        if (topModifierSprite.transform.GetSiblingIndex() != 0)
        {
            lastChild = topModifierSprite.transform;
        }
        else
        {
            lastChild = bottomModifierSprite.transform;
        }
        lastChild.SetAsFirstSibling();
    }

    private IEnumerator animateAndChangeModSlots(float showForSeconds)
    {
        topModifierSlot.gameObject.SetActive(true);
        bottomModifierSlot.gameObject.SetActive(true);
        animator.speed = 1 / showForSeconds;
        animator.SetTrigger("modSlotChange");
        topModifierSlot.SetAbilitySprite(topModifier);
        bottomModifierSlot.SetAbilitySprite(bottomModifier);

        yield return new WaitForSeconds(showForSeconds / 2);

        updateModifierSlotSprites();
        //switch sprites
        topModifierSlot.SetModifierSprite(topModifier);
        bottomModifierSlot.SetModifierSprite(bottomModifier);

        yield return new WaitForSeconds(showForSeconds/2);

        //topModifierSlot.SetAbilitySprite(topModifier);
        //bottomModifierSlot.SetAbilitySprite(bottomModifier);
        switching = false;
    }

}
