using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGameOver : MonoBehaviour
{
    public GameObject selfObject;
    public PlayerController playerCon;

    void Update()
    {
        if (playerCon.gameOver == true)
        {
            Destroy(selfObject);
        }
    }
}
