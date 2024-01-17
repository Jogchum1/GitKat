using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 1f;
    public LayerMask interactLayers;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] interactablesInRange = Physics2D.OverlapCircleAll(transform.position, interactRange, interactLayers);
        foreach (Collider2D npc in interactablesInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                npc.GetComponent<IInteractable>().Interact();
            }
        }
    }
}
