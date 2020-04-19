using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour {
    [SerializeField] Rigidbody2D body;

    void Start() {
        float angle = 90 + Random.Range(-45, 45);
        body.velocity = MathUtils.PolarToCartesian(angle, 200);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Flammable>()) {
            collision.gameObject.GetComponent<Flammable>().SetOnFire();
        }
        if (collision.gameObject.GetComponent<Mortal>()) {
            collision.gameObject.GetComponent<Mortal>().Damage(gameObject.tag, 1);
        }
        Destroy(gameObject);
    }
}
