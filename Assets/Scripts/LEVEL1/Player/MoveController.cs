using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    public float Speed = 10f;
    public float Gravity = 10f;
    public float JumpPower = 5f;
    public float MinY = -7.55f;
    public GameObject Exit;
    bool isGrounded = false;
    private string tagg;

    private bool changedSpeed = false;
    public Animator anim;

    Rigidbody2D rb;

    void Start()
    {
        Physics2D.gravity = new Vector2(0, -Gravity);
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        rb = GetComponent<Rigidbody2D>();

        tagg = Exit.tag;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == tagg)
        {
            //anim.SetTrigger("isDead");
            Animator a = Instantiate(anim);
            a.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }

    }


    IEnumerator chSpeed()
    {
        yield return new WaitForSeconds(3.0f);
        Speed += Speed / 100 * 40;
        changedSpeed = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(Speed * move, rb.velocity.y);

        move = Input.GetAxis("Vertical");

        isGrounded = rotationAndOther.GroundCheck(this.gameObject);
        if ((move != 0 && isGrounded == true) || (Input.GetKeyDown(KeyCode.Space) && isGrounded == true))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }
        if (gameObject.transform.position.y < MinY)
        {
            Destroy(gameObject);
        }
        uint i = System.Convert.ToUInt32(rotationAndOther.Default.count);
        if (i % 20 == 0 && i != 0 && changedSpeed == false)
        {
            StartCoroutine(chSpeed());
            changedSpeed = true;
        }

        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Exit.GetComponent<Collider2D>());
    }
}
