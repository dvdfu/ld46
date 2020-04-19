using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour {
    [SerializeField] float rps = 1; // Rotations per second
    [SerializeField] Transform spinTransform;

    float angle = 0;

    public float RotationsPerSecond {
        get { return rps; }
        set { rps = value; }
    }

    void FixedUpdate() {
        angle += Time.deltaTime * rps * 360;
        spinTransform.eulerAngles = Vector3.forward * angle;
    }
}
