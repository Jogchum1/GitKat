using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class syncAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip footstepR;
    public AudioClip footstepL;
    public AudioClip flutteringWings;
    public AudioClip sausfootstepR;
    public AudioClip sausfootstepL;

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

    void PlaySausfootstepR()
    {
        source.PlayOneShot(sausfootstepR);
    }

    void PlaySausfootstepL()
    {
        source.PlayOneShot(sausfootstepL);
    }

}
