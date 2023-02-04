using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bokidController : MonoBehaviour
{
    public float speed;
    private float moveInput;

    private Rigidbody2D rb_boi;

    public float jumpForce;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    
    // Start is called before the first frame update
    void Start()
    {
        rb_boi = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        //print(moveInput);

        rb_boi.velocity = new Vector2(moveInput * speed, rb_boi.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (isGrounded)
        {
            print("we're grounded!");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("I'm jumpin!");

                rb_boi.velocity = Vector2.up * jumpForce;
            }
        }
        
            


        
    }
}
