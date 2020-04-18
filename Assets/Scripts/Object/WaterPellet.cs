using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPellet : MonoBehaviour {
    [SerializeField] GameObject waterSplashPrefab;
    [SerializeField] Rigidbody2D body;

    public void Shoot(float angle) {
        angle += (Random.value - 0.5f) / 3;
        Vector2 velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 200;
        body.velocity = velocity + Vector2.up * 60;
    }

    public void OnExpire() {
        Instantiate(waterSplashPrefab, transform.position, Quaternion.identity, transform.parent);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<Flammable>()) {
            Instantiate(waterSplashPrefab, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }
}
