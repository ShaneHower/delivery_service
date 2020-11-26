using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float speed = 150.0f;
    public float dstFromTarget = 50.0f;
    public float smoothTimePos = 0.4f;

    Vector3 smoothVP;

    // Update is called once per frame
    void LateUpdate()
    {
        moveCamera();
    }

    void moveCamera()
    {
        Vector3 newPosition = target.position - (transform.forward * dstFromTarget);

        //SmoothDamp makes the camera feel like the player is pulling it along.  Think of a balloon on a string tied to the PC
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref smoothVP, smoothTimePos);
    }
}
