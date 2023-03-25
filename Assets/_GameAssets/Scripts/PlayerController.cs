using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovmentController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float gravity = 5.0f;
    public float jumpyForce = 5.0f;

    private float ySpeed = 0;
    private float xSpeed = 0;
    private MovmentController movmentController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentsInChildren<Animator>()[0];
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>()[0];
        movmentController = GetComponent<MovmentController>();
    }

    void Update() {
        // Flip sprites
        if(xSpeed < 0) {
            spriteRenderer.flipX = true;
        }
        if(xSpeed > 0) {
            spriteRenderer.flipX = false;
        }

        // Update animation states
        if(movmentController.grounded && Mathf.Abs(xSpeed) < 0.01f) {
            animator.SetBool("Idling",  true);
            animator.SetBool("Jumping", false);
            animator.SetBool("Running", false);
        } else if (!movmentController.grounded) {
            animator.SetBool("Jumping", true);
            animator.SetBool("Idling",  false);
            animator.SetBool("Running", false);
        } else {
            animator.SetBool("Running", true);            
            animator.SetBool("Jumping", false);
            animator.SetBool("Idling",  false);
        }
    }

    // Update is called once per physics frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        xSpeed = horizontalInput * moveSpeed;
        if(movmentController.grounded) {
            ySpeed = Mathf.Max(0.0f, ySpeed);
        } else if (movmentController.hitTop) {
            ySpeed = Mathf.Min(0.0f, ySpeed);
        }
        ySpeed -= gravity * Time.deltaTime;

        if (movmentController.grounded && Input.GetKey("space")) {
            ySpeed += jumpyForce;
        }

        Vector2 moveVector = new Vector2(xSpeed, ySpeed) * Time.deltaTime;

        movmentController.Move(moveVector);
    }
}
