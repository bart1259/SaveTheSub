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
        Vector2 origin = new Vector2(transform.position.x, collider2D.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.05f, groundMask);

        if (hit.transform != null) {
            grounded = true;
        } else {
            grounded = false;
        }

        // Check if player hit head
        origin = new Vector2(transform.position.x, collider2D.bounds.max.y);
        hit = Physics2D.Raycast(origin, Vector2.up, 0.05f, groundMask);

        if (hit.transform != null) {
            hitTop = true;
        } else {
            hitTop = false;
        }
    }
}
