using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float hSpeed, vSpeed, runSpeed;

    float horizontal, vertical;

    public Rigidbody rb;

    Inputs inputs;

    Vector3 speedVector;
    Vector3 lastPosition;

    int count = 0;

    bool run;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speedVector = Vector3.zero;
        lastPosition = transform.localPosition;

        hSpeed = 0.17f;
        vSpeed = 0.1f;
        runSpeed = 3.5f;

        inputs = GetComponent<Inputs>();
    }


    private void FixedUpdate()
    {
        // Controlamos el limite de velocidad cada 10 fotogramas
        if (count == 10 || count == 0)
        {
            lastPosition = transform.position;
            count = 0;
        }

        horizontal = inputs.GetHorizontalAxis();
        vertical = inputs.GetVerticalAxis();

        Vector3 direction = new(horizontal, 0, vertical);

        if (Input.GetKey(KeyCode.LeftShift) && direction != Vector3.zero)
        {
            if (!run)
            {
                hSpeed *= runSpeed;
                vSpeed *= runSpeed;
                run = true;
            }
        }
        else
        {
            if (run)
            {
                run = false;
                hSpeed /= runSpeed;
                vSpeed /= runSpeed;
            }
        }

        if (speedVector.x > -hSpeed && speedVector.x < hSpeed &&
            speedVector.y > -hSpeed && speedVector.y < hSpeed &&
            speedVector.z > -hSpeed && speedVector.z < hSpeed)
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

        rb.AddRelativeForce(direction * 10);
    }
}
