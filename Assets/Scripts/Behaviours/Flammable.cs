using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour {
    [SerializeField] float fireDamage = 30; // Damage per second
    [SerializeField] ParticleSystem fire;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] Mortal mortal;

    bool isOnFire;

    public void SetOnFire() {
        if (!isOnFire) {
            isOnFire = true;
            fire.Play();
            smoke.Play();
        }
    }

    public void Extinguish() {
        if (isOnFire) {
            isOnFire = false;
            fire.Stop();
            smoke.Stop();
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Fire")) {
            SetOnFire();
        }
    }

    void FixedUpdate() {
        if (isOnFire) {
            mortal.Damage(gameObject.tag, fireDamage * Time.deltaTime);
        }
    }
}
