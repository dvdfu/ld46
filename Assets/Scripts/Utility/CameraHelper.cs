using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour {
    [SerializeField] Camera camera;
    [SerializeField] Transform target;

    Vector3 velocity = Vector3.zero;

    void FixedUpdate() {
        Vector3 position = target.position;
        position.z = -10;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, position, ref velocity, 0.25f);
    }
}
