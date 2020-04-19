using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteWobble : MonoBehaviour {
    [SerializeField] float period = 0.5f;
    [SerializeField] Transform wobbleTransform;

    void Start() {
        StartCoroutine(WobbleRoutine());
    }

    IEnumerator WobbleRoutine() {
        float t = 0;
        float phase, scale;
        while (true) {
            t += Time.deltaTime;
            phase = (t % 1) * Mathf.PI * 2;
            scale = 1 + Mathf.Sin(phase / period) / 10;
            wobbleTransform.localScale = new Vector3(scale, 1 / scale);
            yield return null;
        }
    }
}
