using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    public float life;
    public float speed;
    public float runSpeed;

    public Rigidbody rb;
    public Transform transf;
    
    Animator animator;

    Vector3 speed_vector;
    Vector3 last_position;

    int count = 0;

    bool front, back, left, right, run;


    private void Start()
    {
        life = 100;
        speed = 0.17f;
        runSpeed = 3.5f;
        transf = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        speed_vector = Vector3.zero;

        last_position = transf.position;
    }


    private void FixedUpdate()
    {
        if (count == 10 || count == 0)
        {
            last_position = transf.position;
            count = 0;
        }
        
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");


        Vector3 direction = new Vector3(horizontal, 0, vertical);
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!run)
            {
                speed *= runSpeed;
            }
            run = true;
        }
        else
        {
            if (run)
            {
                speed /= runSpeed;
            }
            run = false;
        }

        SetAnimValues(direction);

        if (speed_vector.x > -speed && speed_vector.x < speed && 
            speed_vector.y > -speed && speed_vector.y < speed &&
            speed_vector.z > -speed && speed_vector.z < speed)
        {
            MovePlayer(direction);
        }

        Vector3 actual_position = transf.position;

        speed_vector = new Vector3(actual_position.x - last_position.x,
            actual_position.y - last_position.y,
            actual_position.z - last_position.z);

        count++;
    }

    private void MovePlayer(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            rb.drag = 0;
        }
        else
        {
            rb.drag = 1;
        }

        if (!front && !back)
        {
            rb.AddRelativeForce(direction * 10);
        }
        else
        {
            rb.AddRelativeForce(new Vector3(0, 0, direction.z * 10));
        }
        
        

        

        animator.SetBool("Front", front);
        animator.SetBool("Back", back);
        animator.SetBool("Left", left);
        animator.SetBool("Right", right);
        animator.SetBool("Running", run);
    }

    private void SetAnimValues(Vector3 direction)
    {
        if (direction.z > 0)
        {
            front = true;
            back = false;
        }
        else if (direction.z < 0)
        {
            front = false;
            back = true;
        }
        else
        {
            front = false;
            back = false;
        }

        if (direction.x > 0)
        {
            left = false;
            right = true;
        }
        else if (direction.x < 0)
        {
            left = true;
            right = false;
        }
        else
        {
            left = false;
            right = false;
        }

        if (direction == Vector3.zero)
        {
            front = back = left = right = false;
        }
    }
}
