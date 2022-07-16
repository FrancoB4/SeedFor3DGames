using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackSide : MonoBehaviour
{
    Transform tr;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            tr.localPosition = new(-tr.localPosition.x,
                tr.localPosition.y, tr.localPosition.z);
        }
    }
}
