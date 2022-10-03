using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpSpeed = 5f;
    Animator runAnimator;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCapsuleCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        runAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
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

}
