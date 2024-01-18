using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationDamageGround : MonoBehaviour
{
    private DamageGround damageGround;

    private void Start()
    {
        damageGround = GetComponentInParent<DamageGround>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == damageGround.playerTag)
        {
            damageGround.enterDir = damageGround.enterDirection(damageGround.activationCollider, collision);
        }
    }
}
