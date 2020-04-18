using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures multiple sources of fire only apply damage once
public class Flammable : MonoBehaviour {
    [SerializeField]
    bool onFire = false;
    [SerializeField]
    float fireDamage = 1.0f;
    // Number of times damage is applied per second
    [SerializeField]
    float fireTickRate = 5.0f;

    [SerializeField]
    GameObject smoke;
    [SerializeField]
    Mortal mortal;

    private float lastDamageApplied = 0;

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Fire") {
            onFire = true;
        }
    }

    void FixedUpdate() {
        smoke.SetActive(onFire);

        if (onFire) {
            onFire = false;

            float currentTime = Time.fixedTime;
            float elapsedTime = currentTime - lastDamageApplied;
            if (elapsedTime >= 1.0f / fireTickRate) {
                mortal.Damage(gameObject.tag, fireDamage);
                lastDamageApplied = currentTime;
            }
        }
    }
}
