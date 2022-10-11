using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRigidBody;
    [SerializeField] float enemyMoveSpeed = 1f;
    [SerializeField] AudioClip enemyCrashSFX;
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyRigidBody.velocity = new Vector2(enemyMoveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enemyMoveSpeed = -enemyMoveSpeed;
        FlipEnemyFacing();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(enemyCrashSFX, Camera.main.transform.position);
        }
    }
    void FlipEnemyFacing()
    {
        bool enemyHasHorizontalMove = Mathf.Abs(enemyRigidBody.velocity.x) > Mathf.Epsilon;
        if (enemyHasHorizontalMove)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
        }
    }
}
