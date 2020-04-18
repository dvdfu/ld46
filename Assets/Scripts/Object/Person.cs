using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
    [SerializeField] GameObject tombstonePrefab;
    [SerializeField] GameObject poofPrefab;
    [SerializeField] Rigidbody2D body;
    [SerializeField] Sprite8Directional sprite8Directional;

    float angle;

    public void OnDie() {
        Instantiate(tombstonePrefab, transform.position, Quaternion.identity, transform.parent);
        Instantiate(poofPrefab, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    void Start() {
        angle = Random.value * 360;
        StartCoroutine(RunRoutine());
    }

    void FixedUpdate() {
        body.AddForce(MathUtils.PolarToCartesian(angle, 40));
    }

    void LateUpdate() {
        sprite8Directional.SetAngle(angle);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        angle = 180 - angle;
    }

    IEnumerator RunRoutine() {
        while (true) {
            yield return new WaitForSeconds(1 + Random.value);
            angle = angle + Random.Range(-45, 45);
        }
    }
}
