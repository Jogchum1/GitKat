using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<InventorySlot>().hasItem = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        if (GetComponentInParent<InventorySlot>() != null)
        {
            GetComponentInParent<InventorySlot>().hasItem = false;
            if(GetComponentInParent<InventorySlot>().selectedAbilitySlot == true)
            {
                GetComponentInParent<InventorySlot>().RemoveAbility();
            }

        }
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        
    }
}
