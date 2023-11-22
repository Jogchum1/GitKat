using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject CollectibleFoundText;

    private void Start()
    {
        CollectibleFoundText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            CollectibleFoundText.SetActive(true);
            Destroy(gameObject);
        }
    }
}
