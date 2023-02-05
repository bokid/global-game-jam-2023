using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{    
    [SerializeField] private LayerMask physicalLayers;

    float horizontalVelocity = 0f;
    float verticalVelocity = 0f;
    int movementDirection = 0;
    bool isGrounded;

    [SerializeField]BoxCollider2D groundedBox;

    public float MOVE_SPEED = 0.1f;
    public float JUMP_FORCE = 0.5f;
    public float GRAVITY_FORCE = 1f;
    public float MAX_HORIZONTAL_SPEED = 0.8f;
    
    private void FixedUpdate() {
        GroundedCheck();
        HandleAcceleration();
        transform.position += new Vector3(horizontalVelocity, verticalVelocity, 0);
    }

    void GroundedCheck() {
        // please don't ask me how this works
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D noFilter = new ContactFilter2D();
        int hits = groundedBox.OverlapCollider(noFilter.NoFilter(), results);
Debug.Log(hits);

        isGrounded = false;
        for(int i = 0; i < hits; i++)
        {
            if (((1 << results[i].gameObject.layer) & physicalLayers) > 0)
            {
                isGrounded = true;
                break;
            }
        }
    }

    void HandleAcceleration() {
        // surely this can be made nicer somehow
        if (movementDirection == 1) {
            horizontalVelocity = Mathf.Clamp(horizontalVelocity + MOVE_SPEED, 0, MAX_HORIZONTAL_SPEED);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementDirection == -1) {
            horizontalVelocity = Mathf.Clamp(horizontalVelocity - MOVE_SPEED, -MAX_HORIZONTAL_SPEED, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else {
            if (horizontalVelocity < 0) {
                horizontalVelocity = Mathf.Clamp(horizontalVelocity + MOVE_SPEED, -MAX_HORIZONTAL_SPEED, 0);
            }
            else if (horizontalVelocity > 0) {
                horizontalVelocity = Mathf.Clamp(horizontalVelocity - MOVE_SPEED, 0, MAX_HORIZONTAL_SPEED);
            }
        }

        if (!isGrounded) {
            verticalVelocity -= GRAVITY_FORCE * Time.fixedDeltaTime;
        }
        else if (verticalVelocity < 0) {
            verticalVelocity = 0;
        }
    }

    public void HandleJump() {
        if(isGrounded) {
            verticalVelocity = JUMP_FORCE;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext c) {
        float movementX = c.ReadValue<Vector2>().x;
        if (movementX > 0.1) {
            movementDirection = 1;
        }
        else if (movementX < -0.1) {
            movementDirection = -1;
        }
        else {
            movementDirection = 0;
        }
    }


    public void OnJumpInput(InputAction.CallbackContext c) {
        if (c.started) {
            HandleJump();
        }
    }
}
