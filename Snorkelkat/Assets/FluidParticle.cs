using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidParticle : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position * moveSpeed; 
    }
}
