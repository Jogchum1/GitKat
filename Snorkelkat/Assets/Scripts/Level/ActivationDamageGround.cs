using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationDamageGround : MonoBehaviour
{
    private DamageGround damageGround;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        damageGround = GetComponentInParent<DamageGround>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == damageGround.playerTag)
        {
            damageGround.playerIsHere = true;
            damageGround.enterDir = damageGround.enterDirection(damageGround.activationCollider, collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == damageGround.playerTag)
        {
            damageGround.playerIsHere = false;
            if (!damageGround.knockingBack)
            {
                gameManager.TogglePlayerMovement(true);
            }
        }
    }
}
