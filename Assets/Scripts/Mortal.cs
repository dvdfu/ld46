using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mortal : MonoBehaviour {
    [SerializeField]
    float health = 100.0f;
    [SerializeField]
    bool alive = true;

    [SerializeField]
    Collider2D coll;
    [SerializeField]
    public UnityEvent dieEvent;

    // Returns whether the mortal object was killed or not
    public bool Damage(float amount) {
        health -= amount;
        if (health <= 0.0f && alive) {
            alive = false;
            dieEvent.Invoke();

            if (coll != null) {
                coll.enabled = false;
            }

            return true;
        }
        return false;
    }

    public bool GetAlive() {
        return alive;
    }

    public float GetHealth() {
        return health;
    }
}
