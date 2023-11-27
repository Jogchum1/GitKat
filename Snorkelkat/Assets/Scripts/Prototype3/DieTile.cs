using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTile : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerCombat playerCombat;

    private void Start()
    {
        gameManager = GameManager.instance;
        playerCombat = gameManager.player.GetComponent<PlayerCombat>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCombat.Die();
        }
    }
}
