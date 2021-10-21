using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	public bool jump = false;               // Condition for whether the player should jump.
    private float horizontal;

    public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.

    //ground
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.

    //components
	private Animator anim;                  // Reference to the player's animator component.
    private InventoryManager inventoryManager;
    private Rigidbody2D rb;

    //dash
    public float dashSpeed;
    public float startDashTime; //lenght of the dash
    private float dashTime;
    private bool dashing = false;
    private bool canDash = true;
    public float startDashInterval;
    private float dashInterval;
    public Vector2 savedVelocity;


    void Start()
	{
        rb = GetComponent<Rigidbody2D>();
        inventoryManager = GetComponent<InventoryManager>();
        anim = GetComponent<Animator>();

        groundCheck = transform.Find("groundCheck");
        dashTime = startDashTime;
        dashInterval = startDashInterval;
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

            if (dashInterval > 0)
                dashInterval -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                canDash = false;
                dashing = true;
            }
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
        if (grounded && dashInterval <= 0)
        {
            dashInterval = startDashInterval;
            canDash = true;
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
