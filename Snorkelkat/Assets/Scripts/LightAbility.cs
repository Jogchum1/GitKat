using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightAbility : MonoBehaviour
{
    public Light2D playerLight;
    public GameObject placeableLight;
    public GameObject envLights;
    //public List<Light2D> fungiLights = new List<Light2D>();

    private bool canPlaceLights = false;
    private List<GameObject> Lights = new List<GameObject>();

    public void PlayerLight()
    {
        canPlaceLights = true;
        //playerLight.enabled = true;
        //foreach (Light2D light in fungiLights)
        //{
        //    light.enabled = false;
        //}
        //envLights.SetActive(false);
    }

    public void FungiLight()
    {
        canPlaceLights = false;
        //playerLight.enabled = false;
        //foreach (Light2D light in fungiLights)
        //{
        //    light.enabled = true;
        //}
        //envLights.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && canPlaceLights == true)
        {
            GameObject tmpLight = Instantiate(placeableLight);

        }
    }
}
