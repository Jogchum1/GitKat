using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddoSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip bounce;
    public AudioClip spawn;

    void PlayBounce()
    {
        source.PlayOneShot(bounce);
    }

    void PlaySpawn()
    {
        source.PlayOneShot(spawn);
    }
}
