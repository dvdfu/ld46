using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinkler : MonoBehaviour {
    [SerializeField] GameObject waterPelletPrefab;

    void Start() {
        StartCoroutine(SprinkleRoutine());
    }

    IEnumerator SprinkleRoutine() {
        while (true) {
            Instantiate(waterPelletPrefab, transform.position + Vector3.up * 10, Quaternion.identity, transform.parent);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
