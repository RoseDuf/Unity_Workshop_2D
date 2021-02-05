using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private int m_Speed;

    private Vector2 inputVector;
    private Vector2 moveAmount;
    private Vector2 smoothMoveVelocity;
    private Vector2 initialScale;

    private bool isMoving;
    private bool isGrounded;

    private Animator m_Animator;
    
    void Start()
    {
        initialScale = transform.localScale;
        isMoving = false;
        m_Animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        HandleInput();
        Movement();
    }

    void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        isMoving = horizontal != 0;

        inputVector = new Vector2(horizontal, 0f);

        moveAmount = Vector2.SmoothDamp(moveAmount, inputVector, ref smoothMoveVelocity, 0.15f);
    }

    void Movement()
    {
        //transform.position += new Vector3(inputVector.x, 0f, 0f) * m_Speed * Time.deltaTime;
        transform.Translate(inputVector * m_Speed * Time.deltaTime);
        FaceDirection();
    }

    void FaceDirection()
    {
        if (isMoving)
        {
            transform.localScale = new Vector3(inputVector.x * initialScale.x, transform.localScale.y, 0) ;
        }
    }

    //IsGrounded function that performs the logic and returns a boolean - true if the player is on the ground, false otherwise.
    //private void CheckGrounded()
    //{
    //    foreach (GroundCheck g in mGroundCheckList)
    //    {
    //        if (g.CheckGrounded(kGroundCheckRadius, mWhatIsGround, gameObject))
    //        {
    //            isGrounded = true;
    //            return;
    //        }
    //    }
    //    isGrounded = false;
    //}
}
