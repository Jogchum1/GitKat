using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddo : MonoBehaviour
{
    public int paddoTime = 10;

    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        //DisablePaddoCollider();
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
        col.isTrigger = true;
    }

    public void DestroyPaddoNow()
    {
        Destroy(gameObject);
    }

    public void DespawnPaddo ()
    {
        animator.SetTrigger("Despawn");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            Vector2 enterDirection = (collision.transform.position - col.bounds.center).normalized;
            if (enterDirection.y > 0f)
            {
                animator.SetTrigger("Bounce");
            }
        }
    }
}
