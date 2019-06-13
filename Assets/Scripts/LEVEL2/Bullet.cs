using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float Speed = 20f;
    public int Damage;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * Speed;
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        Debug.Log(hitInfo.name);
        if (enemy)
        {
            try
            {
                enemy.TakeDamage(Damage);
            }
            catch
            {
                Debug.Log("Problem at bullet.OnTriggerEnter2D");
            }
        }
        if (hitInfo.tag != "Finish")
        {
            Destroy(gameObject);
        }
    }
}
