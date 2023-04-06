using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovmentController : MonoBehaviour
{
    public LayerMask groundMask;
    // [HideInInspector()]
    public bool grounded;
    public bool hitTop;
    public bool hitLeft;
    public bool hitRight;
    public int groundRays = 5;

    private Rigidbody2D rigidBody2D;
    private new Collider2D collider2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        collider2D =  GetComponent<Collider2D>();
    }

    public void Move(Vector2 movementVector) {
        rigidBody2D.MovePosition(transform.position + new Vector3(movementVector.x, movementVector.y, 0));

        // Check if player is grounded
        float startX = collider2D.bounds.min.x;
        float endX = collider2D.bounds.max.x;
        grounded = false;
        for(int i = 0; i < groundRays; i++) {
            Vector2 origin = new Vector2(Mathf.Lerp(startX, endX, i / (float)(groundRays-1)), collider2D.bounds.min.y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.05f, groundMask);
            if (hit.transform != null) {
                grounded = true;
                break;
            }
        }

        hitTop = false;
        for(int i = 0; i < groundRays; i++) {
            Vector2 origin = new Vector2(Mathf.Lerp(startX, endX, i / (float)(groundRays-1)), collider2D.bounds.max.y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, 0.05f, groundMask);
            if (hit.transform != null) {
                hitTop = true;
                break;
            }
        }

        float startY = collider2D.bounds.min.y;
        float endY = collider2D.bounds.max.y;
        hitLeft = false;
        for(int i = 0; i < groundRays; i++) {
            Vector2 origin = new Vector2(collider2D.bounds.min.x, Mathf.Lerp(startY, endY, i / (float)(groundRays-1)));
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.left, 0.05f, groundMask);
            if (hit.transform != null) {
                hitLeft = true;
                break;
            }
        }

        hitRight = false;
        for(int i = 0; i < groundRays; i++) {
            Vector2 origin = new Vector2(collider2D.bounds.max.x, Mathf.Lerp(startY, endY, i / (float)(groundRays-1)));
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right, 0.05f, groundMask);
            if (hit.transform != null) {
                hitRight = true;
                break;
            }
        }
    }
}
