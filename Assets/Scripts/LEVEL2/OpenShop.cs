using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenShop : MonoBehaviour
{
    public Canvas ShopWindow;
    public int Ttime;
    public Text score;
    public Text text;

    public void OpenWindow()
    {
        if (Ttime == 0)
        {
            Time.timeScale = 0;
            text.text = "У тебя " + score.text + " денег.\nПосмотри что можешь купить, ковбой!!";
            ShopWindow.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            ShopWindow.gameObject.SetActive(false);
        }
    }
}
