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
    public AudioClip becomeSaus;
    public ParticleSystem particleSystem;
    public ParticleSystem particleSystemSaus;

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

    public void PlaybecomeSaus()
    {
        source.PlayOneShot(becomeSaus);
    }

    void PlayParticleSystem()
    {
        particleSystem.Play();
    }

    void PlayParticleSystemSaus()
    {
        particleSystemSaus.Play();
    }
}
