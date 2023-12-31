using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IDropHandler
{
    [HideInInspector] public bool hasItem = false;
    public bool selectedAbilitySlot = false;
    public DraggableItem item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
            hasItem = true;
            item = draggableItem;
            if (selectedAbilitySlot == true)
                AddAbility();
        }
    }

    public void AddAbility()
    {
        Debug.Log("ADDING ABILITY TO PLAYER");
        item.gameObject.GetComponent<Ability>().Activate();
    }

    public void RemoveAbility()
    {
        item.gameObject.GetComponent<Ability>().Deactivate();
    }

    
}
