using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public int slices;

    public float lerp = 0.01f;
    public float fCurrent, fTarget, sCurrent, sTarget;
    float fRange, sRange;

    string fValue, sValue;

    Animator anim;

    private void Start()
    {
        fCurrent = fTarget = 0;

        sCurrent = sTarget = 0;

        slices = 30;

        fValue = "forwardSpeed";
        sValue = "sideSpeed";

        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float fDif = Mathf.Abs(fTarget - fCurrent);
        float sDif = Mathf.Abs(sTarget - sCurrent);

        if (fDif > Mathf.Abs(fRange))
        {
            fCurrent = Mathf.Lerp(fCurrent + fRange, fCurrent, lerp);
            anim.SetFloat(fValue, fCurrent);
        }
        else if (fDif != 0)
        {
            fCurrent = fTarget;
            anim.SetFloat(fValue, fCurrent);
        }

        if (sDif > Mathf.Abs(sRange))
        {
            sCurrent = Mathf.Lerp(sCurrent + sRange, sCurrent, lerp);
            anim.SetFloat(sValue, sCurrent);
        }
        else if (sDif != 0)
        {
            sCurrent = sTarget;
            anim.SetFloat(sValue, sCurrent);
        }
    }

    public void SetTarget(float tar, string value)
    {
        if (value == "f")
        {
            fTarget = tar;
            fRange = (tar - fCurrent) / slices;
        }
        else if (value == "s")
        {
            sTarget = tar;
            sRange = (tar - sCurrent) / slices;
        }
    }

    public float GetTarget(string value)
    {
        if (value == "f")
        {
            return fTarget;
        }
        else if (value == "s")
        {
            return sTarget;
        }
        return 0;
    }

    public void SetAnimValues(Vector3 direction, bool run)
    {
        bool isSide = anim.GetBool("Sides");
        bool isForward = anim.GetBool("Forward");

        SetBools(direction, isSide, isForward);
        SetFloats(direction, run);
    }

    private void SetBools(Vector3 direction, bool s, bool f)
    {
        if (direction.x != 0 && !s)
        {
            anim.SetBool("Sides", true);
        }
        else if (direction.x == 0 && s)
        {
            anim.SetBool("Sides", false);
        }

        if (direction.z != 0 && !f)
        {
            anim.SetBool("Forward", true);
        }
        else if (direction.z == 0 && f)
        {
            anim.SetBool("Forward", false);
        }
    }

    private void SetFloats(Vector3 direction, bool run)
    {
        if (direction.z != 0)
        {
            if (run && fTarget != 1.5f)
            {
                SetTarget(1.5f, "f");
                slices = 30;
            }
            else if (!run && fTarget != 1f && direction.z > 0)
            {
                SetTarget(1f, "f");
            }
            else if (!run && fTarget != 0f && direction.z < 0)
            {
                SetTarget(0f, "f");
            }
        }
        else if (fTarget != 0.5f)
        {
            SetTarget(0.5f, "f");
        }

        if (direction.x > 0f && sTarget != 1f)
        {
            SetTarget(1f, "s");
        }
        else if (direction.x < 0f && sTarget != 0f)
        {
            SetTarget(0f, "s");
        }
        else if (direction.x == 0f && sTarget != 0.5f)
        {
            SetTarget(0.5f, "s");
        }
    }
}
