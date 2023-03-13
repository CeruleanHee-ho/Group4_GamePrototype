using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowardPlayer : MonoBehaviour
{
    public float speed;
    public float despawnPoint;
    public GameObject selfObject;

    // Update is called once per frame
    void Update()
    {
        // Despawns after certain distance.
        if (transform.position.z < despawnPoint)
        {
            Destroy(selfObject);
        }

        // Makes the enemy move continuously.
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }
}