using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    public float mouseSpeed;
    public float horizontalSpeed;

    public PlayerCenterRotation centerRotation;

    private void Start()
    {
        mouseSpeed = 2.5f;
        horizontalSpeed = 0.1f;
    }

    void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            transform.eulerAngles += mouseSpeed * new Vector3(0, Input.GetAxis("Mouse X"), 0);
        }

        if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            transform.eulerAngles += horizontalSpeed * new Vector3(0, 
                                                            Input.GetAxis("Horizontal"), 0);
        }
    }
}
