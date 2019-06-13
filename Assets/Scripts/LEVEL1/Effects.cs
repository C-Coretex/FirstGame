using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    AudioSource a;
    void Start()
    {
        a = gameObject.GetComponent<AudioSource>();
        a.volume = Random.Range(0.2f, 1.0f);
        a.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (!a.isPlaying)
        {
            Object.Destroy(gameObject);
        }
    }
}
