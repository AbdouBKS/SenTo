using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	public bool jump = false;				// Condition for whether the player should jump.

	[HideInInspector]
	public bool running = false;				// Condition for whether the player should jump.

	// public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	// public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.


	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.


	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
    }

	void FixedUpdate()
	{
		// Cache the horizontal input.
		float horizontal = Input.GetAxis("Horizontal");

		// // The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(horizontal));

		// // If the player is changing direction (horizontal has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		// if(horizontal * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
		// 	// ... add a force to the player.
		// 	GetComponent<Rigidbody2D>().AddForce(Vector2.right * horizontal * moveForce);

		// // If the player's horizontal velocity is greater than the maxSpeed...
		// if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
		// 	// ... set the player's velocity to the maxSpeed in the x axis.

		GetComponent<Rigidbody2D>().velocity = new Vector2(horizontal * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (horizontal > 0 && !facingRight) {
			Flip();
		} else if(horizontal < 0 && facingRight) {
			Flip();
		}

		if (horizontal != 0 && grounded) {
			anim.SetBool("Running", true);
		} else {
			anim.SetBool("Running", false);
		}

		if (grounded) {
			anim.SetBool("Jump", false);
		}
        else
            anim.SetBool("Jump", true);

        // If the player should jump...
        if (jump)
		{
			// Set the Jump animator trigger parameter.
			

			// Add a vertical force to the player.
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

			jump = false;
		}
	}

    void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
