using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class DamageGround : MonoBehaviour
{
    [SerializeField] public Collider2D activationCollider;
    [SerializeField] public string playerTag;
    [SerializeField] private float knockbackAmountX;
    [SerializeField] private float knockbackAmountY;
    [SerializeField] private float knockbackTime;
    private GameManager gameManager;
    [HideInInspector] public Vector2 enterDir;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == playerTag)
        {
            gameManager.playerMovement.rb.velocity = (enterDir * knockbackAmountX) + (new Vector2(0, 1) * knockbackAmountY);
            StartCoroutine(EnablePlayerMovementAfterSeconds(knockbackTime));
        }
    }

    public Vector2 enterDirection(Collider2D centerCollider, Collider2D enteringCollider)
    {
        Vector2 returnVector = (enteringCollider.transform.position - centerCollider.bounds.center).normalized;
        return returnVector;
    }

    private IEnumerator EnablePlayerMovementAfterSeconds(float seconds)
    {
        gameManager.TogglePlayerMovement(false);
        yield return new WaitForSeconds(seconds);
        gameManager.TogglePlayerMovement(true);
    }
}
