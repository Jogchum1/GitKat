using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightAbility : MonoBehaviour
{
    public Light2D playerLight;
    //public List<Light2D> fungiLights = new List<Light2D>();
    public GameObject envLights;
    public void PlayerLight()
    {
        playerLight.enabled = true;
        //foreach (Light2D light in fungiLights)
        //{
        //    light.enabled = false;
        //}
        envLights.SetActive(false);
    }

    public void FungiLight()
    {
        playerLight.enabled = false;
        //foreach (Light2D light in fungiLights)
        //{
        //    light.enabled = true;
        //}
        envLights.SetActive(true);
    }
}
