using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StickerPopUp : MonoBehaviour
{
    [SerializeField]
    private UnityEvent BeforePopUpEvent;
    [SerializeField]
    private UnityEvent AfterPopUpEvent;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.pauseMenu.enabled = false;
        gameManager.playerMovement.enabled = false;
        gameManager.abilitySwitcher.enabled = false;
        BeforePopUpEvent?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.pauseMenu.enabled = true;
            gameManager.playerMovement.enabled = true;
            gameManager.abilitySwitcher.enabled = true;
            AfterPopUpEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
