using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShadow : MonoBehaviour
{
    public DiscRotation enemy;
    public GameObject selfObject;
    private float posX;
    private float posY;
    private float posZ;
    private float selfPosY;

    void Update()
    {
        posX = enemy.transform.position.x;
        posY = enemy.transform.position.y;
        posZ = enemy.transform.position.z;

        // Sets self Y position to the floor
        if (posY > 1)
        {
            selfObject.transform.position = new Vector3(posX, 0, posZ);
            selfObject.transform.localScale = new Vector3(2.85f, 2.85f, 2.85f);
        }
        else
        {
            selfObject.transform.position = new Vector3(posX, 0, posZ);
            selfObject.transform.localScale = new Vector3(2, 2, 2);
        }
    }
}
