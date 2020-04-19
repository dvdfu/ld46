using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
    [SerializeField] GameObject tombstonePrefab;
    [SerializeField] GameObject poofPrefab;
    [SerializeField] Rigidbody2D body;
    [SerializeField] Sprite8Directional sprite8Directional;
    [SerializeField] CircleCollider2D collider;

    float angle;

    enum State {
        Running,
        WaitingForPickup
    }
    State state = State.Running;

    public void OnDie() {
        Instantiate(tombstonePrefab, transform.position, Quaternion.identity, transform.parent);
        Remove();
    }

    public void Remove() {
        Instantiate(poofPrefab, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    void Start() {
        angle = Random.value * 360;
        StartCoroutine(RunRoutine());
    }

    public void WaitForPickup() {
        state = State.WaitingForPickup;
        collider.enabled = false;
    }

    void FixedUpdate() {
        switch(state) {
            case State.Running:
                body.AddForce(MathUtils.PolarToCartesian(angle, 40));
                break;
            case State.WaitingForPickup:
                body.AddForce(MathUtils.PolarToCartesian(angle, 5));
                break;
        }
    }

    void LateUpdate() {
        sprite8Directional.SetAngle(angle);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        angle = 180 - angle;
    }

    IEnumerator RunRoutine() {
        while (true) {
            switch(state) {
                case State.Running:
                    yield return new WaitForSeconds(1 + Random.value);
                    angle = angle + Random.Range(-45, 45);
                    break;
                case State.WaitingForPickup:
                    yield return null;
                    angle = angle + 1f;
                    break;
            }
        }
    }
}
