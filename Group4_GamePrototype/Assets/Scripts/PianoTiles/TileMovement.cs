using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    #region Variables
    public float speed;
    public float despawnPoint;
    public GameObject selfObject;
    #endregion
    
    #region Spawning/Despawning
    void Update()
    {
        // Makes the tiles move continuously in the direction of the keyboard.
        transform.Translate(-Vector2.up * speed * Time.deltaTime);

        // Despawns after certain distance.
        if (transform.position.y < despawnPoint)
        {
            Destroy(selfObject);
        }
    }
    #endregion
}
