using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddo : MonoBehaviour
{
    public int paddoTime = 10;

    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D col;
    private GameManager gameManager;
    [SerializeField] private BouncyPlatform bouncyPlatform;
    [SerializeField] private float bounceAmount;

    // Start is called before the first frame update
    void Start()
    {
        //DisablePaddoCollider();
        gameManager = GameManager.instance;
        StartCoroutine(DestoryPaddo());
    }

    private IEnumerator DestoryPaddo()
    {
        yield return new WaitForSeconds(paddoTime);
        DisablePaddoCollider();
        DespawnPaddo();
    }

    public void EnablePaddoCollider()
    {
        //col.isTrigger = false;
    }  

    public void DisablePaddoCollider()
    {
        col.enabled = false;
    }

    public void DestroyPaddoNow()
    {
        Destroy(gameObject);
    }

    //plays despawn animation and destroys gameobject after
    public void DespawnPaddo ()
    {
        gameManager.playerMovement.paddos.Remove(gameObject);
        animator.SetTrigger("Despawn");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            Vector2 enterDirection = (collision.transform.position - col.bounds.center).normalized;
            if (enterDirection.y > 0f)
            {
                animator.SetTrigger("Bounce");
                bouncyPlatform.bounceAmount = bounceAmount;
                bouncyPlatform.playerIncoming = true;
            }
        }
    }
}
