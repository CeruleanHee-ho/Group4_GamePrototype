using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    #region Variables
    public PlayerController player;
    private Vector2 startPos;
    private int pixelDistToDetect;
    private bool fingerDown;
    public bool isSwiping;
    #endregion

    void Start()
    {
        pixelDistToDetect = 20;
    }
    
    void Update()
    {
        MobileTouch();
    }

    // If you want to test out the controls on on PC with a mouse.
    void PCTouch()
    {
        if (fingerDown == false && Input.GetMouseButtonDown(0))
        {
            // If so, we're going to set the startPos to the first touch's position...
            startPos = Input.mousePosition;
            // ... and set fingerDown to true to start checking the direction of the swipe.
            fingerDown = true;
        }

        // Is our finger touching the screen?
        if (fingerDown)
        {
            // Did we swipe up?
            if (Input.mousePosition.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe up");
                if (player.isOnGround)
                {
                    isSwiping = true;
                }
            }
        }

        // If the finger is released from the screen...
        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            // ...startPos will be reset.
            fingerDown = false;
        }
    }

    // If you want to use mobile controls.
    void MobileTouch()
    {
        if (fingerDown == false && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            // If so, we're going to set the startPos to the first touch's position...
            startPos = Input.mousePosition;
            // ... and set fingerDown to true to start checking the direction of the swipe.
            fingerDown = true;
        }

        // Is our finger touching the screen?
        if (fingerDown)
        {
            // Did we swipe up?
            if (Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                //Debug.Log("Swipe up");
                if (player.isOnGround)
                {
                    isSwiping = true;
                }
            }
        }

        // If the finger is released from the screen...
        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            // ...startPos will be reset.
            fingerDown = false;
        }
    }
}
