using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    const float MAX_SPEED = 500;

    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D body;

    Vector2 moveDirection;
    bool shouldChase = true;

    void Start() {
        StartCoroutine(ChaseRoutine());
    }

    void FixedUpdate() {
        if (shouldChase) {
            moveDirection = (target.position - transform.position).normalized;
        }
        body.AddForce(moveDirection.normalized * MAX_SPEED);
    }

    IEnumerator ChaseRoutine() {
        while (true) {
            yield return new WaitForSeconds(4 + 2 * Random.value);
            shouldChase = false;
            yield return new WaitForSeconds(1 + Random.value);
            shouldChase = true;
        }
    }
}
