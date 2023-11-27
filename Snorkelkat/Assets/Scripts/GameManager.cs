using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject inventory;
    private bool inventoryActive;

    public bool playerActive = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        player.GetComponent<PlayerMovement>().enabled = playerActive;
        player.GetComponent<PlayerInteract>().enabled = playerActive;
    }

    public void TogglePlayerMovement(bool set)
    {
        playerActive = set;
        player.GetComponent<PlayerMovement>().enabled = playerActive;
        player.GetComponent<PlayerInteract>().enabled = playerActive;
    }

    public void StopPlayerVelocity()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void ToggleInventory()
    {
        inventoryActive = !inventoryActive;
        inventory.SetActive(inventoryActive);
        TogglePlayerMovement(!inventoryActive);
    }
}
