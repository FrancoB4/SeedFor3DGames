using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMousePosition : MonoBehaviour
{
    Vector2 mousePosition;

    public float x; public float y; public float z;

    void FixedUpdate()
    {
        mousePosition = Input.mousePosition;

        x = mousePosition.x;
        y = mousePosition.y;
    }
}
