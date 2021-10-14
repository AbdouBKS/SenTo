using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
	private Animator anim;                  // Reference to the player's animator component.
    private InventoryManager inventoryManager;

    private Rigidbody2D rb;

    //dash
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private bool dashing;
    private float horizontal;
    public Vector2 savedVelocity;


    void Start()
	{
        rb = GetComponent<Rigidbody2D>();
        inventoryManager = GetComponent<InventoryManager>();
        groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();

        dashing = false;
        dashTime = startDashTime;
	}

    void Update()
    {
        Dash();
        horizontal = Input.GetAxis("Horizontal");
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;

        //move
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        rb.velocity = new Vector2(horizontal * maxSpeed, rb.velocity.y);

        //flip
        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();



    }

	void FixedUpdate()
	{
        //set run anim
		if (horizontal != 0 && grounded)
			anim.SetBool("Running", true);
        else
			anim.SetBool("Running", false);
		
        
        //set jump anim
		if (grounded)
			anim.SetBool("Jump", false);
        else
            anim.SetBool("Jump", true);
        

        //jump system
        if (jump)
		{
			rb.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
    }

    void Dash()
    {
        //dash
        if (!dashing)
        {
            savedVelocity = rb.velocity;

            if (Input.GetKeyDown(KeyCode.LeftShift))
                dashing = true;
        }
        else
        {
            if (dashTime <= 0)
            {
                dashing = false;
                dashTime = startDashTime;
                rb.velocity = savedVelocity;
            }
            else
            {
                dashTime -= Time.deltaTime;


                if (facingRight)
                {
                    rb.AddForce(new Vector2(dashSpeed, 0f));
                }
                else if (!facingRight)
                {
                    rb.AddForce(new Vector2(-dashSpeed, 0f));
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Pickup"))
        {
            PickUp item = hit.GetComponent<PickUp>();
            inventoryManager.Add(item);
            Destroy(hit.gameObject);
        }
    }

    void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
