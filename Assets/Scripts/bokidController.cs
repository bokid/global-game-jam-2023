using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bokidController : MonoBehaviour {
    public float speed;
    private float moveInput;

    private bool jumpInput;

    private Rigidbody2D rb_boi;

    private SpriteRenderer spriteRenderer;

    public float jumpForce;

    public float slidingSpeed;

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
        jumpInput = Input.GetKeyDown(KeyCode.Space);

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
            if (jumpInput) {
                jumpingSound.Play();
                velocity.y = jumpForce;
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
