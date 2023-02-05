using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bokidController : MonoBehaviour
{
    public float speed;
    private float moveInput;

    private Rigidbody2D rb_boi;

    public float jumpForce;

    public float slidingSpeed;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private bool onWall;
    public Transform frontCheck;

    int facing = 1;

    public AudioSource walkSound;


    
    // Start is called before the first frame update
    void Start()
    {
        rb_boi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        onWall = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (onWall) 
        {
            if (facing == 1) 
            {
                moveInput = Mathf.Clamp(moveInput, -1, 0); // if movement is 1, make it 0 instead
            }
            else 
            {
                moveInput = Mathf.Clamp(moveInput, 0, 1); // if movement is -1, make it 0 instead
            }
        }

        rb_boi.velocity = new Vector2(moveInput * speed, rb_boi.velocity.y);
        
        transform.localScale = new Vector3(facing, 1, 1);

        if (moveInput < 0) 
        {
            facing = -1;
            if(!walkSound.isPlaying && isGrounded)
            {
                walkSound.Play();
            }
            else if (!isGrounded)
            {
                walkSound.Stop();
            }
        }
        else if (moveInput > 0) 
        {
            facing = 1;
            if (!walkSound.isPlaying && isGrounded)
            {
                walkSound.Play();
            }
            else if (!isGrounded)
            {
                walkSound.Stop();
            }
        }
        else
        {
            walkSound.Stop();
        }
        

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb_boi.velocity = Vector2.up * jumpForce;
            }
        }

    }
}
