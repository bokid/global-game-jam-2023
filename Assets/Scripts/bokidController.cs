using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bokidController : MonoBehaviour {
    public float speed;
    private float moveInput;

    private bool jumpInput;
    
    private bool jumpStop;

    private bool isJumping;

    private float jumpTimeCounter;

    public float jumpTime;
    

    private Rigidbody2D rb_boi;

    private SpriteRenderer spriteRenderer;

    public float jumpForce;

    public Vector2 counterForce;

    public float slidingSpeed;

    public float fallingSpeed;

    /*[SerializeField]*/ private bool isGrounded;
    /*[SerializeField]*/ private bool touchingLeftWall;
    /*[SerializeField]*/ private bool touchingRightWall;

    public LayerMask whatIsGround;

    public AudioSource walkingSound;
    public AudioSource jumpingSound;

    BoxCollider2D bodyCollider;
    private ContactFilter2D contactFilter;
    private ContactPoint2D[] contactPoints = new ContactPoint2D[16];
    private Vector3 previousPosition;
    private HashSet<Collider2D> touchedFloors;
    private HashSet<Collider2D> touchedLeftWalls;
    private HashSet<Collider2D> touchedRightWalls;

    // Start is called before the first frame update
    void Start() {
        touchedFloors = new HashSet<Collider2D>();
        touchedLeftWalls = new HashSet<Collider2D>();
        touchedRightWalls = new HashSet<Collider2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        rb_boi = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        contactFilter.SetLayerMask(whatIsGround);
    }

    // Update is called once per frame
    void Update() {
        moveInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKey(KeyCode.Space);
        

        Vector2 velocity = rb_boi.velocity;
        previousPosition = transform.position;

        velocity.x = moveInput * speed;

        if (touchingRightWall) {
            if (velocity.x > 0) {
                velocity.x = 0;
            }

            if (velocity.y < -slidingSpeed) {
                velocity.y = -slidingSpeed;
            }
        }
        else if (touchingLeftWall) {
            if (velocity.x < 0) {
                velocity.x = 0;
            }

            if (velocity.y < -slidingSpeed) {
                velocity.y = -slidingSpeed;
            }
        }

        if (moveInput < 0) {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0) {
            spriteRenderer.flipX = false;
        }

        //transform.localScale = new Vector3((spriteRenderer.flipX ? -1 : 1), 1, 1);

        if (isGrounded) {
            Debug.Log("I'm Grounded!");
            isJumping = false;

            
            if (jumpInput) 
            {
                jumpingSound.Play();
                
                isGrounded = false;
                isJumping = true;
                jumpTimeCounter = jumpTime;        
            }
            
            // play some sounds
            if (velocity.x != 0) {
                if (!walkingSound.isPlaying) {
                    walkingSound.Play();
                }
            }
            else {
                walkingSound.Stop();
            }
        }
        else {
            walkingSound.Stop();
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                velocity.y = jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            
            }
            else
            {
                isJumping = false;
                //velocity.y = -jumpForce;
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        
        rb_boi.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        int contactCount = collision.GetContacts(contactPoints);
        bool foundFloor = false;
        bool foundWall = false;
        float angleFromUp;

        if (((1 << collision.collider.gameObject.layer) & whatIsGround) > 0) {
            for (int i = 0; i < contactCount; i++) {
                angleFromUp = Vector2.Angle(Vector2.up, contactPoints[i].normal);

                if (!foundFloor && (angleFromUp <= 45)) {
                    foundFloor = true;
                    isGrounded = true;
                    touchedFloors.Add(collision.collider);
                }
                
                if (!foundWall) {
                    if (Vector2.Angle(Vector2.left, contactPoints[i].normal) <= 45) {
                        foundWall = true;
                        touchingRightWall = true;
                        touchedRightWalls.Add(collision.collider);
                    }
                    else if (Vector2.Angle(Vector2.right, contactPoints[i].normal) <= 45) {
                        foundWall = true;
                        touchingLeftWall = true;
                        touchedLeftWalls.Add(collision.collider);
                    }
                }

                if (foundWall && foundFloor) {
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        //this is only called if we stop touching altogether, so just remove from every set! =P
        touchedFloors.Remove(collision.collider);
        touchedRightWalls.Remove(collision.collider);
        touchedLeftWalls.Remove(collision.collider);

        isGrounded = touchedFloors.Count > 0;
        touchingRightWall = touchedRightWalls.Count > 0;
        touchingLeftWall = touchedLeftWalls.Count > 0;
    }
}
