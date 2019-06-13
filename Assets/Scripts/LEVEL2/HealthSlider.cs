using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
   public GameObject player;
   Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        try
        {
            slider.maxValue = player.GetComponent<Enemy>().health;
        }
        catch
        {
            slider.maxValue = player.GetComponent<Enemy>().health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            Vector2 position = Camera.main.WorldToScreenPoint(player.transform.position);
            position.y += 40;
            slider.value = player.GetComponent<Enemy>().health;

            slider.transform.position = position;
        }
        catch
        {
            slider.value = 0;
            Destroy(gameObject);
        }
    }
}
