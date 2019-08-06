using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    public bool attack;
    public bool jump;
    private bool isJumping;
    private bool isFalling;
    public float jumpHeight;
    public float jumpSpeed;
    public float fallSpeed;
    Animator anim;
    [SerializeField] private bool isAttackPlaying;
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;
    BoxCollider2D col;
    CircleCollider2D weaponCol;
    List<SpriteRenderer> spriteRenderers;
    private bool dead;
    public bool isDead { get { return dead; } }
    private Vector3 startPosition;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<BoxCollider2D>();
        weaponCol = GetComponentInChildren<CircleCollider2D>();

        spriteRenderers = new List<SpriteRenderer>();
        SpriteRenderer[] spriteRendererArray = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer s in spriteRendererArray)
        {
            spriteRenderers.Add(s);
        }
        startPosition = transform.position;
     
    }
    private void InitializeCharacter()
    {
        attack = false;
        jump = false;
        isFalling = false;
        dead = false;
    }
    public void OnPlayerResetEvent()
    {
        transform.position = startPosition;
        foreach (SpriteRenderer s in spriteRenderers)
            s.transform.rotation = Quaternion.Euler(Vector3.zero);
        InitializeCharacter();
    }
    private void Update()
    {
        if (!dead)
        {
            if (!anim.gameObject.activeSelf)
                anim.gameObject.SetActive(true);

            InputHandler();
            HandleAnimations();
            HandleCharacterMovement();
        }
        else
        {
            anim.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Method to handle character movement
    /// </summary>
    private void HandleCharacterMovement()
    {
        if (jump)
        {
            if (transform.position.y < jumpHeight && !isFalling)
            {
                isJumping = true;
                CalculateCharacterJump();
            }
            else
            {
                isFalling = true;
            }
        }
        else
        {
            if (!GroundCheck()) { 
                isFalling = true;
            }
        }

        if (isFalling)
        {
            CalculateCharacterFall();
            if (GroundCheck())
                isFalling = false;
        }
    }
    private void CalculateCharacterJump()
    {
        transform.position = Vector2.Lerp(transform.position, (Vector2)transform.position + new Vector2(0, jumpHeight), Time.deltaTime * jumpSpeed);
    }
    private void CalculateCharacterFall()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, -3.5f), Time.deltaTime * fallSpeed);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            dead = true;
        }
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
