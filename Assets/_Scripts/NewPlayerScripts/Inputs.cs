using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;

    float verticalAxis, horizontalAxis;
    float currentAnimVertical, currentAnimHorizontal;

    bool pressedVertical, pressedHorizontal, running;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
    }


    void Update()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");

        currentAnimHorizontal = animator.GetFloat("hSpeed");
        currentAnimVertical = animator.GetFloat("vSpeed");

        pressedVertical = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        pressedHorizontal = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
        running = Input.GetKey(KeyCode.LeftShift);
    }

    public float GetVerticalAxis()
    {
        return verticalAxis;
    }

    public float GetHorizontalAxis()
    {
        return horizontalAxis;
    }

    public bool GetPressedVertical()
    {
        return pressedVertical;
    }

    public bool GetPressedHorizontal()
    {
        return pressedHorizontal;
    }

    public bool GetRunning()
    {
        return running;
    }

    public float GetCurrentAnimatorVertical()
    {
        return currentAnimVertical;
    }

    public float GetCurrentAnimatorHorizontal()
    {
        return currentAnimHorizontal;
    }
}
