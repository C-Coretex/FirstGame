using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        Pistol = 0,
        Shotgun = 1,
        Rifle = 2,
        Raycast = 3
    }

    public Transform firePoint;
    public int damage = 25;
    public LineRenderer LineRenderer;
    public GameObject Hand;

    public WeaponType CurrentWeaponType;

    public GameObject PistolBulletPrefab;
    public GameObject RifleBulletPrefab;
    public GameObject ShotgunBulletPrefab;

    //public GameObject impactEffect;

    public float timeBetweenShots = 0.3333f;  // Allow 3 shots per second
    private float timestamp;

    public Button ShopButton;

    private bool isEnded = true;

    public Text Count;

    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            CurrentWeaponType = data.TypeOfWeapon;
            damage = data.damage;
            timeBetweenShots = data.timeBetweenShots;
            Count.text = data.score.ToString();
        }
        else
        {
            CurrentWeaponType = WeaponType.Pistol;
            damage = 40;
            timeBetweenShots = 0.5f;
            Count.text = "0";
        }
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Time.time >= timestamp && Input.GetButton("Fire1") && CurrentWeaponType == WeaponType.Pistol && Hand != null)
            {
                Shoot();
                timestamp = Time.time + timeBetweenShots;
            }
            else if (Time.time >= timestamp && Input.GetButton("Fire1") && Hand != null && CurrentWeaponType == WeaponType.Raycast)  // GetButtonDown - единичное нажатие  
            {
                StartCoroutine(ShootRay());
                timestamp = Time.time + timeBetweenShots;
            }
            else if (Time.time >= timestamp && Input.GetButton("Fire1") && Hand != null && CurrentWeaponType == WeaponType.Rifle && isEnded == true)
            {
                isEnded = false;
                StartCoroutine(ShootRifle(RifleBulletPrefab));
                timestamp = Time.time + timeBetweenShots;
            }
            else if (Time.time >= timestamp && Input.GetButtonDown("Fire1") && Hand != null && CurrentWeaponType == WeaponType.Shotgun && isEnded == true)
            {
                isEnded = false;
                StartCoroutine(ShootShotgun(ShotgunBulletPrefab));
                timestamp = Time.time + timeBetweenShots;
            }
            if (Hand != null)
                FaceMouse();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShopButton.GetComponent<OpenShop>().OpenWindow();
        }
    }

    void FaceMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Hand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Shoot()
    {
        //Shooting with prefab
        GameObject bullet = Instantiate(PistolBulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Damage = damage;
        //shooting logic
    }
    IEnumerator ShootRay()
    {
        //Shooting with ray
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.name);

            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
            }


            LineRenderer.SetPosition(0, firePoint.position);
            LineRenderer.SetPosition(1, hitInfo.point);
            //Если надо - анимация будет проигрываться при попадании
            //Instantiate(impactEffect, hitInfo.point, Quaternion.identity);
        }
        else
        {
            LineRenderer.SetPosition(0, firePoint.position);
            LineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }
        LineRenderer.enabled = true;

        yield return new WaitForSeconds(0.09f);

        LineRenderer.enabled = false;
    }

    IEnumerator ShootShotgun(GameObject bulletPrefab)
    {
        Hand.transform.Rotate(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z - 3.5f);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Damage = damage;
        Destroy(bullet, 1.5f);

        Hand.transform.Rotate(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z + 3.5f);
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Damage = damage;
        Destroy(bullet, 1.5f);

        Hand.transform.Rotate(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z + 3.5f);
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Damage = damage;
        Destroy(bullet, 1.5f);

        Hand.transform.Rotate(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z - 3.5f);

        yield return new WaitForSeconds(0);
        isEnded = true;
    }

    IEnumerator ShootRifle(GameObject bulletPrefab)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Damage = damage;

        yield return new WaitForSeconds(0.01f);
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Damage = damage;

        yield return new WaitForSeconds(0.05f);
        bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Damage = damage;

        isEnded = true;
    }
}
