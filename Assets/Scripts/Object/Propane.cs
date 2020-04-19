using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propane : MonoBehaviour {
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject poofPrefab;

    public void Remove() {
        Instantiate(poofPrefab, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    void Start() {
        Instantiate(poofPrefab, transform.position, Quaternion.identity, transform.parent);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Remove();
    }
}
