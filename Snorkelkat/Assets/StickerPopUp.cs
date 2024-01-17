using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerPopUp : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
