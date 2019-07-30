using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    public bool attack;
    public bool jump;

    Animator anim;
    [SerializeField] private bool isAttackPlaying;
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    public float jumpForce;

    Collider2D col;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();

        attack = false;
        jump = false;
    }

    private void Update()
    {
        InputHandler();
        HandleAnimations();
        HandleCharacterMovement();
    }

    private void HandleCharacterMovement()
    {
        if (jump && GroundCheck())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    public bool GroundCheck()
    {
        Vector2 footPosition = new Vector2(col.bounds.center.x, col.bounds.center.y - col.bounds.extents.y);
        if(Physics2D.Raycast(footPosition, Vector3.down, groundRaycastDistance, groundLayerMask))
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Method to handle player input
    /// </summary>
    private void InputHandler()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            jump = false;
        }

        if (Input.GetMouseButton(0))
        {
            attack = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            attack = false;
        }
    }
    /// <summary>
    /// Method to handle animations based on player input
    /// </summary>
    private void HandleAnimations()
    {
        if (attack)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if(jump)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }
}
