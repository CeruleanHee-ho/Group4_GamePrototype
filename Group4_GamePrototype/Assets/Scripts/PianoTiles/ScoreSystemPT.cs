using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystemPT : MonoBehaviour
{
    public int score;
    public int amountToAdd;

    void Update()
    {
        score = score + amountToAdd;
    }

    void FixedUpdate()
    {
        amountToAdd = 0;
    }
}
