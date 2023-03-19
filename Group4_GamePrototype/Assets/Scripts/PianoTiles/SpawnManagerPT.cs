using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerPT : MonoBehaviour
{
    #region Variables
    public GameObject obstaclePrefab;
    private Vector2 spawnPosition;
    public float startDelay;
    public float repeatRate;
    public int randNum; // Generates a random number to determine which lane the tile spawns in.
    public int lastNum; // Keeps track of the last number (randNum) that was generated.
    #endregion

    #region Setup
    // Randomizes starting position of the tiles.
    void Start()
    {
        randNum = Random.Range(0, 4);
        spawnPosition = new Vector2(RandNumOut(randNum), 11);
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }
    #endregion

    #region Spawning
    // This code calls a random number from 0-3, and each of those three numbers has their own output X position for the tile to spawn at.

    // Spawns tiles and randomizes which lane they'll spawn in.
    void SpawnObstacle()
    {
        randNum = Random.Range(0, 4);
        // This piece of code ensures that a tile will never spawn in the same lane twice in a row.
        while (lastNum == randNum)
        {
            randNum = Random.Range(0, 4);
        }

        Vector2 pos = new Vector2(RandNumOut(randNum), 11);
        Instantiate(obstaclePrefab, pos, obstaclePrefab.transform.rotation);
        lastNum = randNum;
    }

    // Gives each number input a position output for the enemy cubes.
    private float RandNumOut(float rn)
    {
        switch (rn)
        {
            case 0:
                return -4.75f; // You don't need breaks when using returns.
            case 1:
                return -1.75f;
            case 2:
                return 1.25f;
            default: // default also serves as case 3.
                return 4.25f;
        }
    }
    #endregion
}