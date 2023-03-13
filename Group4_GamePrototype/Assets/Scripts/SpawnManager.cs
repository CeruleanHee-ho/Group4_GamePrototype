using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPosition;
    public float startDelay;
    public float repeatRate;
    private float startY = 0.5f;
    private int startZ = 11;
    public int randNum;
    public int lastNum;

    // Start is called before the first frame update
    void Start()
    {
        randNum = Random.Range(0, 3);
        spawnPosition = new Vector3(RandNumOut(randNum), startY, startZ);
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    void SpawnObstacle()
    {
        randNum = Random.Range(0, 3);
        while (lastNum == randNum)
        {
            randNum = Random.Range(0, 3);
        }
        Vector3 pos = new Vector3(RandNumOut(randNum), startY, startZ);
        Instantiate(obstaclePrefab, pos, obstaclePrefab.transform.rotation);
        lastNum = randNum;
    }

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
            default:
                return -12;
                break;
        }
    }
}