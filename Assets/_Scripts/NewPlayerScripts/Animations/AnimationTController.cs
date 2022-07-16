using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTController : MonoBehaviour
{
    Animator animator;
    Inputs inputs;

    float vSpeed, hSpeed;
    float targetHSpeed = 0, targetVSpeed = 0;
    float animH, animV;
    float currentTopSpeed = 0.5f;

    public float aceleration = 2f;
    public float deceleration = 2f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputs = GetComponent<Inputs>();
    }

    private void FixedUpdate()
    {
        vSpeed = inputs.GetVerticalAxis();
        hSpeed = inputs.GetHorizontalAxis();

        animH = inputs.GetCurrentAnimatorHorizontal();
        animV = inputs.GetCurrentAnimatorVertical();

        currentTopSpeed = SetCurrentTopSpeed(inputs.GetRunning());

        targetVSpeed = UpdateTargetValue(vSpeed, targetVSpeed, currentTopSpeed, aceleration, deceleration);
        targetHSpeed = UpdateTargetValue(hSpeed, targetHSpeed, currentTopSpeed, aceleration, deceleration);

        UpdateAnimatorValues(animH, animV, targetHSpeed, targetVSpeed);
    }

    private void UpdateAnimatorValues(float currentH, float currentV, float targetH, float targetV)
    {
        if (currentH != targetH)
        {
            animator.SetFloat("hSpeed", targetH);
        }

        if (currentV != targetV)
        {
            animator.SetFloat("vSpeed", targetV);
        }
    }

    private float UpdateTargetValue(float speed, float current, float topSpeed, float acel, float dec)
    {
        float newTarget = current;
        if (speed != 0)
        {
            if (OnRange(newTarget, topSpeed) || !SameSign(newTarget, speed))
            {
                newTarget = AceleratedValue(newTarget, speed, acel, dec);
            }
            else if (Mathf.Abs(newTarget) > topSpeed + 0.05f)
            {
                newTarget = DecelerateValue(newTarget, speed, acel, dec);
            }
            else
            {
                newTarget = topSpeed * Polarize(speed);
            }
        }
        else if (Mathf.Abs(newTarget) > .05f)
        {
            newTarget = DecelerateValue(newTarget, speed, acel, dec);
        }
        else if (newTarget != 0)
        {
            newTarget = 0;
        }

        return newTarget;
    }

    private float AceleratedValue(float value, float direction, float aceleration, float deceleration)
    {
        float aceleratedValue = value;
        if (direction > 0)
        {
            aceleratedValue += (Time.deltaTime * aceleration);
        }
        else if (direction < 0)
        {
            aceleratedValue -= (Time.deltaTime * aceleration);
        }
        else if (value != 0)
        {
            if (value > 0)
            {
                aceleratedValue += Time.deltaTime * deceleration;
            }
            else if (value < 0)
            {
                aceleratedValue -= Time.deltaTime * deceleration;
            }
        }
        return aceleratedValue;
    }

    private float DecelerateValue(float value, float direction, float aceleration, float deceleration)
    {
        float deceleratedValue = value;
        if (direction > 0)
        {
            deceleratedValue -= Time.deltaTime * deceleration;
        }
        else if (direction < 0)
        {
            deceleratedValue += Time.deltaTime * deceleration;
        }
        else if(value != 0)
        {
            if (value > 0)
            {
                deceleratedValue -= Time.deltaTime * deceleration;
            }
            else if( value < 0)
            {
                deceleratedValue += Time.deltaTime * deceleration;
            }
        }
        return deceleratedValue;
    }

    private float SetCurrentTopSpeed(bool running)
    {
        if (running)
        {
            return 2f;
        }
        return .5f;
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
        return 0f;
    }

    private bool SameSign(float a, float b)
    {
        return a * b > 0;
    }

    private bool OnRange(float speed, float topSpeed)
    {
        if (Mathf.Abs(speed) < topSpeed)
        {
            return true;
        }
        return false;
    }
}
