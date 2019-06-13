using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotationAndOther : MonoBehaviour
{
    //1 экземпляр
    public static rotationAndOther Default => _default;
    private static rotationAndOther _default;

    public rotationAndOther()
    {
        _default = this;
    }

    public GameObject player;
    private Vector3 offset;
    public int count = 0;
    public Text score;
    public Text score2;
    public static bool hit = false;
    public GameObject Canvas1;
    public GameObject Canvas2;

    #region Camera 
    void Start()
    {
        offset = transform.position - player.transform.position;
        hit = false;
    }

    bool gameOverOrNot = true;

    // LateUpdate is called after Update each frame
    [System.Obsolete]
    void LateUpdate()
    {
        if (player.gameObject)
        {
            transform.position = player.transform.position + offset;
            if (hit)
            {
                Inc();
            }
        }
        else
        {
            if (gameOverOrNot)
            {
                score2.text += score.text + " очков, найс";
                score.text = "";
                gameOverOrNot = false;
                var resp = GameObject.FindGameObjectsWithTag("Respawn");
                foreach (var a in resp)
                {
                    Destroy(a);
                }
                Canvas1.SetActive(Canvas2.active);
                Canvas2.SetActive(!Canvas2.active);
            }
        }
    }

    #endregion

    #region Functions 
    public static bool GroundCheck(GameObject gm)
    {
        float distance = 1f;
        Vector2 dir = new Vector2(0, -1);
        bool isGrounded = false;

        try
        {
            //if (Physics2D.Raycast(transform.position, dir, distance).collider.gameObject.name == "Ground")
            if (Physics2D.Raycast(gm.transform.position, dir, distance))
            {
                 isGrounded = true;
            }
            else
            {
                 isGrounded = false;
            }
            return isGrounded;
        }
        catch { return false; }
    }

    public static void Shake(float offsetX, float offsetY, GameObject camera)
    {
        Quaternion rotate = Quaternion.Euler(Random.Range(-offsetX, offsetX), Random.Range(-offsetY, offsetY), 0f);
        camera.transform.localRotation = Quaternion.Slerp(camera.transform.localRotation, camera.transform.localRotation * rotate, 0.5f);
    }
    public void Inc()
    {
        count++;
        score.text = count.ToString();
        hit = false;
    }
    #endregion

}
