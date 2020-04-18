using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damaging : MonoBehaviour {
    [SerializeField]
    float amount = 1.0f;
    [SerializeField]
    public UnityEvent onKill;

    void OnCollisionEnter2D(Collision2D col) {
        doDamage(col.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) {
        doDamage(col.gameObject);
    }

    void doDamage(GameObject otherObject) {
        Mortal m = otherObject.GetComponent<Mortal>();
        if (m != null) {
            if (m.Damage(gameObject.tag, amount)) {
                onKill.Invoke();
            }
        }
    }
}
