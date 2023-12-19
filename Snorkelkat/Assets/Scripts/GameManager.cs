using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    /*[HideInInspector]*/ public PlayerMovement playerMovement;
    /*[HideInInspector]*/ public PlayerInteract playerInteract;
    /*[HideInInspector]*/ public PlayerCombat playerCombat;
    /*[HideInInspector]*/ public PlayerPhysicsStateMachine playerPhysicsStateMachine;
    /*[HideInInspector]*/ public Rigidbody2D playerRigidbody2D;
    /*[HideInInspector]*/ public AbilitySwitcher abilitySwitcher;
    public CamManager camManager;

    public GameObject inventory;
    private bool inventoryActive;

    public bool playerActive = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerMovement = player.GetComponent<PlayerMovement>();
        playerInteract = player.GetComponent<PlayerInteract>();
        playerCombat = player.GetComponent<PlayerCombat>();
        playerPhysicsStateMachine = player.GetComponent<PlayerPhysicsStateMachine>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        abilitySwitcher = player.GetComponent<AbilitySwitcher>();
    }

    private void Update()
    {
        if (inventoryActive)
        {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleInventory();
            }
        }
    }

    public void TogglePlayerMovement()
    {
        playerActive = !playerActive;
        playerMovement.enabled = playerActive;
        playerInteract.enabled = playerActive;
    }

    public void TogglePlayerMovement(bool set)
    {
        playerActive = set;
        playerMovement.enabled = playerActive;
        playerInteract.enabled = playerActive;
    }

    public void StopPlayerVelocity()
    {
        playerRigidbody2D.velocity = new Vector2(0, 0);
    }

    public void ToggleInventory()
    {
        inventoryActive = !inventoryActive;
        inventory.SetActive(inventoryActive);
        TogglePlayerMovement(!inventoryActive);
    }
}
