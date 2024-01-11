using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class syncAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip footstepR;
    public AudioClip footstepL;
    public AudioClip flutteringWings;

    void PlayFootstepR()
    {
        source.PlayOneShot(footstepR);
    }

    void PlayFootstepL()
    {
        source.PlayOneShot(footstepL);
    }

    void PlayFlutteringWings()
    {
        source.PlayOneShot(flutteringWings);
    }

}
