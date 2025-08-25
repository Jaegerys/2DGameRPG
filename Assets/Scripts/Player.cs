using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float jumpForce = 8;
    private float xInput;
    private float maxChargeTime = 2f;   // How long you can charge
    private float minChargeTime = 0.5f; // Minimum hold for a "charged" attack
    [SerializeField]private float charge;               // Current charge amount
    private bool isCharging;
    private bool canDoubleJump;

    protected override void Update()
    {
        HandleCollision();
        HandleInput();
        HandleChargeTime();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
        HandleAttack();
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            HandleJump();

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
            {
                anim.SetTrigger("attack1");
                damage = 15;
            }
                
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                anim.SetTrigger("attack2");
                damage = 10;
            }

        }
    }

    private void HandleChargeTime()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isCharging = true;
            charge = 0f;
            Debug.Log("Started charging...");
        }

        // While holding the button, increase charge
        if (isCharging && Input.GetKey(KeyCode.R))
        {
            charge += Time.deltaTime;
            charge = Mathf.Clamp(charge, 0f, maxChargeTime);
            Debug.Log("Charging: " + charge);
        }

        // Release button -> perform charge attack
        if (isCharging && Input.GetKeyUp(KeyCode.R))
        {
            isCharging = false;
            HandleChargeAttack();
        }
    }


    private void HandleChargeAttack()
    {
        if (charge >= minChargeTime)
        {
            float power = charge / maxChargeTime;
            Debug.Log("Charge Attack released! Power: " + power);

            anim.SetTrigger("attack3");

            // Example: apply damage scaling with charge
            damage = (int)Mathf.Lerp(10f, 50f, power);
            Debug.Log("Damage dealt: " + damage);
        }
        else
        {
            Debug.Log("Tap was too short — weak attack or nothing happens.");
        }
    }

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            canDoubleJump = true;
        }

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                TryToJump();
            }
            else if (canDoubleJump)
            {
                TryToJump();
                canDoubleJump = false; // Use up double jump
            }
        }
    }

    private void TryToJump()
    {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

}
