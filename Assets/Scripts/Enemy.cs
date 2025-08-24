using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : Entity
{
    private bool playerDetected;
    private float idleTimer;
    private float idleFlipTime = 5f;
    private float pauseTime = 10f;
    private float walkTime = 5f;

    private void Start()
    {
        StartCoroutine (HandleRest());
    }

    protected override void Update()
    {
        HandleCollision();
        HandleMovement();
        HandleRest();
        HandleAnimations();
        HandleDirection();
        HandleAttack();
    }

    protected override void HandleAttack()
    {
        if (playerDetected)
            anim.SetTrigger("attack");
    }

    private IEnumerator HandleRest()
    {
        while (true)
        {
            // Walk for set time
            canMove = true;
            yield return new WaitForSeconds(walkTime);

            // Stop for pauseTime
            canMove = false;
            yield return new WaitForSeconds(pauseTime);

            HandleFlip();

        }
    }

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(facingDir * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private void HandleDirection()
    {
        if (playerDetected)
        {
            idleTimer = 0f; // reset timer if player is seen
        }
        else
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= idleFlipTime)
            {
                Flip();
                idleTimer = 0f; // reset after flipping
            }
        }
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsTarget);
    }
}
