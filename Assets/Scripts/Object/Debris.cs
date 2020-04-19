using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour {
    [SerializeField] GameObject firePrefab;
    [SerializeField] Rigidbody2D body;

    public void OnExpire() {
        // Instantiate(firePrefab, transform.position, Quaternion.identity, transform.parent);
    }

    void Start() {
        float angle = 90 + Random.Range(-45, 45);
        body.velocity = MathUtils.PolarToCartesian(angle, 200);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Flammable>()) {
            collision.gameObject.GetComponent<Flammable>().SetOnFire();
        }
        Destroy(gameObject);
    }
}
