using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    #region Variables
    public float score;
    public float gameTime;
    public float lastTime;
    // These two variables temporarily lock the player out of getting bonuses until they touch the ground. Without the locks, if a player got a jumping bonus, they'd essentially get five to ten points every frame they're above the enemy, however, having the locks active prevents that from happening and the player will only get the bonus once during that whole jump.
    private bool aboveBonusGet;
    private bool laneBonusGet;

    private float lastLane;
    private float lastLaneTemp;

    public bool jumpBonus;
    public bool laneBonus;

    public PlayerController playerCon;

    private bool bonusDelay; // This ensures that the player can't get the lane bonus the first time they jump over an enemy.
    private bool airDelay; // Checks if the player is in the air (for bonusDelay setup).
    #endregion

    void Start()
    {
        bonusDelay = true;
        airDelay = false;
    }

    void Update()
    {
        #region One Point Every Second Still Alive
        if (!playerCon.gameOver)
        {
            gameTime += Time.deltaTime; // Keeps track of how long the player has survived for. May make this a viewable UI element.
            if (Time.time - lastTime >= 1f) // The player earns an extra point for every second they survive.
            {
                score += 1;
                lastTime = Time.time;
            }
        }
        #endregion

        #region Prevent Bonus on Every Frame
        // If the player is on the ground, then the locks for bonuses are lifted.
        if (playerCon.transform.position.y == 0.5f)
        {
            aboveBonusGet = false;
            laneBonusGet = false;
        }
        #endregion

        #region Jump Bonus
        // The player gets five points as a bonus for jumping over an enemy.
        if (playerCon.aboveEnemy && !aboveBonusGet)
        {
            score += 5;
            aboveBonusGet = true;
        }
        #endregion

        #region Lane Check
        // Checks whether or not the player is on the same lane they were on the last time they jumping over an enemy cube. If so, give the player a bonus.
        if (playerCon.aboveEnemy && !playerCon.isOnGround)
        {
            lastLaneTemp = playerCon.transform.position.x;
        }

        if (playerCon.isOnGround && lastLaneTemp != lastLane)
        {
            lastLane = lastLaneTemp;
        }
        #endregion

        #region Different Lane Bonus
        // The player gets ten points as a lane-switch bonus. Stacks with jumping bonus for a total of fifteen points!
        if (!bonusDelay && playerCon.aboveEnemy && lastLane != playerCon.transform.position.x && !playerCon.isOnGround && !laneBonusGet)
        {
            score += 10;
            laneBonusGet = true;
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
    }
}
