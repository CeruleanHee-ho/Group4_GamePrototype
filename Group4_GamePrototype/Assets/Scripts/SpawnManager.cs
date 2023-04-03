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
    public int lastNum2; // Keeps track of the number that was generated before the number value in lastNum.
    public int spamCount; // Keeps track of whether or not the spawn manager has been spawning the same pattern too often.
    public int randNumY; // Generates a random number to Determine whether or not the enemy cube will be floating.
    public int lastNumY; // Keeps track of the last number (randNumY) that was generated.
    #endregion

    #region Setup
    // Randomizes starting position of the first enemy cube.
    void Start()
    {
        randNum = Random.Range(0, 3);
        spawnPosition = new Vector3(randNum, randNumY, 0); //timer + alphaOutput);
        obstaclePrefab = cube;
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }
    #endregion

    #region Spawning
    // This code calls a random number from 0-2, and each of those three numbers have their own output X position for the enemy cube to spawn at.

    // Spawns enemies and randomizes which lane they'll spawn in.
    void SpawnObstacle()
    {
        randNum = Random.Range(0, 3);
        // In it's entirety, this code prevents spawns from becoming too repetitive. Enemies will NEVER spawn in the same lane twice in a row. Likewise, the spawn manager is forced to change patterns if it has done the same pattern three times in a row.
        if (spamCount >= 3)
        {
            while (lastNum == randNum || lastNum2 == randNum) // lastNum and lastNum2 will never be equal to each other.
            {
                randNum = Random.Range(0, 3);
            }
            spamCount = 1; // The spam count is set to one instead of zero because this spawn counts as the first pattern to be compared with. This is done because the only other time the spam count can increase is through code that is unable to be access by this current enemy spawn due to is being locked behind an else statement.
        }
        else
        {
            // This piece of code ensures that an enemy cube will never spawn in the same lane twice in a row.
            while (lastNum == randNum)
            {
                randNum = Random.Range(0, 3);
            }

            if (lastNum == randNum || lastNum2 == randNum) // For every repeated pattern, one point is added to the spam count. When the spam count reaches three, the spawn manager is forced to spawn a different random pattern.
            {
                spamCount += 1;
            }
            else
            {
                spamCount = 0; // Resets the spam count completely if the detected patterns is different from the last one.
            }
        }

        #region Decides if enemy cube should be tall.
        // Ditto, but with the Y value.
        randNumY = Random.Range(0, 14);
        while (lastNumY == randNumY && randNumY < 4) // The values 4 and onward all result in default case, which means there is no difference.
        {
            // A case of 4 results in the default case, meaning this will not allow an enemy cube to spawn in taller or above the player twice in a row.
            randNumY = 4;
        }
        #endregion

        Vector3 pos = new Vector3(randNum, randNumY, 0);
        Instantiate(obstaclePrefab, pos, obstaclePrefab.transform.rotation);
        lastNum2 = lastNum;
        lastNum = randNum;
        lastNumY = randNumY;
    }
    #endregion
}