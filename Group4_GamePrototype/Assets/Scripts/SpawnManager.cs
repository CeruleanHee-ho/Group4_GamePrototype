using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Variables
    public GameObject obstaclePrefab;
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

        #region Commented out code for spawning elevated enemy cubes (for use later).
        // Ditto, but with the Y value.
        randNumY = Random.Range(0, 3);
        while (lastNumY == randNumY)
        {
            randNumY = Random.Range(0, 3);
        }
        #endregion

        Vector3 pos = new Vector3(randNum, randNumY, 0);
        Instantiate(obstaclePrefab, pos, obstaclePrefab.transform.rotation);
        lastNum = randNum;
        lastNumY = randNumY;
    }
    #endregion
}