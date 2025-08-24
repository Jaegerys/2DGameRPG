using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float jumpForce = 8;
    private float xInput;


    protected override void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();

        if (Input.GetKeyDown(KeyCode.Mouse1))
            HandleAttack();

        if (Input.GetKeyDown(KeyCode.Mouse3))
            HandleAttack();
    }

    protected override void HandleAttack()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                anim.SetTrigger("attack1");
                
            if (Input.GetKeyDown(KeyCode.Mouse1))
                anim.SetTrigger("attack2");

            if (Input.GetKeyDown(KeyCode.Mouse3))
                anim.SetTrigger("attack3");
        }
    }

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private void TryToJump()
    {
        if (isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

}
