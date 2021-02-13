﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Shooting variables
    [SerializeField] private GameObject m_Projectile;
    private bool isShooting;

    // Movement variables
    [SerializeField] private float m_Speed;

    private Vector2 inputVector;
    private Vector2 directionVector;
    private Vector2 moveAmount;
    private Vector2 smoothMoveVelocity;
    private Vector2 initialScale;

    private bool isMoving;

    // Rigidbody variables
    [SerializeField] private float m_JumpForce;
    private Rigidbody2D m_Rigidbody;
    private bool isJumping;

    // Ground Check variables
    [SerializeField] private LayerMask m_WhatIsGround;
    private const float GROUND_CHECK_RADIUS = 0.1f;
    private List<ColliderCheck> m_GroundCheckList;
    private bool isGrounded;

    // Life variables
    public int lifeCounter { get; set; }
    public bool isHurt { get; set; }

    // Animation variables
    private Animator m_Animator;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();

        // Obtain ground check components and store in list
        m_GroundCheckList = new List<ColliderCheck>();
        ColliderCheck[] groundChecksArray = transform.GetComponentsInChildren<ColliderCheck>();
        foreach (ColliderCheck g in groundChecksArray)
        {
            m_GroundCheckList.Add(g);
        }
    }

    void Start()
    {
        isShooting = false;

        isJumping = false;

        isHurt = false;

        initialScale = transform.localScale;
        directionVector = new Vector2(transform.position.x/transform.position.x, 0);
        isMoving = false;

        m_Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        Movement();
    }

    void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Logic associated to button presses.
    /// </summary>
    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        isMoving = horizontal != 0;

        if (isMoving)
        {
            directionVector = new Vector2(horizontal, 0);
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            Shoot();
        }

        inputVector = new Vector2(horizontal, 0f);

        moveAmount = Vector2.SmoothDamp(moveAmount, inputVector, ref smoothMoveVelocity, 0.15f);
    }

    /// <summary>
    /// Handles transform and/or rigidbody operations needed to move character.
    /// </summary>
    void Movement()
    {
        if (isGrounded && isJumping)
        {
            m_Rigidbody.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }

        //transform.position += new Vector3(inputVector.x, 0f, 0f) * m_Speed * Time.deltaTime;
        transform.Translate(inputVector * m_Speed * Time.deltaTime);
        //m_Rigidbody.velocity = Vector2.SmoothDamp(m_Rigidbody.velocity, inputVector, ref smoothMoveVelocity, 0.15f);
        FaceDirection();
    }

    /// <summary>
    /// Adjusts player sprite's direction by rescaling.
    /// </summary>
    void FaceDirection()
    {
        if (isMoving)
        {
            transform.localScale = new Vector3(inputVector.x * initialScale.x, transform.localScale.y, 0);
        }
    }

    /// <summary>
    /// Performs the logic and returns a boolean - true if the player is on the ground, false otherwise.
    /// </summary>
    private void CheckGrounded()
    {
        foreach (ColliderCheck g in m_GroundCheckList)
        {
            if (g.CheckCollision(GROUND_CHECK_RADIUS, m_WhatIsGround, gameObject))
            {
                isGrounded = true;
                return;
            }
        }
        isGrounded = false;
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(m_Projectile, transform.position, m_Projectile.transform.rotation) as GameObject;
        projectile.GetComponent<Projectile>().Shoot(directionVector);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "enemy")
        {
            lifeCounter -= 1;
            isHurt = true;
        }
    }
}