using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : Entity
{
    private bool playerDetected;
    private float idleTimer;
    private float idleFlipTime = 7f;

    protected override void Update()
    {
        HandleCollision();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
        HandleDirection();
        HandleAttack();
    }

    protected override void HandleAttack()
    {
        if (playerDetected)
            anim.SetTrigger("attack");
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
