using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static Move Default => _default;
    private static Move _default;
    public Move()
    {
        _default = this;
    }

    public float Speed;
    public float JumpForce;
    private float moveInput;
    public GameObject Hand;

    private Rigidbody2D rb;
    public bool facingRight = true;

    private bool isGrounded;
    public float checkRadius;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    private int ExtraJumps;
    public int ExtraJumpValue = 1;

    void Start()
    {
        ExtraJumps = ExtraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(isGrounded == true)
        {
            ExtraJumps = ExtraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.W) && ExtraJumps > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
            ExtraJumps--;
        } else if (Input.GetKeyDown(KeyCode.W) && ExtraJumps == 0 && ExtraJumps > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
        }

        if (transform.position.y < -9)
        {
            Debug.Log("F");
            var fooGroup = Resources.FindObjectsOfTypeAll<Canvas>();
            foreach (var f in fooGroup)
            {
                f.gameObject.SetActive(false);
                if (f.name == "EndScreen")
                    f.gameObject.SetActive(true);
            }

        Destroy(gameObject);
        }
    }


    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)//Двигается направо = 1
        {
            Flip();
        } else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180, 0f);
        //Vector3 Scaler = transform.localScale;
        //Scaler.x *= -1;
        //transform.localScale = Scaler;
    }
}
