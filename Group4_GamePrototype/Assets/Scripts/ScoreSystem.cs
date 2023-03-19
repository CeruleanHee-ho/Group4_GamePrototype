using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    #region Variables
    public float score;
    public float scoreDec;
    private float valueToAdd;
    private float bonusVal;
    private float lastLane;
    private float lastLaneTemp;
    private float addedVal;

    public bool jumpBonus;
    public bool laneBonus;

    public PlayerController playerCon;

    private bool bonusDelay; // Thias ensures that the player can't get the lane bonus the first time they jump over an enemy.
    private bool airDelay; // Checks if the player is in the air (for bonusDelay setup).
    #endregion

    void Start()
    {
        bonusDelay = true;
        airDelay = false;
    }

    void Update()
    {
        #region Score
        score = Mathf.Floor(scoreDec);
        scoreDec += addedVal;

        if (!playerCon.gameOver)
        {
            addedVal = (valueToAdd / 20) + (bonusVal / 20);
        }
        else
        {
            addedVal = 0;
        }

        if (playerCon.aboveEnemy)
        {
            valueToAdd = 20;
            jumpBonus = true;
        }
        else
        {
            valueToAdd = 1;
            jumpBonus = false;
        }

        #region Lane Check Bonus
        // Checks whether or not the player is on the same lane they were on the last time they jumping over an enemy cube. If so, give the player a bonus.
        if (playerCon.aboveEnemy && !playerCon.isOnGround)
        {
            lastLaneTemp = playerCon.transform.position.x;
        }

        if (playerCon.isOnGround && lastLaneTemp != lastLane)
        {
            lastLane = lastLaneTemp;
        }

        // 
        if (!bonusDelay && playerCon.aboveEnemy && lastLane != playerCon.transform.position.x && !playerCon.isOnGround)
        {
            bonusVal = 50;
            laneBonus = true;
        }
        else
        {
            bonusVal = 0;
            laneBonus = false;
        }

        // Bonus delay.
        if (bonusDelay && playerCon.aboveEnemy && !playerCon.isOnGround)
        {
            airDelay = true;
        }

        if (airDelay && playerCon.isOnGround)
        {
            bonusDelay = false;
        }
        #endregion
        #endregion
    }
}
