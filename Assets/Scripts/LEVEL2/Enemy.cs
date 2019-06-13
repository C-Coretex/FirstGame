using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public int health = 100;
    public int score = 1;
    public GameObject slider;
    private GameObject Canvas;
    private Text text;

    public GameObject DeathEffect;
    public GameObject BloodSplash;

    private RipplePostProcessor camRipple;

    void Start()
    {
        Canvas = GameObject.Find("CanvasHealth");
        text = Canvas.GetComponentInChildren<Text>();

        GameObject sl = Instantiate(slider);
        sl.GetComponent<HealthSlider>().player = gameObject;
        sl.transform.SetParent(Canvas.transform, false);
        camRipple = Camera.main.GetComponent<RipplePostProcessor>();
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
            text.text = System.Convert.ToString(System.Convert.ToUInt32(text.text.ToString()) + score);
        }
    }

    void Die()
    {
            camRipple.MaxAmount = Random.Range(15, 35);
            camRipple.Friction = Random.Range(0.5f, 0.9f);

            camRipple.RippleEffect();
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            Quaternion a = Quaternion.identity;
            a.z = -179;
            Instantiate(BloodSplash, transform.position, a);

        if(gameObject.name == "Player")
        {
            Debug.Log("Schet = " + gameObject.GetComponent<Weapon>().Count.text);
            SaveSystem.SavePlayer(gameObject.GetComponent<Weapon>());

            //Time.timeScale = 0;
            Debug.Log("F");

            var fooGroup = Resources.FindObjectsOfTypeAll<Canvas>();
            foreach(var f in fooGroup)
            {
                f.gameObject.SetActive(false);
                if (f.name == "EndScreen")
                    f.gameObject.SetActive(true);
            }
        }

        Destroy(gameObject);
    }
}
