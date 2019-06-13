using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject Player;

    void Update()
    {
        if (Player != null)
        {
            float newXPosition = Player.transform.position.x;

            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
    }
}
