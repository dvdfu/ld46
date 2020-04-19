using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mortal : MonoBehaviour {
    [SerializeField] float health = 10;
    [SerializeField] bool alive = true;

    [SerializeField] Collider2D coll;
    [SerializeField] public UnityEvent dieEvent;

    // A list of tags this object is immune to damage from
    [SerializeField] List<string> immuneToTags = new List<string>();

    // Returns whether the mortal object was killed or not
    public bool Damage(string tag, float amount) {
        if (immuneToTags.Contains(tag)) return false;

        health -= amount;
        if (health <= 0.0f && alive) {
            Die();
            return true;
        }
        return false;
    }

    public void Die() {
        alive = false;
        dieEvent.Invoke();

        if (coll != null) {
            coll.enabled = false;
        }
    }

    public bool IsAlive() {
        return alive;
    }

    public float GetHealth() {
        return health;
    }
}
