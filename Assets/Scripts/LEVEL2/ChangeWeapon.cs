using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject GM;

    public int Damage;
    public Text score;
    public Weapon.WeaponType Type;
    public float TimeBerweenShots;
    public uint Cost;

    public void ChangeSomethind()
    {
        if (System.Convert.ToUInt32(score.text) >= Cost)  //Сделать стоимость
        {
            GM.GetComponent<Weapon>().CurrentWeaponType = Type;
            GM.GetComponent<Weapon>().damage = Damage;
            GM.GetComponent<Weapon>().timeBetweenShots = TimeBerweenShots;
            score.text = (System.Convert.ToUInt32(score.text) - Cost).ToString();
        }
        else
        {
            Debug.Log("Neeeee, tebje ne hvataet " + (Cost - System.Convert.ToUInt32(score.text)));
        }
    }
}
