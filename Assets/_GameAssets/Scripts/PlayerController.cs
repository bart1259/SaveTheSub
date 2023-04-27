using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MovmentController))]
public class PlayerController : MonoBehaviour
{
    public delegate void PlayerDieEventHandler();
    public event PlayerDieEventHandler OnPlayerDie;

    public float moveAcceleration = 2.0f;
    public float maxSpeed = 3.0f;
    public float gravity = 5.0f;
    public float jumpForce = 5.0f;
    public float stopDragMultiplier = 10;

    public float wallJumpAngle = 45.0f;
    public float wallJumpForce = 5.0f;

    public int maxHP = 5;

    private float ySpeed = 0;
    private float xSpeed = 0;
    private MovmentController movmentController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource jumpSound;
    private AudioSource deathSound;
    private AudioSource[] sounds;

    private int hp;
    private bool spaceHit = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        animator = GetComponentsInChildren<Animator>()[0];
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>()[0];
        movmentController = GetComponent<MovmentController>();
        sounds = GetComponents<AudioSource>();
        jumpSound = sounds[0];
        deathSound = sounds[1];
    }

    void Update() {
        // Flip sprites
        if(xSpeed < 0) {
            spriteRenderer.flipX = true;
        }
        if(xSpeed > 0) {
            spriteRenderer.flipX = false;
        }

        if(Input.GetKeyDown("space")) {
            spaceHit = true;
        }

        // Update animation states
        if(movmentController.grounded && Mathf.Abs(xSpeed) < 0.15f) {
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

        xSpeed += horizontalInput * moveAcceleration * Time.deltaTime;

        // Kill horizontal movement
        if(movmentController.hitLeft && horizontalInput < 0) {
            xSpeed = 0.0f;
        }
        if (movmentController.hitRight && horizontalInput > 0) {
            xSpeed = 0.0f;
        }

        // Apply drag
        float dragCoeff = moveAcceleration / (maxSpeed * maxSpeed);
        if(Mathf.Abs(horizontalInput) < 0.5f && movmentController.grounded) {
            dragCoeff *= (1-Mathf.Abs(horizontalInput)) * stopDragMultiplier;
        }
        xSpeed -= (Mathf.Sign(xSpeed) * Mathf.Min((xSpeed * xSpeed * dragCoeff) * Time.deltaTime, Mathf.Abs(xSpeed)));

        if(movmentController.grounded) {
            ySpeed = Mathf.Max(0.0f, ySpeed);
        } else if (movmentController.hitTop) {
            ySpeed = Mathf.Min(0.0f, ySpeed);
        }
        ySpeed -= gravity * Time.deltaTime;

        if (movmentController.grounded && spaceHit) {
            // Jump
            ySpeed = jumpForce;
            jumpSound.Play();
        }
        else if (movmentController.hitLeft && spaceHit && horizontalInput < -0.1f) {
            // Wall Jump
            ySpeed = wallJumpForce * Mathf.Sin(Mathf.Deg2Rad * wallJumpAngle);
            xSpeed = wallJumpForce * Mathf.Cos(Mathf.Deg2Rad * wallJumpAngle);
        }
        else if (movmentController.hitRight && spaceHit && horizontalInput > 0.1f) {
            // Wall Jump
            ySpeed = wallJumpForce * Mathf.Sin(Mathf.Deg2Rad * wallJumpAngle);
            xSpeed = -wallJumpForce * Mathf.Cos(Mathf.Deg2Rad * wallJumpAngle);
        }

        Vector2 moveVector = new Vector2(xSpeed, ySpeed) * Time.deltaTime;

        movmentController.Move(moveVector);
        spaceHit = false;
    }

    public void TakeDamage(int amount) {
        hp -= amount;
        if(hp < 0) {
            Die();
        }
    }

    public void Die() {
        xSpeed = 0.0f;
        ySpeed = 0.0f;
        deathSound.Play();
        if(OnPlayerDie != null) {
            OnPlayerDie();
        }
    }
}
