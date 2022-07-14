using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public float speed;
    public float runSpeed;

    public Rigidbody rb;

    AnimationController animControl;

    Vector3 speedVector;
    Vector3 lastPosition;

    int count = 0;

    bool run;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speedVector = Vector3.zero;
        lastPosition = transform.position;

        speed = 0.17f;
        runSpeed = 3.5f;

        animControl = GetComponent<AnimationController>();
    }


    private void FixedUpdate()
    {
        // Controlamos el limite de velocidad cada 10 fotogramas
        if (count == 10 || count == 0)
        {
            lastPosition = transform.position;
            count = 0;
        }

        float forward = Input.GetAxis("Vertical");
        float sides = Input.GetAxis("Horizontal");

        float polarizedForward = Polarize(forward);
        float polarizedSides = Polarize(sides);

        Vector3 direction = new Vector3(polarizedSides, 0, polarizedForward);

        if (Input.GetKey(KeyCode.LeftShift) && direction != Vector3.zero)
        {
            if (!run)
            {
                speed *= runSpeed;
                run = true;
            }
        }
        else
        {
            if (run)
            {
                run = false;
                speed /= runSpeed;
            }
        }

        animControl.SetAnimValues(direction, run);

        if (speedVector.x > -speed && speedVector.x < speed &&
            speedVector.y > -speed && speedVector.y < speed &&
            speedVector.z > -speed && speedVector.z < speed)
        {
            MovePlayer(direction);
        }

        Vector3 actualPosition = transform.position;

        speedVector = new Vector3(actualPosition.x - lastPosition.x,
            actualPosition.y - lastPosition.y,
            actualPosition.z - lastPosition.z);

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

        if (direction.z == 0)
        {
            rb.AddRelativeForce(direction * 10);
        }
        else
        {
            rb.AddRelativeForce(new Vector3(0, 0, direction.z * 10));
        }
    }

    private float Polarize(float n)
    {
        if (n > 0)
        {
            return 1f;
        }
        else if (n < 0)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }
}
