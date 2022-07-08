using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCenterRotation : MonoBehaviour
{
    public Transform tr;

    public float speed;

    Vector3 lastEulerAngles;

    bool first;

    void Start()
    {
        tr = this.GetComponent<Transform>();
        speed = 2.5f;
        first = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (first)
            {
                SaveLastState();
            }
            transform.eulerAngles += speed * new Vector3(-Input.GetAxis("Mouse Y"),
                Input.GetAxis("Mouse X"), 0);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            transform.eulerAngles = lastEulerAngles;
            first = true;
        }
        else
        {
            transform.eulerAngles += speed * new Vector3(-Input.GetAxis("Mouse Y"),
                0, 0);
        }
    }

    private void SaveLastState()
    {
        lastEulerAngles = transform.eulerAngles;
        first = false;
    }

}
