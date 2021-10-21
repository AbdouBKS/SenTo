using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    private bool jump = false;               // Condition for whether the player should jump.
    private float xAxis;

    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
    public float jumpForce = 1000f;         // Amount of force added when the player jumps.

    //ground
    [SerializeField] Transform groundTransform; //This is supposed to be a transform childed to the player just under their collider.
    [SerializeField] float groundCheckY = 0.2f; //How far on the Y axis the groundcheck Raycast goes.
    [SerializeField] float groundCheckX = 1;//Same as above but for X.
    [SerializeField] LayerMask groundLayer;

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
    private Vector2 savedVelocity;
    private bool dashButtonPressed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventoryManager = GetComponent<InventoryManager>();
        anim = GetComponent<Animator>();
        dashTime = startDashTime;
        dashInterval = startDashInterval;
    }

    void Update()
    {
        Dash();
        GetInputs();

        if (xAxis > 0 && !facingRight)
            Flip();
        else if (xAxis < 0 && facingRight)
            Flip();
    }

    void GetInputs()
    {
        xAxis = Input.GetAxis("Horizontal");

        //move
        anim.SetFloat("Speed", Mathf.Abs(xAxis));
        rb.velocity = new Vector2(xAxis * maxSpeed, rb.velocity.y);

        if (xAxis != 0 && Grounded())
            anim.SetBool("Running", true);
        else
            anim.SetBool("Running", false);

        if (Input.GetButtonDown("Jump") && Grounded())
            jump = true;

        //Debug.Log(Input.GetButtonDown("RT"));

        dashButtonPressed = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("RT");
    }

    public bool Grounded()
    {
        if (Physics2D.Linecast(transform.position, groundTransform.position, groundLayer)
            || Physics2D.Linecast(transform.position, groundTransform.position + new Vector3(-groundCheckX, 0), groundLayer)
            || Physics2D.Linecast(transform.position, groundTransform.position + new Vector3(groundCheckX, 0), groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void FixedUpdate()
    {
        //set jump anim
        if (Grounded())
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

            if (dashButtonPressed && canDash)
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
        if (Grounded() && dashInterval <= 0)
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
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(groundTransform.position, groundTransform.position + new Vector3(0, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(-groundCheckX, 0), groundTransform.position + new Vector3(-groundCheckX, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(groundCheckX, 0), groundTransform.position + new Vector3(groundCheckX, -groundCheckY));
    }
}
