using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    void Start() {
        Camera.main.gameObject.GetComponent<CameraHelper>().Shake();
    }
}
