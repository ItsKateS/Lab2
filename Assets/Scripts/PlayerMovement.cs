using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    float walkSpeed = 3f;
    float runSpeed = 10f;
    float backwardsSpeed = 2f;
    
    public LayerMask ground;
    public Transform groundCheck;
    
    Animator animator;
    string state = "State";

    void Start()
    {
        rb = GetComponent<Rigidbody>();       
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {  
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (!Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else
            {
                Fall();
            }
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            if (isGrounded())
                Idle();
            else
                Fall();
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Backwards();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") == -1)
        {
            transform.Rotate(Vector3.up, -90 * Time.fixedDeltaTime, Space.Self);
        }
        else if (Input.GetAxis("Horizontal") == 1)
        {
            transform.Rotate(Vector3.up, 90 * Time.fixedDeltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Kick();
        }
    }

    private void Idle()
    {
        animator.SetInteger(state, 0);
    }

    private void Walk()
    {
        animator.SetInteger(state, 1);
        rb.MovePosition(transform.position + (transform.forward * Time.deltaTime * walkSpeed));
    }

    private void Run()
    {
        animator.SetInteger(state, 2);
        rb.MovePosition(transform.position + (transform.forward * Time.deltaTime * runSpeed));
    }

    private void Backwards()
    {
        animator.SetInteger(state, -1);
        rb.MovePosition(transform.position + (-transform.forward * Time.deltaTime * backwardsSpeed));
    }

    private void Jump()
    {
        animator.SetInteger(state, 3);
        rb.velocity = new Vector3(0, 7, 0);
    }

    private void Fall()
    {
        animator.SetInteger(state, -2);
    }

    private void Kick()
    {
        animator.SetInteger(state, 4);
    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
}
