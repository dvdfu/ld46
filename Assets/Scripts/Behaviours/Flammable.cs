using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flammable : MonoBehaviour {
    public UnityEvent igniteEvent = new UnityEvent();
    public UnityEvent extinguishEvent = new UnityEvent();

    [SerializeField] float fireDamage = 30; // Damage per second
    [SerializeField] GameObject steamPrefab;
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] Mortal mortal;

    int fireHealth = 0;

    public void SetOnFire() {
        if (!IsOnFire()) {
            fireHealth = 10;
            fire.Play();
            smoke.Play();
            igniteEvent.Invoke();
        }
    }

    public void Extinguish() {
        if (IsOnFire()) {
            fireHealth--;
            if (fireHealth == 0) {
                fire.Stop();
                smoke.Stop();
                extinguishEvent.Invoke();
                Instantiate(steamPrefab, transform.position + Vector3.up * 8, Quaternion.identity, transform.parent);
            }
        }
    }

    bool IsOnFire() {
        return fireHealth > 0;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Fire")) {
            SetOnFire();
        } else if (collider.gameObject.CompareTag("Water")) {
            Extinguish();
        }
    }

    void FixedUpdate() {
        if (IsOnFire()) {
            mortal.Damage(gameObject.tag, fireDamage * Time.deltaTime);
        }
    }
}
