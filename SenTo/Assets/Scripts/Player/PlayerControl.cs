using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public PlayerStateList pState;

    [Header("Movement")]
    float xAxis;
    float yAxis;
    [SerializeField] float walkSpeed = 10f;             // The fastest the player can travel in the x axis.
    [SerializeField] float jumpForce = 1150;         // Amount of force added when the player jumps.
    [Space(5)]

    [Header("Ground")]
    [SerializeField] Transform groundTransform; //This is supposed to be a transform childed to the player just under their collider.
    [SerializeField] float groundCheckY = 0.2f; //How far on the Y axis the groundcheck Raycast goes.
    [SerializeField] float groundCheckX = 1;//Same as above but for X.
    [SerializeField] LayerMask groundLayer;
    [Space(5)]

    [Header("Components")]
    private Animator anim;                  // Reference to the player's animator component.
    private Rigidbody2D rb;
    [Space(5)]

    [Header("Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float startDashTime; //lenght of the dash
    float dashTime;
    [SerializeField] float startDashInterval; //time between dash
    float dashInterval;
    Vector2 savedVelocity;
    bool dashButtonPressed;
    bool canDash = true;

    void Start()
    {
        if (pState == null)
        {
            pState = GetComponent<PlayerStateList>();
        }
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        dashTime = startDashTime;
        dashInterval = startDashInterval;
    }

    void Update()
    {
        GetInputs();

        Flip();
        Walk();
        Dash();
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;

        if (xAxis < 0 && !pState.lookingRight)
        {
            pState.lookingRight = !pState.lookingRight;

            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else if (xAxis > 0 && pState.lookingRight)
        {
            pState.lookingRight = !pState.lookingRight;

            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void Walk()
    {
        anim.SetFloat("Speed", Mathf.Abs(xAxis));
        rb.velocity = new Vector2(xAxis * walkSpeed, rb.velocity.y);

        if (Mathf.Abs(rb.velocity.x) > 0 && Grounded())
        {
            pState.walking = true;
        }
        else
        {
            pState.walking = false;
        }
        anim.SetBool("Running", pState.walking);
    }

    void GetInputs()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical"); ;

        if (Input.GetButtonDown("Jump") && Grounded())
            pState.jumping = true;

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
        anim.SetBool("Jump", !Grounded());

        if (pState.jumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            pState.jumping = false;
        }
    }

    void Dash()
    {
        //dash
        if (!pState.dashing)
        {
            savedVelocity = rb.velocity;

            if (dashInterval > 0)
                dashInterval -= Time.deltaTime;

            if (dashButtonPressed && canDash)
            {
                canDash = false;
                pState.dashing = true;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                pState.dashing = false;
                dashTime = startDashTime;
                rb.velocity = savedVelocity;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (pState.lookingRight)
                {
                    rb.AddForce(new Vector2(-dashSpeed, 0f));
                }
                else
                {
                    rb.AddForce(new Vector2(dashSpeed, 0f));
                }
            }
        }
        if (Grounded() && dashInterval <= 0)
        {
            dashInterval = startDashInterval;
            canDash = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(groundTransform.position, groundTransform.position + new Vector3(0, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(-groundCheckX, 0), groundTransform.position + new Vector3(-groundCheckX, -groundCheckY));
        Gizmos.DrawLine(groundTransform.position + new Vector3(groundCheckX, 0), groundTransform.position + new Vector3(groundCheckX, -groundCheckY));
    }
}
