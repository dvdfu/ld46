using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
    [SerializeField] SessionData sessionData;
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
        sessionData.peopleDied++;
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

    public void PickupAfter(float delay) {
        StartCoroutine(PickupRoutine(delay));
    }

    void FixedUpdate() {
        if (state == State.Running) {
            body.AddForce(MathUtils.PolarToCartesian(angle, 40));
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
            if (state == State.Running) {
                angle = angle + Random.Range(-45, 45);
                yield return new WaitForSeconds(1 + Random.value);
            } else {
                yield return null;
            }
        }
    }

    IEnumerator PickupRoutine(float delay) {
        state = State.WaitingForPickup;
        collider.enabled = false;
        yield return new WaitForSeconds(delay);
        Remove();
    }
}
