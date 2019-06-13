using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawn : MonoBehaviour
{
    public GameObject Bomb;
    public float SpawnSpeed = 5f;
    public float BombSpeed = 1f;
    public float X1 = -5f;
    public float X2 = 5f;
    public float Y1 = -5f;
    public float Y2 = 5f;
    private bool changedSpeed = true;

    private uint countOfSpawners = 2;
    private GameObject wow;

    void Start()
    {
        InvokeRepeating("SpawnBomb", 1f, SpawnSpeed);
        wow = this.gameObject;
    }

    void Update()
    {
        uint i = Convert.ToUInt32(rotationAndOther.Default.count);
        if (i % 5 == 0 && i!=0 && changedSpeed == false)
        {
            if (i % 30 == 0)
            {
                SpawnSpeed = countOfSpawners / 1f;
                BombSpeed = 1f / countOfSpawners;
                GameObject wow2 = Instantiate(wow);
                wow2.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                countOfSpawners += countOfSpawners;
            }
            else
            {
                SpawnSpeed -= SpawnSpeed / 100 * (40 / countOfSpawners);
                BombSpeed += BombSpeed / 100 * (40 / countOfSpawners);
            }

            CancelInvoke("SpawnBomb");
            Start();
            changedSpeed = true;
        }
    }

    void SpawnBomb()
    {
        GameObject bomb = Instantiate(Bomb);
        bomb.transform.position = new Vector2(gameObject.transform.position.x + UnityEngine.Random.Range(X1, X2), gameObject.transform.position.y + UnityEngine.Random.Range(Y1, Y2));
        bomb.transform.rotation = transform.rotation;

        bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, -BombSpeed));
        //rb.gravityScale = BombSpeed;
        //rb.AddForce(new Vector2(1,-BombSpeed));
        changedSpeed = false;
    }
}