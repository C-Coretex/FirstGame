using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hit : MonoBehaviour
{
    bool isTrigger = false;
    float offsetX = 0.3f;
    float offsetY = 0.07f;
    public AudioSource exp1;
    public AudioSource exp2;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Finish")
        {
            isTrigger = true;
            Invoke("Destroy", 0.1f);
            GetComponent<SpriteRenderer>().enabled = false;

            var a = Random.Range(0, 2);
            if(a == 0)
            {
                Instantiate(exp1);
            }
            else
            {
                Instantiate(exp2);
            }
            //Можно добавить анимацию взрыва
        }
    }
    void Update()
    {
        if (isTrigger == true)
        {
            rotationAndOther.Shake(offsetX, offsetY, Camera.main.gameObject);
        }
    }
    void Destroy()
    {
        if (isTrigger == true)
        {
            Camera.main.gameObject.transform.localRotation = new Quaternion(0,0,0,0);
            Destroy(gameObject);
            rotationAndOther.hit = true;
        }
    }
}
