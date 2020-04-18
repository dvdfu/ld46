using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPellet : MonoBehaviour {
    [SerializeField] Rigidbody2D body;

    void Start() {
        body.velocity = new Vector2(Random.value, 1) * 200;
    }
}
