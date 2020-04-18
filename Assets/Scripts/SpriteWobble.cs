using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteWobble : MonoBehaviour {
    const float PERIOD = 0.5f;

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
            scale = 1 + Mathf.Sin(phase / PERIOD) / 10;
            transform.localScale = new Vector3(scale, 1 / scale);
            yield return null;
        }
    }
}
