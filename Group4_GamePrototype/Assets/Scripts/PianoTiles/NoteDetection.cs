using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDetection : MonoBehaviour
{
    
    
    // Update is called once per frame
    void Update()
    {
        
    }

    #region Collision Detection
    void FixedUpdate()
    {
        Vector2 position = new Vector2(-8, 0);
        Vector2 direction = Vector2.right;
        float distance = 20;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, LayerMask.GetMask("Tile"));

        if (hit.collider != null)
        {
            Debug.Log("Tile is hit!");
        }

        //Method to draw the ray in scene for debug purposes.
        Debug.DrawRay(transform.position, direction * distance, Color.green);
    }
    #endregion
}
