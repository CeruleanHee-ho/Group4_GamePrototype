using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Variables
    public GameObject obstaclePrefab; // Prefab to be spawned; can be overwritten under certain conditions.
    public GameObject cube;
    private Vector3 spawnPosition;
    public float startDelay;
    public float repeatRate;
    public int randNum; // Generates a random number to determine which lane the enemy cube spawns in.
    public int lastNum; // Keeps track of the last number (randNum) that was generated.
    public int randNumY; // Generates a random number to Determine whether or not the enemy cube will be floating.
    public int lastNumY; // Keeps track of the last number (randNumY) that was generated.
    #endregion

    #region Setup
    // Randomizes starting position of the first enemy cube.
    void Start()
    {
        randNum = Random.Range(0, 3);
        spawnPosition = new Vector3(randNum, randNumY, 0);
        obstaclePrefab = cube;
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }
    #endregion

    #region Spawning
    // This code calls a random number from 0-2, and each of those three numbers has their own output X position for the enemy cube to spawn at.

    // Spawns enemies and randomizes which lane they'll spawn in.
    void SpawnObstacle()
    {
        randNum = Random.Range(0, 3);
        // This piece of code ensures that an enemy cube will never spawn in the same lane twice in a row.
        while (lastNum == randNum)
        {
            randNum = Random.Range(0, 3);
        }

        #region Decides if enemy cube should be tall.
        // Ditto, but with the Y value.
        randNumY = Random.Range(0, 10);
        while (lastNumY == randNumY && randNumY < 4) // The values 4 and onward all result in default case, which means there is no difference.
        {
            // A case of 4 results in the default case, meaning this will not allow an enemy cube to spawn in taller or above the player twice in a row.
            randNumY = 4;
        }
        #endregion

        Vector3 pos = new Vector3(randNum, randNumY, 0);
        Instantiate(obstaclePrefab, pos, obstaclePrefab.transform.rotation);
        lastNum = randNum;
        lastNumY = randNumY;
    }
    #endregion
}