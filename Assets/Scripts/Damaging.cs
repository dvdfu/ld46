using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour {
    [SerializeField]
    float amount = 1.0f;

    void OnCollisionEnter2D(Collision2D col) {
        Mortal m = col.gameObject.GetComponent<Mortal>();
        if (m != null) {
            m.Damage(amount);
        }
    }
}
