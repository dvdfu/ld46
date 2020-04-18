using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    const float MAX_SPEED = 500;

    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D body;

    void FixedUpdate() {
        Vector2 moveDirection = (target.position - transform.position).normalized;
        body.AddForce(moveDirection.normalized * MAX_SPEED);
    }
}
