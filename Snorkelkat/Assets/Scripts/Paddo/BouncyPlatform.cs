using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPlatform : MonoBehaviour
{
    private GameManager gameManager;
    [HideInInspector]
    public bool playerIncoming;
    [HideInInspector]
    public float bounceAmount;


    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && playerIncoming)
        {
            playerIncoming = false;
            if (gameManager.playerMovement.rb.velocity.y < bounceAmount)
            {
                gameManager.playerMovement.rb.velocity = new Vector2(gameManager.playerMovement.rb.velocity.x, bounceAmount);
            }
        }
    }
}
