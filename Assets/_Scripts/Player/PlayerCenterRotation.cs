using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCenterRotation : MonoBehaviour
{
    public Transform tr;

    public float speed;

    void Start()
    {
        tr = this.GetComponent<Transform>();
        speed = 2.5f;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            transform.eulerAngles += speed * new Vector3(-Input.GetAxis("Mouse Y"),
                Input.GetAxis("Mouse X"), 0);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            tr.rotation = new Quaternion(0, 0, 0 ,0);
        }
        else
        {
            transform.eulerAngles += speed * new Vector3(-Input.GetAxis("Mouse Y"),
                0, 0);
        }
    }
}
