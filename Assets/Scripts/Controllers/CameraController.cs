using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    public Vector3 offSet;

    public float smoothSpeed = 0.125f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offSet;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // this feels a bit too wonky
        //transform.LookAt(player);
    }
}
