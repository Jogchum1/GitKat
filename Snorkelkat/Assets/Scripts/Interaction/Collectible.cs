using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactPopUp;
    [SerializeField] private GameObject collectedPopUp;
    [SerializeField] private int collectableIndex;
    [SerializeField] private List<GameObject> childrenToDisable = new List<GameObject>(); 
    private GameManager gameManager;
    private bool interactable = true;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Interact()
    {
        if (interactable)
        {
            gameManager.collectables.CollectSticker(collectableIndex);
            collectedPopUp.SetActive(true);
            interactable = false;
            ToggleInteractPopUp();
            foreach (GameObject gameObject in childrenToDisable)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && interactable)
        {
            ToggleInteractPopUp();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && interactable)
        {
            ToggleInteractPopUp();
            Debug.Log("Toggled");
        }
    }

    public void ToggleInteractPopUp()
    {
        bool newbool = !interactPopUp.activeInHierarchy;
        interactPopUp.SetActive(newbool);
    }
}
