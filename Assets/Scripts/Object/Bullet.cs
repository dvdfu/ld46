using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] Collider2D collider;
    [SerializeField] Rigidbody2D body;

    public void Init(Collider2D parentCollider, float angle) {
        body.velocity = MathUtils.PolarToCartesian(angle, 2000);
        Physics2D.IgnoreCollision(collider, parentCollider);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Mortal mortal = collision.gameObject.GetComponent<Mortal>();
        if (mortal) {
            mortal.Damage(gameObject.tag, 3);
        }
    }
}
