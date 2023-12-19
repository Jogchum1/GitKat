using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddo : MonoBehaviour
{
    public int paddoTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestoryPaddo());
    }

    private IEnumerator DestoryPaddo()
    {
        yield return new WaitForSeconds(paddoTime);
        Destroy(gameObject);
    }
}
