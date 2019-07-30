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

    BoxCollider2D col;
    CircleCollider2D weaponCol;

    private bool dead;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponentInChildren<Animator>();
        col = GetComponent<BoxCollider2D>();
        weaponCol = GetComponentInChildren<CircleCollider2D>();
        attack = false;
        jump = false;

        weaponCol.gameObject.SetActive(false);

        dead = false;
    }

    private void Update()
    {
        if (!dead)
        {
            InputHandler();
            //HandleAnimations();
            HandleCharacterMovement();
        }
    }
    /// <summary>
    /// Method to handle character movement
    /// </summary>
    private void HandleCharacterMovement()
    {
        if (attack)
        {
            weaponCol.gameObject.SetActive(true);
        }
        else
        {
            weaponCol.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (jump && GroundCheck())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
    /// <summary>
    /// Raycast to check if player is hitting the ground
    /// </summary>
    /// <returns></returns>
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

        if (attack) { 
            if (Input.GetMouseButtonUp(0))
            {
                attack = false;
            }
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
