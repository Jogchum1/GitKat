using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 1f;
    public LayerMask interactLayers;

    public GameObject inventoryUI;
    private bool invActive = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] interactablesInRange = Physics2D.OverlapCircleAll(transform.position, interactRange, interactLayers);
        foreach (Collider2D npc in interactablesInRange)
        {
            Debug.Log(npc.name);
            if(npc.GetComponent<NPCDialogue>() != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    npc.GetComponent<IInteractable>().Interact();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleUI();
        }

    }

    private void ToggleUI()
    {
        invActive = !invActive;
        inventoryUI.SetActive(invActive);
    }
}
