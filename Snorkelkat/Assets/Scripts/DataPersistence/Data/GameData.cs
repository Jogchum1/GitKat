using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int jumpCount;

    public Vector3 playerPosition;
    public GameData()
    {
        this.jumpCount = 0;
        playerPosition = Vector3.zero;
    }
}
