using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform spotlightTransform; // Reference to the spotlight
    [SerializeField] private float wallSlideSpeed = 2f; // Speed at which the player slides down the wall
    [SerializeField] private float groundSlideSpeed = 2f; // Speed at which the player slides on the ground
    [SerializeField] private float wallJumpCooldown = 0.5f; // Cooldown period for wall jumps
    private Rigidbody2D body;
    private Animator anim;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding; // Flag to track if the player is sliding down the wall
    private bool isTouchingSide; // Flag to track if the player is sliding on the ground
    private int jumps = 1;
    private bool canDoubleJump; // Flag to track if the player can perform a double jump
    private float lastWallJumpTime; // Timestamp of the last wall jump
    public float timer = 15f;
    private float maxTime = 10f;
    private bool isTimerRunning = true;
    private SpriteRenderer spriteRenderer;
    private float lastRotationAngle = -90f;
	public Text timerText;
	public SodaManager sm;
	public SodaManager bt;
	public AudioSource src;
	[SerializeField] AudioSource bgm;
	public AudioClip background;
	public AudioClip pick;
	public AudioClip banana;
	public AudioClip soda;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		src.clip = pick;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
                Die();
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        Vector2 rotationDirection = Vector2.zero;
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true; // Flip sprite if moving left
            rotationDirection = Vector2.right; // Rotate spotlight to the right
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false; // Don't flip sprite if moving right
            rotationDirection = Vector2.left; // Rotate spotlight to the right
        }

        RotateSpotlight(rotationDirection);

        if ((isGrounded || jumps > 0) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
            JumpAnimation();
                // Wall sliding
        if (isTouchingWall && !isGrounded && body.velocity.y < 0)
        {
            isWallSliding = true;
            canDoubleJump = true; // Enable double jump when touching a wall
        }
        else
        {
            isWallSliding = false;
        }

        // Ground sliding
        if (isTouchingSide && !isGrounded && Mathf.Abs(horizontalInput) > 0)
        {
            SlideOnGround(horizontalInput);
        }
		timerText.text = timer.ToString();
    }

    private void FixedUpdate()
    {
        if (isWallSliding)
        {
            // Apply downward force to slide down the wall
            body.velocity = new Vector2(body.velocity.x, -wallSlideSpeed);
        }
    }

    private void JumpAnimation()
    {
        if (isWallSliding && canDoubleJump) // Double jump while touching a wall
        {
            WallJump();
        }
        if (isGrounded || jumps > 0)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            jumps--;
        }
        anim.SetInteger("jujmo", Mathf.Abs((int)body.velocity.y));
    }

    private void WallJump()
    {
        // Check if enough time has passed since the last wall jump
        if (Time.time - lastWallJumpTime > wallJumpCooldown)
        {
            // Perform wall jump
            float wallSide = isTouchingWall ? -1f : 1f;
            body.velocity = new Vector2(jumpForce * wallSide, jumpForce);

            // Update last wall jump time
            lastWallJumpTime = Time.time;
        }
        canDoubleJump = false; // Disable double jump after wall jump
    }

    private void SlideOnGround(float horizontalInput)
    {
        // Apply horizontal force to slide on the ground
        body.velocity = new Vector2(horizontalInput * groundSlideSpeed, body.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumps = 1;
            anim.SetInteger("jujmo", 0);
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            isTouchingWall = true;
            canDoubleJump = true; // Enable double jump when touching a wall
            jumps = 1;
			anim.SetInteger("jujmo", 0);   
        }
        else if (collision.contacts[0].normal.y == 0)
        {
            // If the collision is with a side and not the ground, set the isTouchingSide flag to true
            isTouchingSide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            isTouchingWall = false;
            isWallSliding = false; // Reset wall sliding flag when leaving the wall
        }
        else
        {
            // If the collision was with a side and not the ground, set the isTouchingSide flag to false
            isTouchingSide = false;
        }
    }

    private void RotateSpotlight(Vector2 rotationDirection)
    {
        if (spotlightTransform != null)
        {
            float angle = 0f; // Default angle
	
        // Calculate the angle from the rotationDirection vector if it's not zero
            if (rotationDirection != Vector2.zero)
            {
                angle = Mathf.Atan2(rotationDirection.y, rotationDirection.x) * Mathf.Rad2Deg;
                angle += 90f; // Adjust the angle to properly align with the sprite
                lastRotationAngle = angle; // Update the last rotation angle
            }
            else
            {
                angle = lastRotationAngle; // Use the last rotation angle if no input
            }
            spotlightTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShiraMelonSoda"))
        {
            sm.sodaCount++;
			src.clip = soda;
			if(sm.sodaCount == 5)
			{
				src.clip = banana;
			}
			src.Play();
			
        }
		if (other.gameObject.CompareTag("Battery"))
		{
			src.clip = pick;
        	other.gameObject.SetActive(false);
        	timer = timer + maxTime;
			bt.batCount++;
			src.Play();
		}

    }

    private void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("You Died");
    }
}