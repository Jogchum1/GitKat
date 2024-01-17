using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    public DialogueRunner dialogueRunner;
    public string textTitle;
    public GameObject textComponent;
    public bool isActive = false;
    public bool isInteractable = true;

    [Header("Events")]
    [SerializeField]
    private UnityEvent onTalkEvent;
    [SerializeField]
    private UnityEvent NPCEvent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && isInteractable)
        {
            TogglePopUp(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            TogglePopUp(false);
        }
    }

    public void TogglePopUp () 
    {
        isActive = !isActive;
        textComponent.SetActive(isActive);
    }   

    public void TogglePopUp (bool input) 
    {
        isActive = input;
        textComponent.SetActive(isActive);
    }

    public void ToggleNPC()
    {
        isInteractable = !isInteractable;
        TogglePopUp(isInteractable);
    }

    public void Interact()
    {
        //Debug.Log("Interacting");
        if (!isInteractable)
        {
            return;
        }
        dialogueRunner.StartDialogue(textTitle);
        onTalkEvent.Invoke();
    }

    [YarnCommand("RunNPCEvent")]
    public void RunNPCEvent()
    {
        NPCEvent.Invoke();
    }
}
