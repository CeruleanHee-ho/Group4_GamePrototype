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
        if (transform.position.z < despawnPoint)
        {
            Destroy(selfObject);
        }

        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }
}