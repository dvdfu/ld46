using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propane : MonoBehaviour {
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject poofPrefab;
    [SerializeField] Collider2D collider;

    public void Detonate() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }

    void Awake() {
        StartCoroutine(AppearRoutine());
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            Instantiate(poofPrefab, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        } else {
            Detonate();
        }
    }

    IEnumerator AppearRoutine() {
        collider.enabled = false;
        Instantiate(poofPrefab, transform.position, Quaternion.identity, transform.parent);
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }
}
