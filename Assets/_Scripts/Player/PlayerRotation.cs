using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    public float speed;

    private void Start()
    {
        speed = 2.5f;
    }

    void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            transform.eulerAngles += speed * new Vector3(0,
                Input.GetAxis("Mouse X"), 0);
        }
    }
}
