using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public enum WeaponType
    {
        PrefabStandart = 0,
        Raycast = 1,
        PrefabShotgun = 2
    }


    private Weapon WeaponScript;

    private GameObject Player;
    public GameObject Hand;
    public Transform firePoint;
    public float Speed = 1;

    public GameObject bulletPrefab;
    public WeaponType CurrentWeaponType;
    public int damage = 25;
    public LineRenderer LineRenderer;
    public LineRenderer LineWarningRenderer;
    public bool canFace = true;


    public float timeBetweenShots = 0.3333f;  // Allow 3 shots per second
    private float timestamp;

    private bool isEnded = true;

    void Start()
    {
        WeaponScript = GetComponent<Weapon>();

        Player = GameObject.Find("Player");

        StartCoroutine(Wait());
    }

    void Update()
    {
        if (Time.timeScale != 0 && canFace)
        {
            FacePlayer();

            Move();
        }

            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        if (Time.time >= timestamp && CurrentWeaponType == WeaponType.Raycast)
        {
            Debug.Log("I'm calling RayCast");
            StartCoroutine(ShootRay()); // Сделать красную линию прицеливания
            timestamp = Time.time + timeBetweenShots;
        }
        else if (Time.time >= timestamp && hitInfo.transform.name == "Player")
            {
            if (CurrentWeaponType == WeaponType.PrefabStandart)
                Shoot();
            else if (CurrentWeaponType == WeaponType.PrefabShotgun && isEnded == true)
            {
                isEnded = false;
                StartCoroutine(ShootShotgun());
            }

            timestamp = Time.time + timeBetweenShots;
            }

        if (transform.position.y < -9)
            {
                Destroy(gameObject);
            }
    }

    void FacePlayer()
    {
        Vector3 dir = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Hand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Move()
    {

        Vector3 a = new Vector3(Random.Range(Player.transform.position.x - 5, Player.transform.position.x + 5), 0, 0);
        float time = Speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(gameObject.transform.position, a, time);
    }


    void Shoot()
    {
        //Shooting with prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().Damage = damage;
        //shooting logic
    }

    IEnumerator ShowWarning(RaycastHit2D hitInfo)
    {
        LineWarningRenderer.SetPosition(0, firePoint.position);
        LineWarningRenderer.SetPosition(1, firePoint.position + firePoint.right * 1000);
        LineWarningRenderer.enabled = true;
        for (int i = 0; i < 100; i++)
        {
            LineWarningRenderer.startWidth -= 0.01f;
            yield return new WaitForSeconds(0.001f);
        }
        LineWarningRenderer.enabled = false;
        LineWarningRenderer.startWidth = 1.1f;
    }
    IEnumerator ShootRay()
    {
        canFace = false;
        //Shooting with ray
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        yield return StartCoroutine(ShowWarning(hitInfo));

        hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        if (hitInfo)
        {
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

        yield return new WaitForSeconds(0.4f);

        LineRenderer.enabled = false;

        canFace = true;
    }


    IEnumerator ShootShotgun()
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

    IEnumerator Wait()
    {
        timestamp = 999999;
        yield return new WaitForSeconds(Random.Range(0.5f,2f));
        timestamp = 0;
    }
}
