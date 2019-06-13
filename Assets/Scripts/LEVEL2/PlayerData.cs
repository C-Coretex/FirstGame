using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    public uint score;
    //public List<Weapon.WeaponType> TypeOfWeapon;
    public Weapon.WeaponType TypeOfWeapon;
    public float timeBetweenShots;
    public int damage;

    public PlayerData(Weapon Weapon)
    {
        score = System.Convert.ToUInt32(Weapon.Count.text);
        TypeOfWeapon = Weapon.CurrentWeaponType;
        damage = Weapon.damage;
        timeBetweenShots = Weapon.timeBetweenShots;
    }
}
