using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    Vector2 moveInput;
    Animator runAnimator;
    Animator jumpAnimator;
    Animator climbAnimator;
    Animator deathAnimator;

    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    bool isAlive = true;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        runAnimator = GetComponent<Animator>();
        jumpAnimator = GetComponent<Animator>();
        climbAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        deathAnimator = GetComponent<Animator>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);

        }
        // Jump Animation
        // if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        // {
        //     runAnimator.SetBool("isJumping", true);
        // }
        // else
        // {
        //     runAnimator.SetBool("isJumping", false);
        // }

    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerIsRunningOrNot = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerIsRunningOrNot)
        {
            runAnimator.SetBool("isRunning", true);
        }
        else
        {
            runAnimator.SetBool("isRunning", false);
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidBody.gravityScale = gravityScaleAtStart;
            runAnimator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        bool playerIsClimbingOrNot = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        runAnimator.SetBool("isClimbing", playerIsClimbingOrNot);
        // or we can do it like this
        // bool playerIsClimbingOrNot = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        // if (playerIsClimbingOrNot)
        // { 
        //   runAnimator.SetBool("isRunning", true); 
        // }
        // else
        // { 
        //   runAnimator.SetBool("isRunning", false); 
        // }
    }
    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            deathAnimator.SetTrigger("Dying");
            myRigidBody.velocity = deathKick;
        }
    }
}
