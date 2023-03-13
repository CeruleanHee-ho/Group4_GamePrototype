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
    private float startY = 0.5f;
    private int startZ = 11;
    public int randNum;
    public int lastNum; // Keeps track of the last number that was generated.
    #endregion

    // This code calls a random number from 0-2, and each of those three numbers has their own output X position for the enemy cube to spawn at.

    // Randomizes starting position of the first enemy cube.
    void Start()
    {
        randNum = Random.Range(0, 3);
        spawnPosition = new Vector3(RandNumOut(randNum), startY, startZ);
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Spawns enemies and randomizes which lane they'll spawn in.
    void SpawnObstacle()
    {
        randNum = Random.Range(0, 3);
        // This piece of code ensures that an enemy cube will never spawn in the same lane twice in a row.
        while (lastNum == randNum)
        {
            randNum = Random.Range(0, 3);
        }
        Vector3 pos = new Vector3(RandNumOut(randNum), startY, startZ);
        Instantiate(obstaclePrefab, pos, obstaclePrefab.transform.rotation);
        lastNum = randNum;
    }

    // Gives each number input a position output for the enemy cubes.
    private int RandNumOut(int rn)
    {
        switch (rn)
        {
            case 0:
                return -2;
                break;
            case 1:
                return 0;
                break;
            case 2:
                return 2;
                break;
            // I believe this default case is what's causing the "Unreachable code" detection, although, having a default case is good practice and I think it required.
            default:
                return 0;
                break;
        }
    }
}