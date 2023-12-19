using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTile : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.playerCombat.Die();
        }
    }
}
