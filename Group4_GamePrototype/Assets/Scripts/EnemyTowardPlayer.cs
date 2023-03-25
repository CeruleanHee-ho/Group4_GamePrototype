using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowardPlayer : MonoBehaviour
{
    #region Variables
    public float despawnPoint;
    public GameObject selfObject;
    public float randNum;
    public float randNumY;

    // Rotation Stuffs.
    private Vector3 Target; // Position that the object rotates around.
    private float rotationSpeed = -2; // Speed.
    private float circleRadius; // Wideness of rotation (determines where the object ends up in relation to the player).
    private float elevationOffset = 0.6f; // Offset for Y position.
    private Vector3 positionOffset;
    private float angle;
    #endregion

    #region Setup
    void Awake()
    {
        randNum = transform.position.x;
        randNumY = transform.position.y;
    }

    void Start()
    {
        Target = new Vector3(-4.5f, -0.1f, -0.5f);
        circleRadius = RandNumOut(randNum);

        MethodY(randNumY);
    }
    #endregion

    #region Spawning/Despawning
    void Update()
    {
        circleRadius = RandNumOut(randNum);
        // Despawns after certain distance.
        if (randNum == 0)
        {
            if (transform.position.x < (despawnPoint) && transform.position.z < (despawnPoint + 2))
            {
                Destroy(selfObject);
            }
        }
        else
        {
            if (transform.position.z < despawnPoint)
            {
                Destroy(selfObject);
            }
        }
    }
    #endregion

    #region Rotation Movement
    // Code for rotation.
    private void LateUpdate() // LateUpdate happens after the normal Update method.
    {
        // Uses trigonometry for calculations.
        positionOffset.Set
        (
            Mathf.Cos(angle) * -circleRadius, // Negative radius makes them go in the opposite direction.
            elevationOffset,
            Mathf.Sin(angle) * -circleRadius
        );
        transform.position = Target + positionOffset;
        angle += rotationSpeed * Time.deltaTime;
    }

    // Makes the enemy move continuously (old code, but is here for reference).
    //transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    #endregion

    #region Lane Positioning
    // Gives each number input a position output for the enemy cubes.
    private float RandNumOut(float rn)
    {
        switch (rn)
        {
            case 0:
                return 3.4f; // You don't need breaks when using returns.
            case 1:
                return 4.9f;
            default: // default also serves as case 2.
                return 6.7f;
        }
    }
    #endregion

    #region Scaling and Floating on Spawn
    // Decides if the cube should be tall, floating, or in its default state.
    // Translates Y value from "SpawnManager" script into 
    private void MethodY(float rn)
    {
        // Numerical values range from 0-9 (ten numbers).
        switch (rn)
        {
            // The first two cases make the enemy cube tall (1/4 chance of spawning like this).
            case 0: // Leaving a case without a break or return makes the switch go onto the next case;
            case 1:
                elevationOffset = 0.6f; // Is on floor.
                transform.localScale = new Vector3(1, 4, 1); // Is taller.
                break;
            // The second two cases make the enemy cube spawn above the player (1/4 chance of spawning like this).
            case 2:
            case 3:
                elevationOffset = 2.4f; // Is floating.
                transform.localScale = new Vector3(1, 1, 1); // Is normal size.
                break;
            // Every other case results in the enemy cube spawning on the floor at its normal size (3/5 chance of spawning like this).
            default:
                elevationOffset = 0.6f; // Is on floor.
                transform.localScale = new Vector3(1, 1, 1); // Is normal size.
                break;
        }
    }
    #endregion
}