using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Yarn;
using Yarn.Unity;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    public DialogueRunner dialogueRunner;
    public string textTitle;
    public GameObject textComponent;
    public bool isActive = false;
    [SerializeField]
    private UnityEvent NPCEvent;

    public UnityEvent onTalkEvent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            isActive = !isActive;
            textComponent.SetActive(isActive);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isActive = !isActive;
            textComponent.SetActive(isActive);
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        dialogueRunner.StartDialogue(textTitle);
        
        onTalkEvent.Invoke();
    }

    [YarnCommand("RunNPCEvent")]
    public void RunNPCEvent()
    {
        NPCEvent.Invoke();
    }
}
