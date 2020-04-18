using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour {
    [SerializeField] float fireDamage = 30; // Damage per second
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] Mortal mortal;

    int fireHealth = 0;

    public void SetOnFire() {
        fireHealth = 10;
        fire.Play();
        smoke.Play();
    }

    public void Extinguish() {
        fireHealth = 0;
        fire.Stop();
        smoke.Stop();
    }

    bool IsOnFire() {
        return fireHealth > 0;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Fire")) {
            SetOnFire();
        } else if (collider.gameObject.CompareTag("Water")) {
            if (IsOnFire()) {
                fireHealth--;
            } else {
                Extinguish();
            }
        }
    }

    void FixedUpdate() {
        if (IsOnFire()) {
            mortal.Damage(gameObject.tag, fireDamage * Time.deltaTime);
        }
    }
}
