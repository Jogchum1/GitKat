using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRoomEnter()
    {
        InitializeEnemies();
    }

    public void InitializeEnemies()
    {
        foreach (SpawnPoint enemySpawn in spawnPoints)
        {
            enemySpawn.SpawnEnemies();
        }
    }
}
